
namespace Afca.Services
{
    using System.Data;
    using Afca.Common.DataAccessor;
    using Afca.Common.ServicesInterfaces;
    using Afca.Common.DataSets;
    using FrameworkForge.Common;
    using FrameworkForge.Common.DataStructures;
    using Afca.Common;
    using Afca.Services.Clases;

    public class GenerarService : IGenerarService
    {

        private ClsGenerar ObjGenerar = new ClsGenerar();
        private IXmlDataAccessor iXmlDA;
        //private IControlDataAccessor controlDA;
       public GenerarService()
        {
            iXmlDA = Afca.Common.AfcaDataAccessorProvider.GetXmlDataAccessor();
        }
        public bool GenerarXMLs()

        {
            
            iXmlDA = AfcaDataAccessorProvider.GetXmlDataAccessor();
            
            //ganaderiaDA= AfcaDataAccessorProvider.GetGanaderiaDataAccessor();
            //controlDA= AfcaDataAccessorProvider.GetControlDataAccessor();
            return true;
        }
        public XmlData SetXmlGanaderiasData(int argControlador)
        {
           // las 2 instrucciones hacen lo mismo 
            return ObjGenerar.SetGanaderiasToXMLganaderias(argControlador);
            //return iXmlDA.SetXmlGanaderiasByControladorID(argControlador);
        }
        public XmlData GetXmlGanaderiasData(int argControlador)
        {
            return iXmlDA.SetXmlGanaderiasByControladorID(argControlador);
        }
        public XmlData GetXmlControlesData(string argGanaderia)
        {
            return iXmlDA.GetXmlControlesByEstablo( argGanaderia);
        }
        public XmlData SetXmlControlesData(string argGanaderia)
        {
            return iXmlDA.SetXmlControlesByEstablo(argGanaderia);
        }


    }
}

