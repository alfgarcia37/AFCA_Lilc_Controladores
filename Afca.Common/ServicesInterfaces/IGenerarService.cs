namespace Afca.Common.ServicesInterfaces
{
    using FrameworkForge.Common.Interfaces;
    using FrameworkForge.Common.DataStructures;
    using Afca.Common.DataSets;


    public interface IGenerarService : IService
    {
        bool GenerarXMLs();
        
        XmlData GetXmlGanaderiasData(int controlador);
        XmlData GetXmlControlesData(string ganaderia);

        XmlData SetXmlGanaderiasData(int controlador);
        XmlData SetXmlControlesData(string ganaderia);



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

