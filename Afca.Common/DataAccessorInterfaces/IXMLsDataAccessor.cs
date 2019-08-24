
namespace Afca.Common.DataAccessor
{
    using FrameworkForge.Common.Interfaces;
    using Afca.Common.DataSets;


    public interface IXmlDataAccessor : IDataAccessor
    {
        //XMLDATOS (GANADERIAS)
        //XmlData LoadXmlDatos();//       ---->>>> No lo Pongo porque en mi caso no tiene sentido
        XmlData GetXmlGanaderiasByID(int controlador);
        XmlData SetXmlGanaderiasByControladorID(int argControladorID);
        int DeleteXmlGanaderiasByID(int argControlador);
        

        //XMLCONTROL (Controles)
        XmlData GetXmlControlesByEstablo(string argEstablo);
        XmlData SetXmlControlesByEstablo(string argEstablo);
        int DeleteXmlControlByEstablo(string argEstablo);


        // Este me valdría para todos los xmls ???????????????????????????????
        int UpdateXmlData(XmlData data);
    }
}