using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afca.Common.DataSets;

namespace Afca.Services.Clases
{
    public class ClsGanaderiasXML

    {
       public XmlData xmlData{get;set;}

        public ClsGanaderiasXML()
        {
            xmlData = new XmlData();
        }

        public XmlData GenerarXmlData()
        {
            return xmlData;
        }
    }
}
