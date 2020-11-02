using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;
using System.IO;
using Ghostscript.NET;
using System.Drawing.Printing;

namespace Afca.UI.Reports
{
    public partial class ReportViewer : UserControl
    {
        public object ReportSource
        {
            get { return this.crystalReportViewer.ReportSource; }
            set
            {
                this.crystalReportViewer.ReportSource = value;
            }
        }
        public ReportViewer()
        {
            InitializeComponent();
        }
        public void ExportToTiff(ReportClass report)
        {
            try
            {
                var fileName = string.Format(@"{0}\{1}"
                        //, @"C:\Program Files (x86)\gs\gs9.53.3\bin"
                        ,Application.StartupPath
                        , "test.pdf");
                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileName);
                report.Close();
                int desired_x_dpi = 96;
                int desired_y_dpi = 96;

                //string inputPdfPath = @"E:\gss_test\test.pdf";
                //string outputPath = @"E:\gss_test\output\";
                string outputPath = Application.StartupPath;

                var lastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(
                        GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                        GhostscriptLicense.GPL);

                using (var rasterizer = new GhostscriptRasterizer())
                {
                    rasterizer.Open(fileName, lastInstalledVersion, true);

                    for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                    {
                        var pageFilePath = Path.Combine(outputPath, string.Format("Page-{0}.tiff", pageNumber));

                        var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                        img.Save(pageFilePath, ImageFormat.Tiff);

                        Console.WriteLine(pageFilePath);
                    }
                }
            }
            catch (Ghostscript.NET.GhostscriptAPICallException ggc)
            {
                System.Diagnostics.Debug.WriteLine(ggc.Message);
            }
        }
        public void ExportToTiff()
        {
            var fileName = string.Format(@"{0}\{1}"
                        //, @"C:\Program Files (x86)\gs\gs9.53.3\bin"
                        , Application.StartupPath
                        , "test.pdf");
            ((ReportClass)ReportSource).ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileName);

            var outputFile = string.Format(@"{0}\{1}", Application.StartupPath, "test.tiff");
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png16m);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(96, 96);
            dev.InputFiles.Add(fileName);
            dev.Pdf.FirstPage = 1;
            dev.Pdf.LastPage = 1;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.OutputPath = outputFile;
            dev.Process();
        }
        public void Export_As_24bit_Png()
        {
            var fileName = string.Format(@"{0}\{1}"
                        //, @"C:\Program Files (x86)\gs\gs9.53.3\bin"
                        , Application.StartupPath
                        , "test.pdf");
            ((ReportClass)ReportSource).ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileName);
            var outputFile = string.Format(@"{0}\{1}", Application.StartupPath, "test.tiff");
            
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png256);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(720, 720);
            dev.InputFiles.Add(fileName);
            dev.Pdf.FirstPage = 1;
            dev.Pdf.LastPage = 1;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.OutputPath =outputFile;
            dev.Process();
        }
        public void PrintTiff()
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = "PDFCreator";            

            PageSettings pgs = new PageSettings();
            pgs.PrinterSettings = ps;

            ((ReportClass)ReportSource).SummaryInfo.ReportTitle = "ES123421";

            ((ReportClass)ReportSource).PrintToPrinter(ps, pgs, false);
        }
        private void crystalReportViewer_Load(object sender, EventArgs e)
        {
            //    if (this.crystalReportViewer.ReportSource != null)
            //        ExportToTiff((ReportClass)crystalReportViewer.ReportSource);
            //Export_Second_And_Third_Pdf_Page_As_24bit_Png();
        }
    }
}
