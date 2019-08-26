
namespace Afca.Common.DataAccessor
{
    using FrameworkForge.Common.Interfaces;
    using Afca.Common.DataSets;


    public interface IXmlsDataAccessor : IDataAccessor
    {
        //XMLDATOS (GANADERIAS)
        int SetXmlGanaderiasByControlador(int argControlador);
        int SetXmlControlesByGanaderia(string argGanaderia);
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