using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Utilities
{
    public class FileTracer
    {
        long traceFileSizeLimit = 40;    //Maximun size of trace file in MB
        int lastTraceCheck = 0;

        string fileName;
        string filePrefix;

        public string FileName => fileName;

        public FileTracer(string argFilePrefix)
        {
            filePrefix = argFilePrefix;
            SetFileName();
        }
        void SetFileName()
        {
            DateTime current = DateTime.Now;
            fileName = string.Format(@"{0}\{1}_{2}.txt"
                    , Environment.CurrentDirectory, filePrefix, current.ToString("_yyMMdd_hhmmss"));
        }
        public void AppendTraceText(string text)
        {
            text = DateTime.Now.ToString("dd/MM/yy-hh:mm:ss") + Environment.NewLine + text;

            using (StreamWriter sw = File.AppendText(this.fileName))
            {
                sw.WriteLine(text);
                sw.Flush();
                sw.Close();
            }
            this.CheckTraceFileSizeLimit();
        }
        void CheckTraceFileSizeLimit()
        {
            if (lastTraceCheck >= 10000)
            {
                long lenght = new FileInfo(this.fileName).Length;
                long sizeMB = lenght / 1000000;

                if (sizeMB > traceFileSizeLimit)
                {
                    DateTime current = DateTime.Now;
                    SetFileName();
                }
                lastTraceCheck = 0;
            }

            lastTraceCheck++;
        }
    }
}
