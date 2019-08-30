using System.Data;
using Afca.Common.DataAccessor;
using Afca.Common.ServicesInterfaces;
using Afca.Common.DataSets;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using Afca.Common;
using Afca.Services.Clases;
using dO = FrameworkForge.Common.DataOperations;

namespace Afca.Services
{

    public class GenerarService : IGenerarService
    {
        private Result resultGanaderias;
        private Result resultControles;

        private ClsGenerar ObjGenerar = new ClsGenerar();
        private IXmlsDataAccessor iXmlDA;
        private ILecturaControlDataAccessor ilecturasDA;
       public GenerarService()
        {
            iXmlDA = Afca.Common.AfcaDataAccessorProvider.GetXmlDataAccessor();
            ilecturasDA = Afca.Common.AfcaDataAccessorProvider.GetLecturaControlDataAccessor();
        }
        public bool GenerarXMLs(int argControlador)

        {
            //GANADERIAS.... Podría poner directamente las func de más abajo pero prefiero así hasta que haga todo en procdtos.
            resultGanaderias= SetXmlGanaderiasData(argControlador);
            //Recorro GANADERIAS para Generar Tablas
            LecturaControlData lecturaControlData = ilecturasDA.LoadLecturaControlDataByControladorEstado(argControlador, EstadoLectura.Planificar.ToString());
            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {
                //CONTROLES
                resultControles = SetXmlControlesData(drLectura["Establo"].ToString(), argControlador);
                if (resultControles.value == "0") resultControles.success = false;
                //INSEMINACIONES
                resultControles = SetXmlInseminacionesData(drLectura["Establo"].ToString(), argControlador);
                if (resultControles.value == "0") resultControles.success = false;
                //TOROS y REGISTRADAS
                resultControles = SetXmlTorosData(drLectura["Establo"].ToString(), argControlador);
                if (resultControles.value == "0") resultControles.success = false;
                
                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
                //REGISTRADAS
                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
            }
            //TABLAS GLOBAL (Independiente del Establo)
            resultControles = SetXmlActualizacionesGlobalData(argControlador);



            return true;
        }
        public Result SetXmlGanaderiasData(int argControlador)
        {
            return ObjGenerar.SetXmlGanaderiasData( iXmlDA, argControlador);
        }
        public Result SetXmlControlesData(string argGanaderia,int argcontrolador)
        {
            return ObjGenerar.SetXmlControlesData(iXmlDA, argGanaderia, argcontrolador);
        }
        public Result SetXmlInseminacionesData(string argGanaderia, int argcontrolador)
        {
            return ObjGenerar.SetXmlInseminacionesData(iXmlDA, argGanaderia, argcontrolador);
        }
        public Result SetXmlTorosData(string argGanaderia, int argcontrolador)
        {
            return ObjGenerar.SetXmlTorosData(iXmlDA, argGanaderia, argcontrolador);
        }
        public Result SetXmlActualizacionesGlobalData( int argcontrolador)
        {
            return ObjGenerar.SetXmlActualizacionesGlobalData(iXmlDA,  argcontrolador);
        }



    }
}

