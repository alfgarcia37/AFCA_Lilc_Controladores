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
        }
        static void Encrypt()
        {
            string conectionString = @"Server=localhost\SQL2K12;Database=reggen;Integrated Security=True;";
            string cencrypted = Encriptador.Encrypt(conectionString, false);
            Console.WriteLine(cencrypted);
            Console.ReadLine();
        }
        static void Decrypt()
        {
            string cdecrypted = Encriptador.Decrypt("gZziQZqwgFz8m+jB9Y3Me5ZPqyFCdJ9zzzSE6Tq9/iWg69dVXCdbdptGR4x5PhY32J/ZPIM289BOyIhmgbeqzg=");
            Console.WriteLine(string.Format("Afca.xps15: {0}", cdecrypted));
            Console.ReadLine();
        }
    }
}

