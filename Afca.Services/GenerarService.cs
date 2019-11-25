using System.Data;
using Afca.Common.DataAccessor;
using Afca.Common.ServicesInterfaces;
using Afca.Common.DataSets;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using Afca.Common;
using System;

using dO = FrameworkForge.Common.DataOperations;

namespace Afca.Services
{

    public class GenerarService : IGenerarService
    {
        private Result resultGanaderias;
        private Result resultControles;
        private Result resultToros;
        private Result resultInseminaciones;
        private Result resultNacimientos;
        private Result result = new Result();

        //private ClsGenerar ObjGenerar = new ClsGenerar();.... Lo he reconfigurado para evitar este paso redundante (pensabamos usar más clases auxiliares)
        private IXmlsDataAccessor iXmlDA;
        private ILecturaControlDataAccessor ilecturasDA;
        private LecturaControlData lecturaControlData;
       public GenerarService()
        {
            iXmlDA = Afca.Common.AfcaDataAccessorProvider.GetXmlDataAccessor();
            ilecturasDA = Afca.Common.AfcaDataAccessorProvider.GetLecturaControlDataAccessor();
            //lo pongo aqui porque lo suo en 2 sitios
            
        }
        //check...comprobaciones...............................
        public Result CheckToXmlData(int argControlador)
        {
            LecturaControlData lecturaControlData = ilecturasDA.LoadLecturaControlDataByControladorEstado(argControlador, EstadoLectura.Planificar);
            Result result=null ;
            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {
                result = SetXmlCheckData(drLectura["Establo"].ToString());
            }
            return result;
         }

         public Result GenerarXMLs(int argControlador)

        {
            //GANADERIAS.... 
            resultGanaderias = SetXmlGanaderiasData(argControlador);
            if (resultGanaderias.value == "0") { resultGanaderias.success = false;  }
            //Recorro GANADERIAS para Generar Tablas
            LecturaControlData lecturaControlData = ilecturasDA.LoadLecturaControlDataByControladorEstado(argControlador, EstadoLectura.Planificar);
            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {
                //CONTROLES
                resultControles = SetXmlControlesData(drLectura["Establo"].ToString(), argControlador);
                if (resultControles.value == "0") { resultControles.success = false; break; }
                //INSEMINACIONES
                resultInseminaciones = SetXmlInseminacionesData(drLectura["Establo"].ToString(), argControlador);
                if (resultInseminaciones.value == "0") { resultInseminaciones.success = false; break; }
                //TOROS 
                resultToros = SetXmlTorosData(drLectura["Establo"].ToString(), argControlador);
                if (resultToros.value == "0") { resultToros.success = false; break; }
                //NACIMIENTOS
                resultNacimientos = SetXmlNacimientosData(drLectura["Establo"].ToString(), argControlador);
                if (resultNacimientos.value == "0") { resultNacimientos.success = false; break; }
            }
            //TABLAS GLOBAL (Independiente del Establo)
            resultControles = SetXmlActualizacionesGlobalData(argControlador);
            return resultControles;
        }

        public Result SetXmlCheckData(string argGanaderia)
        {
            //return ObjGenerar.SetXmlCheckMaestroControlesData(iXmlDA, argControlador);
            result.value = iXmlDA.CheckToXMLData( argGanaderia).ToString();
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }
        //GENERACION..................
        public Result SetXmlGanaderiasData(int argControlador)
        {
            //return ObjGenerar.SetXmlGanaderiasData( iXmlDA, argControlador);
           
            result.value = iXmlDA.SetXmlGanaderiasByControlador(argControlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Ganaderias";
            return result;
        }
        public Result SetXmlControlesData(string argGanaderia,int argcontrolador)
        {
            //return ObjGenerar.SetXmlControlesData(iXmlDA, argGanaderia, argcontrolador);
            //iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlControlesByGanaderia(argGanaderia, argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Controles";

            return result;
        }
        public Result SetXmlInseminacionesData(string argGanaderia, int argcontrolador)
        {
            // return ObjGenerar.SetXmlInseminacionesData(iXmlDA, argGanaderia, argcontrolador);
            result.value = iXmlDA.SetXmlInseminacionesByGanaderia(argGanaderia, argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Inseminaciones";

            return result;
        }
        public Result SetXmlTorosData(string argGanaderia, int argcontrolador)
        {
            //return ObjGenerar.SetXmlTorosData(iXmlDA, argGanaderia, argcontrolador);
            result.value = iXmlDA.SetXmlTorosByGanaderia(argGanaderia, argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Toros";

            return result;
        }
        public Result SetXmlNacimientosData(string argGanaderia, int argcontrolador)
        {
            // return ObjGenerar.SetXmlNacimientosData(iXmlDA, argGanaderia, argcontrolador);
            result.value = iXmlDA.SetXmlNacimientosByGanaderia(argGanaderia, argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Nacimientos";
            return result;
        }
        public Result SetXmlActualizacionesGlobalData( int argcontrolador)
        {
            //return ObjGenerar.SetXmlActualizacionesGlobalData(iXmlDA,  argcontrolador);
            result.value = iXmlDA.SetXmlActualizacionesGlobales(argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Actualizaciones globales";

            return result;
        }
        public Result PasarXmlToUP(int argcontrolador)
        {
            // return ObjGenerar.PasarXmlToUP(iXmlDA, argcontrolador);
            result.value = iXmlDA.PasarXmlToUPData(argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "pasar de XML a UP";

            return result;
        }


    }
}

