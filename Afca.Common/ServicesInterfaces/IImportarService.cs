using FrameworkForge.Common.Interfaces;
using FrameworkForge.Common.DataStructures;

namespace Afca.Common.ServicesInterfaces
{
 
    using Afca.Common.DataSets;

    using Afca.Common;
    public interface IImportarService : IService
    {
        Result  ImportarXMLs(int controlador);

        //XmlData LoadXMLDatosDataByControlador(int argControlador);... realmente no me hace aqui, pq se usa para DA

        Result CheckXmlEstablosDuplicadosData(int controlador);
        Result CheckXmlControlesData(int controlador);
        Result CheckXmlPartosData(int controlador);
        Result PasarDownToXml(int controlador);

        Result GetXmlGanaderiasData(int controlador);
        Result GetXmlControlesData(string ganaderia,int controlador,System.DateTime FPrevista,string TipoControl);
        Result GetXmlInseminacionesData(string ganaderia, int controlador);
        Result GetXmlNacimientosData(string ganaderia, int controlador);
        Result GetXmlRcsData(int controlador);
        //Result GetXmlActualizacionesGlobalData( int controlador);

        


    }
}

