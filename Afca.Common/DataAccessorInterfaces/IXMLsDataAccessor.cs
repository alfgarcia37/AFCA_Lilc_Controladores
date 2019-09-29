
namespace Afca.Common.DataAccessor
{
    using FrameworkForge.Common.Interfaces;
    using Afca.Common.DataSets;

    using System;
    public interface IXmlsDataAccessor : IDataAccessor
    {
        //XMLDATOS EXPORTAR========================================================
        //.1.Exportar Chequeos....................................
        string CheckMaestroControlesData(string argEstablo);
        //.2.Exportar Set's de XML........................................................
        int SetXmlGanaderiasByControlador(int argcontrolador);
        int SetXmlControlesByGanaderia(string argGanaderia, int argControlador);
        int SetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador);
        int SetXmlTorosByGanaderia(string argGanaderia, int argControlador);
        int SetXmlNacimientosByGanaderia(string argGanaderia, int argControlador);
        int SetXmlActualizacionesGlobales( int argControlador);
        int PasarXmlToUPData(int argControlador);

        //XMLDATOS IMPORTAR=========================================================
        //.1.Importar Chequeos.........
        int PasarDownToXmlData(int argControlador);
        XmlData LoadXMLDatosDataByControlador(int argcontrolador);
        string CheckEstablosDuplicadosData(int argControlador);//establos ya leidos
        string CheckControlesDuplicadosData(int argControlador);
        string CheckPartosDuplicadosData(int argControlador);
        //.2...Importar GET's de Xmls......................................................
        int GetXmlGanaderiasByControlador(int argcontrolador);
        int GetXmlControlesByGanaderia(string argGanaderia, int argControlador,DateTime argFPrevista,string argTipoControl);
        int GetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador);
        int GetXmlNacimientosByGanaderia(string argGanaderia, int argControlador);
        int GetXmlRcsByControlador( int argControlador);

        ////XMLCONTROL (Controles)
        //XmlData GetXmlControlesByEstablo(string argEstablo);
        //XmlData SetXmlControlesByEstablo(string argEstablo);
        //int DeleteXmlControlByEstablo(string argEstablo);


        // Este me valdría para todos los xmls ???????????????????????????????
        int UpdateXmlData(XmlData data);
    }
}