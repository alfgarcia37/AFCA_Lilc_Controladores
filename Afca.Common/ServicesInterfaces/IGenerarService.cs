namespace Afca.Common.ServicesInterfaces
{
    using FrameworkForge.Common.Interfaces;
    using FrameworkForge.Common.DataStructures;
    //using Afca.Common.DataSets;

    using Afca.Common;
    public interface IGenerarService : IService
    {
        bool GenerarXMLs(int controlador);
        Result SetXmlGanaderiasData(int controlador);
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

