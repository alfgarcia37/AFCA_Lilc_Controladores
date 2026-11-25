using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Afca.Common.DataReports;
using System.Data;

using System.Windows.Forms;
using System.Drawing;
using System.IO;

using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;

namespace Afca.UI
{
    static class Program
    {
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NacimientoReportTest();
        }
        static void NacimientoReportTest()
        {
            CartaRegistroReportData data = new CartaRegistroReportData();

            DataRow nombreRow = data.Nombre.NewRow();
            string nombre = "BENERA MATCHUKEN PALOMERA DE LA HUERTA";

            int i = 1;

            foreach (char letra in nombre.ToCharArray())
            {
                if (i == 31)
                    break;

                nombreRow[string.Format("c{0}", i)] = letra.ToString();
                i++;
            }

            data.Nombre.Rows.Add(nombreRow);

            DataRow datosRow = data.Datos.NewRow();
            datosRow["CodigoConafe"] = "SHP12468987291";
            datosRow["Sexo"] = "H";
            datosRow["FNac"] = "09/06/1977";
            datosRow["PadreNombre"] = "MACHUKITO FEVER 201";
            datosRow["PadreNum"] = "ES083824691089";
            datosRow["MadreNombre"] = "REINONA FEVER 201";
            datosRow["MadreNum"] = "ES083824691089";
            datosRow["CapaColor"] = "BN";
            datosRow["PartoGemelar"] = true;
            datosRow["GemeloSexo"] = "H";
            datosRow["DeTransEmbriones"] = true;
            datosRow["FCubricion"] = DateTime.Now;
            datosRow["TipoCubricion"] = "IN";//IN,MN,LP
            datosRow["nf_FIncorporacion"] = DateTime.Now;
            datosRow["nf_FUltimoParto"] = DateTime.Now;
            datosRow["nf_PartoNum"] = 4;
            datosRow["FirmanteLugar"] = "SANTANDER";
            datosRow["FirmaDia"] = "20";
            datosRow["FirmaMes"] = "OCTUBRE";
            datosRow["Firmayear"] = "2020";
            datosRow["OT"] = "12323531";
            datosRow["GanaderiaNombre"] = "FARMY LAND";
            datosRow["GanaderiaCodigo"] = "S123456";

            string imageName = "ES030604992046.png";

            Image silueta = Image.FromFile(string.Format(@"{0}\{1}", Application.StartupPath, imageName));

            using (var ms = new MemoryStream())
            {
                silueta.Save(ms, silueta.RawFormat);
                datosRow["Silueta"] = ms.ToArray();
            }


            string imageFirmaName = "FIRMA.png";

            Image firma = Image.FromFile(string.Format(@"{0}\{1}", Application.StartupPath, imageFirmaName));

            using (var ms = new MemoryStream())
            {
                firma.Save(ms, firma.RawFormat);
                datosRow["Firma"] = ms.ToArray();
            }
            data.Datos.Rows.Add(datosRow);

            DataRow establoRow = data.Establo.NewRow();
            establoRow["e1"] = "0";
            establoRow["e2"] = "1";
            establoRow["e3"] = "3";
            establoRow["e4"] = "4";
            data.Establo.Rows.Add(establoRow);

            DataRow cibRow = data.CIB.NewRow();
            cibRow["Prefijo"] = "ES";

            string cib = "098578773918";
            int j = 1;
            foreach (char c in cib.ToCharArray())
            {
                cibRow[string.Format("c{0}", j)] = c.ToString();
                j++;
            }
            data.CIB.Rows.Add(cibRow);

            DataRow cibPRow = data.CIBPortadora.NewRow();
            cibPRow["Prefijo"] = "ES";

            string cibP = "098578773918";
            int k = 1;
            foreach (char c in cibP.ToCharArray())
            {
                cibPRow[string.Format("c{0}", k)] = c.ToString();
                k++;
            }
            data.CIBPortadora.Rows.Add(cibPRow);

            DataRow codGanRow = data.Ganaderia.NewRow();

            string ganaderia = datosRow["GanaderiaCodigo"].ToString();
            int m = 1;
            foreach (char c in ganaderia.ToCharArray())
            {
                codGanRow[string.Format("c{0}", m)] = c.ToString();
                m++;
            }
            data.Ganaderia.Rows.Add(codGanRow);


            Form viewerForm = new Form();

            Panel panelTop = new Panel();
            viewerForm.Controls.Add(panelTop);
            panelTop.Dock = DockStyle.Bottom;

            Button btn = new Button();
            btn.Text = "Print";
            btn.Click += Btn_Click;

            panelTop.Controls.Add(btn);

            Panel panelReport = new Panel();
            viewerForm.Controls.Add(panelReport);
            panelReport.Dock = DockStyle.Fill;


       
           

            panelReport.Controls[0].Dock = DockStyle.Fill;

            viewerForm.Controls.Add(panelReport);
            viewerForm.Controls[1].Dock = DockStyle.Fill;


            Application.Run(viewerForm);
        }
        private static void Btn_Click(object sender, EventArgs e)
        {
            //_viewer.ExportToTiff((ReportClass)_viewer.ReportSource);
          
        }
    }
}
