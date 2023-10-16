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
using FrameworkForge.Common.Interfaces;
using static Infragistics.Win.OSVersionUtilities;
using FrameworkForge.Common;
//using static System.Net.WebRequestMethods;

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
        BuzonLaboratorioTanqueData toSaveTanqueData;



        public delegate void SetTextDelegate(string text);

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
            toSaveTanqueData = new BuzonLaboratorioTanqueData();
            _service = AfcaServiceProvider.GetBuzonLaboratorioService();

            var utePathText = @"C:\Afca\Laboratorio\LILC_AFCA_230807_010212.I00043";
            utePath.Text = ConfigurationProvider.GetConfigurationValue("pathLaboratorio");
            //uneInterval.Value = 1;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IntervaloCumplido();
        }
        void IntervaloCumplido()
        {
            AppendTraceText(DateTime.Now.ToString());
            //this.timer.Stop();
            toSaveData.Clear();
            cicloActual = cicloActual + 1;
            LimpiarPanel();
            RealizarTrabajoPeriodico();

        }
        void LimpiarPanel()
        {
            if (cicloActual % 10 == 0)
                uteLog.Clear();
        }
        void RealizarTrabajoPeriodico()
        {
            DeshabilitarControles();
            //if (LeerFicheros(null))
            //    GuardarDatos();
            if (LeerFicherosTanque(null))
            {
                GuardarDatosTanque();
                HabilitarControles();
                timer.Stop();
            }

            //this.timer.Interval = TimeSpan.FromMinutes(
            //    double.Parse(uneInterval.Value.ToString())).TotalMilliseconds/10;

            //timer.Start();
        }
        void HabilitarControles()
        {
            uneInterval.Enabled =
                utePath.Enabled =
                utbtnStart.Enabled =
                utbtnStop.Enabled = true;
        }
        void DeshabilitarControles()
        {
            uneInterval.Enabled =
                utePath.Enabled =
                utbtnStart.Enabled =
                utbtnStop.Enabled = false;
        }
        bool LeerFicheros(FileInfo mifileinfo)
        {
            AppendTraceText("Leyendo ficheros...");

            if (mifileinfo == null)
            {
                if (!String.IsNullOrEmpty(utePath.Text))
                {
                    //if (Directory.Exists(utePath.Text))
                    {
                        var files = FindFiles(utePath.Text, "*.dat");
                        if (files.Length > 0)
                        {
                            int oks = 0;
                            foreach (FileInfo file in files)
                            {
                                oks = LeerFichero(file) ? oks + 1 : oks;
                            }
                            AppendTraceText(string.Format(@"{0} de {1} ficheros leidos correctamente", oks, files.Length));
                            return true;
                        }
                        else
                        {
                            AppendTraceText("0 ficheros cargados");
                            return false;
                        }

                    }
                    //else
                    //{
                    //    AppendTraceText("La ruta para buscar ficheros no existe o no es accesible");
                    //    return false;
                    //}
                }
                else
                {
                    AppendTraceText("La ruta para buscar ficheros está vacia");
                    return false;
                }
            }
            else
            {

                return LeerFichero(mifileinfo);

            }


        }
        bool LeerFicherosTanque(FileInfo mifileinfo)
        {
            AppendTraceText("Leyendo ficheros Tanque...");

            if (mifileinfo != null)//Leo desde el path
            {
                if (!String.IsNullOrEmpty(utePath.Text))
                {
                    return LeerFicheroTanque(mifileinfo);
                }
                else
                {
                    AppendTraceText("La ruta para buscar ficheros está vacia");
                    return false;
                }
            }
            else//lectura Automatica
            {

                var files = FindFiles(utePath.Text, "LILC_AFCA*.*");
                if (files.Length > 0)
                {
                    int oks = 0;
                    foreach (FileInfo file in files)
                    {
                        oks = LeerFicheroTanque(file) ? oks + 1 : oks;
                    }
                    AppendTraceText(string.Format(@"{0} de {1} ficheros leidos correctamente", oks, files.Length));
                    return true;
                }
                return false;
            }
        }
        FileInfo[] FindFiles(string directoryPath, string patternName)
        {
            try
            {


           
            FileInfo fileInfo = new FileInfo(directoryPath);
            // Obtener el DirectoryInfo de la ruta del archivo
            DirectoryInfo directoryInfo = fileInfo.Directory;
            // Verificar si el directorio existe antes de realizar cualquier operación
            if (!directoryInfo.Exists)
            {
                throw new Exception("El directorio especificado no existe.");
            }
            // DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            return directoryInfo.GetFiles(string.Format("{0}", patternName), SearchOption.TopDirectoryOnly);
            }
            catch
            {
                throw new Exception("El directorio especificado no existe.");
            }
        }
        bool LeerFichero(FileInfo file)
        {
            try
            {
                //string fichero = file.OpenText();
                StreamReader sr = new StreamReader(file.FullName);
                string line;
                int i = 0, MuestrasMal = 0;
                //preproceso 
                line = sr.ReadLine();
                while (line != null)
                {
                    try
                    {
                        if (line.Substring(26, 5) == "00000")
                            MuestrasMal = MuestrasMal + 1;
                        i++;
                    }
                    catch (System.Exception e)
                    {
                        AppendTraceText(string.Format("Error al leer la linea {0} del fichero {1}:{2}",
                            i, file.FullName, e.Message));

                        continue;
                    }
                    line = sr.ReadLine();
                }
                var PorcentajeMal = Convert.ToDouble(MuestrasMal) * 100 / i;
                PorcentajeMal = Math.Round(PorcentajeMal, 2);
                AppendTraceText(string.Format("Muestras Malas {0} de {1} : Porcentaje {2}", MuestrasMal, i, PorcentajeMal));
                //goto salto;

                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                sr.BaseStream.Position = 0;
                line = sr.ReadLine();
                i = 0;
                while (line != null)
                {
                    try
                    {
                        DataRow datarow = toSaveData.Tables["buzonLaboratorio"].NewRow();
                        //string etiquetaID = line.Substring(0, 14);

                        //lees los campos de la linea de texto y los metes en el campo correspondiene del datarow

                        datarow["ID"] = "69" + line.Substring(5, 12);
                        datarow["Grasa"] = decimal.Parse(line.Substring(17, 3)) / 100;
                        datarow["Proteina"] = decimal.Parse(line.Substring(20, 3)) / 100;
                        datarow["Lactosa"] = decimal.Parse(line.Substring(23, 3)) / 100;
                        datarow["RCS"] = int.Parse(line.Substring(26, 5));
                        datarow["Acetona"] = decimal.Parse(line.Substring(31, 4)) / 1000;
                        datarow["Bhb"] = decimal.Parse(line.Substring(35, 4)) / 1000;
                        datarow["Urea"] = int.Parse(line.Substring(51, 3));

                        datarow["AC26"] = decimal.Parse(line.Substring(54, 5)) / 100;
                        datarow["AC27"] = decimal.Parse(line.Substring(59, 5)) / 100;
                        datarow["AC28"] = decimal.Parse(line.Substring(64, 5)) / 100;
                        datarow["AC29"] = decimal.Parse(line.Substring(69, 5)) / 100;
                        datarow["Caseina"] = decimal.Parse(line.Substring(74, 5)) / 100;

                        datarow["FechaEntrega"] = line.Substring(45, 2) + "/" + line.Substring(47, 2) + "/" + line.Substring(49, 2);
                        datarow["FechaAnalisis"] = line.Substring(39, 2) + "/" + line.Substring(41, 2) + "/" + line.Substring(43, 2);
                        datarow["Controlador"] = int.Parse(line.Substring(9, 2));
                        datarow["Fichero"] = file.Name;


                        toSaveData.Tables[0].Rows.Add(datarow);
                        //GuardarDatos();
                        line = sr.ReadLine();
                        i++;
                    }
                    catch (System.Exception e)
                    {
                        AppendTraceText(string.Format("Error al leer la linea {0} del fichero {1}:{2}",
                            i, file.FullName, e.Message));
                        i++;
                        sr.Close();
                        return false;
                    }
                }
                sr.Close();
                // GuardarDatos();//quitarlo cuanod active todo
                sr.Close();
                int linea = 0;
                //....Mover Ficheros a directorio Cargados
                string nombreCarpeta = Path.GetDirectoryName(file.FullName) + "\\Cargados";
                string rutaCarpetaDestino = Path.Combine(nombreCarpeta, file.Name);
                File.Move(file.FullName, rutaCarpetaDestino);

                if (linea == 0)
                {
                    AppendTraceText("información del establo...");
                }
                return true;
            }
            catch (System.Exception e)
            {
                AppendTraceText(string.Format("letura de {0} incorrecta: {1}", file.Name, e.Message));
                return false;
            }
        }
        bool LeerFicheroTanque(FileInfo file)
        {
            try
            {
                StreamReader sr = new StreamReader(file.FullName);
                string line;
                int i = 0;

                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                sr.BaseStream.Position = 0;
                line = sr.ReadLine();
                i = 0;
                while (line != null)
                {
                    try
                    {
                        // Origen y Naturaleza?
                        DataRow datarow = toSaveTanqueData.Tables["buzonLaboratorioTanque"].NewRow();
                        datarow["AsociacionID"] = 1;
                        datarow["LaboratorioID"] = "1";
                        datarow["ID"] = line.Substring(0, 16).Trim();
                        //chemi Ruta,naturaleza,Urea y Origen
                        datarow["Ruta"] = "Ruta";
                        //fin chemi
                        //procedencia 2 :CA (todos son de Cantabria)
                        datarow["Establo"] = line.Substring(18, 7);
                        datarow["FechaRecogida"] = line.Substring(32, 2) + "/" + line.Substring(30, 2) + "/" + line.Substring(26, 4);
                        datarow["Bacterias"] = int.Parse(line.Substring(34, 6));
                        datarow["RCS"] = int.Parse(line.Substring(40, 6));
                        datarow["Grasa"] = decimal.Parse(line.Substring(46, 6)) / 100;
                        datarow["Proteina"] = decimal.Parse(line.Substring(52, 6)) / 100;
                        datarow["ExtractoSeco"] = decimal.Parse(line.Substring(58, 6)) / 100;
                        //SNF? (6)
                        datarow["Lactosa"] = decimal.Parse(line.Substring(70, 6)) / 100;
                        datarow["VarPC"] = decimal.Parse(line.Substring(76, 6)) / 100000;
                        datarow["PC"] = int.Parse(line.Substring(82, 6));
                        //Bitricos? (6)
                        datarow["Inhibidores"] = line.Substring(94, 1) == "S" ? true : false;
                        datarow["Urea"] = int.Parse(line.Substring(95, 6));

                        datarow["MediaBacterias"] = int.Parse(line.Substring(101, 6));
                        datarow["MediaRCS"] = int.Parse(line.Substring(107, 6));
                        datarow["MediaGrasa"] = decimal.Parse(line.Substring(113, 6)) / 100;
                        datarow["MediaProteina"] = decimal.Parse(line.Substring(119, 6)) / 100;
                        datarow["MediaExtractoSeco"] = decimal.Parse(line.Substring(125, 6)) / 100;
                        datarow["MediaLactosa"] = decimal.Parse(line.Substring(131, 6)) / 100;
                        datarow["MediaPC"] = (int)decimal.Parse(line.Substring(137, 6));
                        datarow["MediaUrea"] = (int)decimal.Parse(line.Substring(143, 6));

                        datarow["IgG"] = line.Substring(149, 6);//calostros (155,6)
                        //Aflatoxinas (155,6)
                        //Quinolonas (161,6)
                        //Mezcla (167,10)
                        datarow["Industria"] = line.Substring(177, 6).ToString(); //Ruta o Industria
                        datarow["CodEnIndustria"] = line.Substring(183, 14).ToString(); //Ruta o Industria
                        datarow["Etiqueta"] = line.Substring(197, 6).ToString(); //Ruta o Industria
                        datarow["CEA"] = line.Substring(203, 14);//Cod en Industria
                        datarow["Mes"] = int.Parse(line.Substring(217, 2));
                        datarow["Ano"] = int.Parse(line.Substring(219, 4));
                        datarow["Cierre"] = line.Substring(223, 1) == "S" ? true : false;
                        datarow["Origen"] = line.Substring(224, 1);
                        datarow["Naturaleza"] = line.Substring(225, 1);

                        datarow["NC"] = line.Substring(226).Trim().ToString();
                        datarow["Fichero"] = file.Name;
                        datarow["FechaRegistro"] = DateTime.Now;

                        toSaveTanqueData.Tables[0].Rows.Add(datarow);
                        //GuardarDatosTanque();
                        line = sr.ReadLine();
                        i++;
                    }
                    catch (System.Exception e)
                    {
                        AppendTraceText(string.Format("Error al leer la linea {0} del fichero {1}:{2}",
                            i, file.FullName, e.Message));
                        i++;
                        sr.Close();
                        return false;
                    }
                }
                sr.Close();
                //....Mover Ficheros a directorio Cargados
                string nombreCarpeta = Path.GetDirectoryName(file.FullName) + "\\old";
                string rutaCarpetaDestino = Path.Combine(nombreCarpeta, file.Name);
                File.Move(file.FullName, rutaCarpetaDestino);

                return true;
            }
            catch (System.Exception e)
            {
                AppendTraceText(string.Format("letura de {0} incorrecta: {1}", file.Name, e.Message));
                return false;
            }
        }

        void GuardarDatos()
        {
            AppendTraceText("Guardando la informacion...");

            try
            {
                _service.UpdateBuzonLaboratorioData(toSaveData);
                toSaveData.AcceptChanges();
                _service.UpdateBuzonLaboratorioEmparejar();
                AppendTraceText("Información guardada correctamente");
            }
            catch (System.Exception e)
            {
                AppendTraceText(string.Format("Información NO GUARDADA. Error:{0}", e.Message));
            }
        }
        void GuardarDatosTanque()
        {
            AppendTraceText("Guardando la informacion...");

            try
            {
                _service.UpdateBuzonLaboratorioTanqueData(toSaveTanqueData);
                toSaveTanqueData.AcceptChanges();
                _service.UpdateBuzonLaboratorioTanqueEmparejar();
                AppendTraceText("Información guardada correctamente");
                timer.Stop();
                //timer.Interval = 0;
            }
            catch (System.Exception e)
            {
                AppendTraceText(string.Format("Información NO GUARDADA. Error:{0}", e.Message));
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

            if (uteLog.InvokeRequired)
            {
                uteLog.Invoke(new SetTextDelegate(SetTextCallBack)
                    , new object[1] { text });
            }
            else
            {
                this.uteLog.AppendText(string.Format("{0}{1}", text, Environment.NewLine));
                uteLog.Select(uteLog.Text.Length - 1, uteLog.Text.Length);
                uteLog.ScrollToCaret();
            }
        }
        void SetTextCallBack(string text)
        {
            this.uteLog.AppendText(string.Format("{0}{1}", text, Environment.NewLine));
            uteLog.Select(uteLog.Text.Length - 1, uteLog.Text.Length);
            uteLog.ScrollToCaret();
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
                this.timer.Interval = TimeSpan.FromMinutes(double.Parse(uneInterval.Value.ToString())).TotalMilliseconds / 10;

            if (String.IsNullOrEmpty(utePath.Text))
            {
                MessageBox.Show("Ha de establecer un valor para la ruta delos ficheros");
                return;
            }
            timer.Start();
            AppendTraceText("Tarea arrancada");
            //IntervaloCumplido();//La primera vez se lanza manualmente
        }
        private void utbtnStop_Click(object sender, EventArgs e)
        {
            AppendTraceText("Tarea parada");
            timer.Stop();
            timer.Interval = 0;
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (utePath.Text != "")
            {
                FileInfo file = new FileInfo(utePath.Text);
                if (file.Exists)
                {
                    if (LeerFicheros(file))
                        GuardarDatos();
                }
            }
            //FileInfo file = new FileInfo(utePath.Text);
            //LeerFichero(file);
        }

        private void ultraPanelTop_PaintClient(object sender, PaintEventArgs e)
        { }

        private void button1_Click(object sender, EventArgs e)
        { //openFileDialog1
            //OpenFileDialog open = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.ToString() != " ")
            {
                //Movemos la ruta del archivo a nuestro Textbox creado para su posterior uso.
                utePath.Text = openFileDialog1.FileName;
            }
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {   //Tanques de LILC
            if (utePath.Text != "")
            {
                FileInfo file = new FileInfo(utePath.Text);
                if (file.Exists)
                {
                    if (LeerFicherosTanque(file))
                        GuardarDatosTanque();
                }
            }
        }

        private void uteLog_ValueChanged(object sender, EventArgs e)
        {

        }
        private void uneInterval_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
