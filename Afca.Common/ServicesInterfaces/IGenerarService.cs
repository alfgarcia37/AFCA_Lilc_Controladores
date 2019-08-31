using FrameworkForge.Common.Interfaces;
using FrameworkForge.Common.DataStructures;

namespace Afca.Common.ServicesInterfaces
{
 
    //using Afca.Common.DataSets;

    using Afca.Common;
    public interface IGenerarService : IService
    {
        Result  GenerarXMLs(int controlador);
        Result CheckXmlMaestroControlesData(int controlador);
        Result SetXmlGanaderiasData(int controlador);
        Result SetXmlControlesData(string ganaderia,int controlador);
        Result SetXmlTorosData(string ganaderia, int controlador);
        Result SetXmlInseminacionesData(string ganaderia, int controlador);
        Result SetXmlActualizacionesGlobalData( int controlador);

        //XmlData GetXmlGanaderiasData(int controlador);
        //XmlData GetXmlControlesData(string ganaderia);


        // Result SetXmlControlesData(string ganaderia);



        //XmlData LoadXmlInseminacionesData();
        //XmlData LoadXmlNacimientosData();
        //XmlData LoadXmlCelosData();
        //XmlData LoadXmlPatologiasData();

        //LecturaControlData LoadLecturaControlDataByParent(ItemData itemParent);

        //XmlData LoadLecturaControlDataByID(string argEstablo);


        //int DeleteLecturaControlDataByID(string argEstablo);

        //int UpdateLecturaControlData(XmlData data);




    }
}

