using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afca.Laboratorio
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new FileImporter());

        }
    }
}
