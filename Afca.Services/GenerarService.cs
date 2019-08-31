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
        private Result resultToros;
        private Result resultInseminaciones;

        private ClsGenerar ObjGenerar = new ClsGenerar();
        private IXmlsDataAccessor iXmlDA;
        private ILecturaControlDataAccessor ilecturasDA;
        private LecturaControlData lecturaControlData;
       public GenerarService()
        {
            iXmlDA = Afca.Common.AfcaDataAccessorProvider.GetXmlDataAccessor();
            ilecturasDA = Afca.Common.AfcaDataAccessorProvider.GetLecturaControlDataAccessor();
            //lo pongo aqui porque lo suo en 2 sitios
            
        }

        public Result CheckXmlMaestroControlesData(int argControlador)
        {
            LecturaControlData lecturaControlData = ilecturasDA.LoadLecturaControlDataByControladorEstado(argControlador, EstadoLectura.Planificar);
            Result result=null ;
            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {
                result = SetXmlCheckControlesData(drLectura["Establo"].ToString());
            }
            return result;
         }

         public Result GenerarXMLs(int argControlador)

        {
          
            //Recorro GANADERIAS para Generar Tablas
            LecturaControlData lecturaControlData = ilecturasDA.LoadLecturaControlDataByControladorEstado(argControlador, EstadoLectura.Planificar);

            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {
                //GANADERIAS.... 
                resultGanaderias = SetXmlGanaderiasData(argControlador);
                if (resultGanaderias.value == "0") { resultGanaderias.success = false; break; }
                //CONTROLES
                resultControles = SetXmlControlesData(drLectura["Establo"].ToString(), argControlador);
                if (resultControles.value == "0") { resultControles.success = false; break; }
                //INSEMINACIONES
                resultControles = SetXmlInseminacionesData(drLectura["Establo"].ToString(), argControlador);
                if (resultInseminaciones.value == "0") { resultInseminaciones.success = false; break; }
                //TOROS y REGISTRADAS
                resultToros = SetXmlTorosData(drLectura["Establo"].ToString(), argControlador);
                if (resultToros.value == "0") { resultToros.success = false; break; }

                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
                //REGISTRADAS
                //ObjGenerar.SetGanaderiasToXmlGanaderias(argControlador);
            }
            //TABLAS GLOBAL (Independiente del Establo)
            resultControles = SetXmlActualizacionesGlobalData(argControlador);



            return resultControles;
        }

        public Result SetXmlCheckControlesData(string argControlador)
        {
            return ObjGenerar.SetXmlCheckMaestroControlesData(iXmlDA, argControlador);
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

