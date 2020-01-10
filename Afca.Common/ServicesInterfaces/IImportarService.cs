using FrameworkForge.Common.Interfaces;
using FrameworkForge.Common.DataStructures;

namespace Afca.Common.ServicesInterfaces
{
 
    using Afca.Common.DataSets;

    using Afca.Common;
    using System.Data;

    public interface IImportarService : IService
    {
        Result  ImportarXMLs(int controlador);
        //XmlData LoadXMLDatosDataByControlador(int argControlador);... realmente no me hace aqui, pq se usa para DA
        Result CheckFromControlXmlDuplicados(int controlador);
        DataSet CheckFromXmlData(int controlador);      
        Result PasarDownToXml(int controlador);
        Result GetXmlGanaderiasData(string ganaderia);
        Result GetXmlControlesData(string ganaderia,int controlador,System.DateTime FPrevista,string TipoControl);
        Result GetXmlInseminacionesData(string ganaderia, int controlador);
        Result GetXmlNacimientosData(string ganaderia, int controlador);
        Result GetXmlRcsData(int controlador);
        Result GetXmlActualizacionesGlobalData( int controlador);
    }
}

