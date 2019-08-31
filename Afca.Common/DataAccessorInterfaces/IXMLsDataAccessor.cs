
namespace Afca.Common.DataAccessor
{
    using FrameworkForge.Common.Interfaces;
    using Afca.Common.DataSets;

    using System;
    public interface IXmlsDataAccessor : IDataAccessor
    {
        //XMLDATOS (GANADERIAS)
        string CheckMaestroControlesData(string argEstablo);
        int SetXmlGanaderiasByControlador(int argcontrolador);
        int SetXmlControlesByGanaderia(string argGanaderia, int argControlador);
        int SetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador);
        int SetXmlTorosByGanaderia(string argGanaderia, int argControlador);
        int SetXmlActualizacionesGlobales( int argControlador);
        //XmlData GetXmlGanaderiasByID(int controlador);
        //XmlData SetXmlGanaderiasByControladorID(int argControladorID);
        //int DeleteXmlGanaderiasByID(int argControlador);


        ////XMLCONTROL (Controles)
        //XmlData GetXmlControlesByEstablo(string argEstablo);
        //XmlData SetXmlControlesByEstablo(string argEstablo);
        //int DeleteXmlControlByEstablo(string argEstablo);


        // Este me valdría para todos los xmls ???????????????????????????????
        int UpdateXmlData(XmlData data);
    }
}