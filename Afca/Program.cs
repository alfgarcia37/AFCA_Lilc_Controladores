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
            // fer....   string conectionString = @"Server=CTXSQL\SQL14_2019;Database=CLM2;Integrated Security=True;";

            string conectionString = @"Server=CTXSQL\SQL14_2019;Database=controllechero;Persist Security Info=true;User ID=alfonsog;Password=afca37;";
            string conectionString1 = @"Server=localhost\SQLE_2K17;Database=controllechero_dev;Integrated Security=True;";

            //string conectionString = @"Data Source=CTX2011\CTXSQL2011;Initial Catalog=CLM2;Persist Security Info=true;User ID=alfonsog;Password=afca37
            string cencrypted = Encriptador.Encrypt(conectionString, false);
            string cencrypted1 = Encriptador.Encrypt(conectionString1, false);
            //ctxsql14
            //A01AAP/PXELFLDbJ/DXp88DrLt0YmD5zuQhQM3QGTmqtZwO8P2TlVtbKqAoG0WDylShtFavNgOTriei3KYp7yd08w56lzKGOFtPhcwEKXKG80kWM6MVfAmcPd7Mj5NvtK19/7ZoaEQz24skai4Bxzg==
            //A01AAP/PXELFLDbJ/DXp88DrLt0YmD5zuQhQM3QGTmpPle + aoUAXOGENlcjK74hcByUraWAslquBEi3ICR4jpTOJ6QGqN9dJpILVIvW6tYM + WJGicMInHFuKSNavLWOQPfl5t40QOx4PVO3M/IpH/g==
            

                              Console.WriteLine(cencrypted);
            Console.WriteLine(cencrypted1);
            Console.ReadLine();
        }
        static void Decrypt()
        {
            string cdecrypted = Encriptador.Decrypt("gZziQZqwgFz8m+jB9Y3Me6XV7kBVmFuxChQcrc36gR79dhWkSIftThm/5Bf1IJnGiJ3sWsbsMjM/LKrcmLzQpg==");
            Console.WriteLine(string.Format("Afca.dell: {0}", cdecrypted));
            Console.ReadLine();
        }
    }
}

