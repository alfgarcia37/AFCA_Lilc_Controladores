using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Afca.Common.ServicesInterfaces;
using Afca.Common.DataSets;
using Afca.Common;
using Infragistics.Win.Misc;

namespace Afca.Laboratorio
{
    public partial class FileImporter : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        int ciclosLimpiezaPanel = 10;
        int cicloActual = 0;

        private string traceFileName;     // Especifica la ruta y el nombre del fichero de log
        long traceFileSizeLimit = 40;    //Maximun size of trace file in MB

        IBuzonLaboratorioService _service;
        BuzonLaboratorioData toSaveData;

        public FileImporter()
        {
            InitializeComponent();
            InitializeVariables();
        }
        void InitializeVariables()
        {
            this.timer.Elapsed += Timer_Elapsed;
            this.traceFileName = GetTraceFileName();
            toSaveData = new BuzonLaboratorioData();
            _service = AfcaServiceProvider.GetBuzonLaboratorioService();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IntervaloCumplido();
        }
        void IntervaloCumplido()
        {
            this.timer.Stop();
            toSaveData.Clear();
            LimpiarPanel();
            RealizarTrabajoPeriodico();
            cicloActual++;
        }
        void LimpiarPanel()
        {
            if (cicloActual % 10 == 0)
                uteLog.Clear();           
        }
        void RealizarTrabajoPeriodico()
        {
            DeshabilitarControles();
            if (LeerFicheros())
                GuardarDatos();
            HabilitarControles();
            timer.Interval = 
                TimeSpan.FromMinutes((double)uneInterval.Value).TotalMilliseconds;
            timer.Start();
        }
        void HabilitarControles()
        {
            uneInterval.Enabled =
                utePath.Enabled = 
                utbtnStart.Enabled =
                utbtnStop.Enabled = false;
        }
        void DeshabilitarControles()
        {
            uneInterval.Enabled =
                utePath.Enabled =
                utbtnStart.Enabled =
                utbtnStop.Enabled = true;
        }
        bool LeerFicheros()
        {
            AppendTraceText("Leyendo ficheros...");
            
            if (!String.IsNullOrEmpty(utePath.Text))
            {
                if (Directory.Exists(utePath.Text))
                {
                    var files = FindFiles(utePath.Text, "*.txt");

                    if (files.Length > 0)
                    {
                        int oks = 0;
                        foreach(FileInfo file in files)
                             oks = LeerFichero(file)? oks+1:oks;

                        AppendTraceText(string.Format(@"{0} de {1} ficheros leidos correctamente"
                                                        ,oks,files.Length));
                        return true;
                    }
                    else
                    {
                        AppendTraceText("0 ficheros cargados");
                        return false;
                    }
                }
                else
                {
                    AppendTraceText("La ruta para buscar ficheros no existe o no es accesible");
                    return false;
                }
            }
            else
            {
                AppendTraceText("La ruta para buscar ficheros está vacia");
                return false;
            }
        }
        FileInfo[] FindFiles(string directoryPath, string patternName)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new Exception("El directorio especificado no existe.");
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            return directoryInfo.GetFiles(string.Format("{0}.*", patternName), SearchOption.TopDirectoryOnly);
        }
        bool LeerFichero(FileInfo file)
        {
            try
            {
                //leer información del fichero y guardarlo en un nuevo registro 
                //en el dataset Buzon Laboratorio Data
                return true;
            }
            catch(System.Exception e){
                AppendTraceText(string.Format("letura de {0} incorrecta: {1}",file.Name,e.Message));
                return false;
            }
        }
        void GuardarDatos()
        {
            AppendTraceText("Guardando la informacion...");

            try
            {
                _service.UpdateBuzonLaboratorioData(toSaveData);
                AppendTraceText("Información guardada correctamente");
            }
            catch (System.Exception e)
            {
                AppendTraceText(string.Format("Información NO GUARDADA. Error:{0}",e.Message));
            }
        }
        public void AppendTraceText(string text)
        {
            text = DateTime.Now.ToString("dd/MM/yy-hh:mm:ss") + " " + text;

            if (!String.IsNullOrEmpty(traceFileName))
            {
                using (StreamWriter sw = File.AppendText(this.traceFileName))
                {
                    sw.WriteLine(text);
                    sw.Flush();
                    sw.Close();
                }
                CheckTraceFileSizeLimit();
            }

            this.uteLog.AppendText(string.Format("{0}{1}",text,Environment.NewLine));
        }
        void CheckTraceFileSizeLimit()
        {

            long lenght = new FileInfo(this.traceFileName).Length;
            long sizeMB = lenght / 1000000;

            if (sizeMB > traceFileSizeLimit)
            {
                this.traceFileName = GetTraceFileName();
            }

        }
        string GetTraceFileName()
        {
            DateTime current = DateTime.Now;
            return Environment.CurrentDirectory +
                        "\\" + "LabLog" + current.ToString("_yyMMdd_hhmmss") + ".txt";
        }
        private void utbtnStart_Click(object sender, EventArgs e)
        {
            if (uneInterval.Value == null)
            {
                MessageBox.Show("Ha de establecer un valor para el intervalo de acutalización");
                return;
            }
            else           
                this.timer.Interval = TimeSpan.FromMinutes((double)uneInterval.Value).TotalMilliseconds;
            
            if (String.IsNullOrEmpty(utePath.Text))
            {
                MessageBox.Show("Ha de establecer un valor para la ruta delos ficheros");
                return;
            }

            AppendTraceText("Tarea arrancada");
            IntervaloCumplido();//La primera vez se lanza manualmente
        }
        private void utbtnStop_Click(object sender, EventArgs e)
        {
            AppendTraceText("Tarea parada");
            timer.Stop();
        }
    }


}
