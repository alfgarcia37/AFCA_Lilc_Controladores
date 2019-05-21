using Microsoft.Practices.EnterpriseLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Afca
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FrameworkForge.UI.ApplicationContext.Run();
            //Encrypt();
        }
        static void Encrypt()
        {
            string conectionString = @"Server=localhost\SQL_2K12;Database=reggen;Integrated Security=True;";
            string cencrypted = Encriptador.Encrypt(conectionString, false);
            Console.WriteLine(cencrypted);
            Console.ReadLine();
        }
        static void Decrypt()
        {
            string cdecrypted = Encriptador.Decrypt("hs3sv1dUaSDnW1bcIpOpCHMWHAgZ5n4CBhpFRV+4OsPco9wH1oLMCiWuN1TlCOGL25+kScOoOP0hl46AQ2IWPqNgz+gRNXgHY+owO7Re4P4=");
            Console.WriteLine(string.Format("Afca.xps15: {0}", cdecrypted));
            Console.ReadLine();
        }
    }
}

