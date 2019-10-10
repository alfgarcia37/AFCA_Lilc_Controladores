using FrameworkForge.Common.Interfaces;
using FrameworkForge.Common.DataStructures;

namespace Afca.Common.ServicesInterfaces
{
 
    //using Afca.Common.DataSets;

    using Afca.Common;
    public interface IGenerarService : IService
    {
        Result  GenerarXMLs(int controlador);
        Result CheckToXmlData(int controlador);
        Result SetXmlGanaderiasData(int controlador);
        Result SetXmlControlesData(string ganaderia,int controlador);
        Result SetXmlTorosData(string ganaderia, int controlador);
        Result SetXmlInseminacionesData(string ganaderia, int controlador);
        Result SetXmlNacimientosData(string ganaderia, int controlador);
        Result SetXmlActualizacionesGlobalData( int controlador);

        Result PasarXmlToUP(int controlador);

       




    }
}

