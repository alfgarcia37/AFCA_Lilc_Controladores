using Microsoft.Practices.EnterpriseLibrary.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrameworkForge.Common;
using System.Diagnostics;

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
        static void ConfigureAgmWebAPI()
        {
            bool modifyConfig = false;

            string url = "https://agmwebapis2.azurewebsites.net/api";
            string credentials = "fernando@gmail.com;Pepona123";

            Debug.WriteLine($"Url:{url}");
            Debug.WriteLine($"credentials:{credentials}");

            url = Encriptador.Encrypt(url, false);
            credentials = Encriptador.Encrypt(credentials, false);

            Debug.WriteLine($"Url:{url}");
            Debug.WriteLine($"credentials:{credentials}");

            if (modifyConfig)
            {
                if (ConfigurationProvider.GetConfigurationValue("AgmWebApiUrl") == null)
                    ConfigurationProvider.RegisterConfigEntry("AgmWebApiUrl", url, @"C:\My Projects\VS2017\Production\AFCA_ControlLechero\Afca\bin\Debug");
                else
                    ConfigurationProvider.UpdateAppSettingValue("AgmWebApiUrl", url);

                if (ConfigurationProvider.GetConfigurationValue("AgmWebApiUser") == null)
                    ConfigurationProvider.RegisterConfigEntry("AgmWebApiUser", credentials, @"C:\My Projects\VS2017\Production\AFCA_ControlLechero\Afca\bin\Debug");
                else
                    ConfigurationProvider.UpdateAppSettingValue("AgmWebApiUser", credentials);
            }
      

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
        static void LeerFichero()
        {
            using (StreamReader reader = new StreamReader(@"J:\txt embarazo.txt"))
            {
                using (StreamWriter writer = new StreamWriter(@"J:\output_txt_embarazo.txt"))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] split = line.Split(new char[] { '|' });

                        string firstPart = split[0];
                        string[] firstSplit = firstPart.Split(new char[] { '.' });

                        int i = 1;
                        while (i < split.Length && split[i] == "")
                        {
                            i++;
                        }

                        writer.WriteLine("{0}|{1}|{2}", firstSplit[0],firstSplit[1], split[i]);

                    }
                }
            }
        }
    }
}

