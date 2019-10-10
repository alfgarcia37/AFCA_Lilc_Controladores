
namespace Afca.Common.DataAccessor
{
    using FrameworkForge.Common.Interfaces;
    using Afca.Common.DataSets;

    using System;
    public interface IXmlsDataAccessor : IDataAccessor
    {
        //XMLDATOS EXPORTAR========================================================
        // Chequeos -> Exportar -> ActGlobales -> Pasar To XML ....................................
        string CheckToXMLData(string argEstablo);
        int SetXmlGanaderiasByControlador(int argcontrolador);
        int SetXmlControlesByGanaderia(string argGanaderia, int argControlador);
        int SetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador);
        int SetXmlTorosByGanaderia(string argGanaderia, int argControlador);
        int SetXmlNacimientosByGanaderia(string argGanaderia, int argControlador);
        int SetXmlActualizacionesGlobales( int argControlador);
        int PasarXmlToUPData(int argControlador);

        //XMLDATOS IMPORTAR=========================================================
        // Pasar From XML -> Chequeos -> ActGlobales -> Importar Chequeos -> Auditoría .........
        int PasarDownToXmlData(int argControlador);
        XmlData LoadXMLDatosDataByControlador(int argcontrolador);
        string CheckFromXMLData(int argControlador);//establos ya leidos
        
        int GetXmlGanaderiasByControlador(int argcontrolador);
        int GetXmlControlesByGanaderia(string argGanaderia, int argControlador,DateTime argFPrevista,string argTipoControl);
        int GetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador);
        int GetXmlNacimientosByGanaderia(string argGanaderia, int argControlador);
        int GetXmlRcsByControlador( int argControlador);
        int GetXmlActualizacionesGlobales(int argControlador);
     

        // Este me valdría para todos los xmls ???????????????????????????????
        int UpdateXmlData(XmlData data);
    }
}