using System.Data;
using Afca.Common.DataAccessor;
using Afca.Common.ServicesInterfaces;
using Afca.Common.DataSets;
using FrameworkForge.Common;
using FrameworkForge.Common.DataStructures;
using Afca.Common;
using System;
using Afca.Services.Clases;

using dO = FrameworkForge.Common.DataOperations;

namespace Afca.Services
{

    public class ImportarService :IImportarService
    {
        private Result resultGanaderias;
        private Result resultControles;
        private Result resultInseminaciones;
        private Result resultNacimientos;
        private Result resultRcs;
        private Result result = new Result();

       // private ClsImportar ObjImportar = new ClsImportar();
        private IXmlsDataAccessor iXmlDA;
        //private ILecturaControlDataAccessor ilecturasDA;
        private IXmlsDataAccessor ixmlsDA;
        //private LecturaControlData lecturaControlData;
        private XmlData xmldatosData;
       public ImportarService()
        {
            iXmlDA = Afca.Common.AfcaDataAccessorProvider.GetXmlDataAccessor();
        }
        //..CHECKS........................................................................
        public Result CheckXmlEstablosDuplicadosData(int argControlador)
        {
            var result = GetXmlCheckGanaderiasData(argControlador);
            return result;
         }
        public Result CheckXmlControlesData(int argControlador)
         {
            var result = GetXmlCheckControlesData(argControlador);
            return result;
         }
        public Result CheckXmlPartosData(int argControlador)
        {
            var result = GetXmlCheckPartosData(argControlador);
            return result;
        }
        public Result ImportarXMLs(int argControlador)

        {
            //Result resul = new Result { success = true, value = "999" };
           // resultGanaderias = GetXmlCheckGanaderiasData(argControlador);

             //Recorro GANADERIAS para Generar Tablas
             XmlData xmldatosData = iXmlDA.LoadXMLDatosDataByControlador(argControlador);

            Result result = null;
            foreach (DataRow drGanaderias in xmldatosData.XMLDATOS.Rows)
            {
                //GANADERIAS.... 
                resultGanaderias = GetXmlGanaderiasData(argControlador);
                if (resultGanaderias.value == "0") { resultGanaderias.success = false; break; }
                ////CONTROLES
                resultControles = GetXmlControlesData(drGanaderias["GanaderiaCodigo"].ToString(), argControlador,Convert.ToDateTime( drGanaderias["FechaPrevista"]), drGanaderias["TipoControl"].ToString());
                if (resultControles.value == "0") { resultControles.success = false; break; }
                ////INSEMINACIONES
                resultInseminaciones = GetXmlInseminacionesData(drGanaderias["GanaderiaCodigo"].ToString(), argControlador);
                if (resultInseminaciones.value == "0") { resultInseminaciones.success = false; break; }
                ////NACIMIENTOS....
                resultNacimientos = GetXmlNacimientosData(drGanaderias["GanaderiaCodigo"].ToString(), argControlador);
                if (resultNacimientos.value == "0") { resultNacimientos.success = false; break; }

            }
            //TABLAS GLOBAL (Independiente del Establo)
            //resultControles = GetXmlActualizacionesGlobalData(argControlador);
            return resultControles;
        }

        #region Chequeos
        //...hago los chequeos....................
        public Result GetXmlCheckGanaderiasData(int argcontrolador)
        {
            //return ObjImportar.GetXmlCheckGanaderiasData(iXmlDA, argcontrolador);
            result.value = iXmlDA.CheckEstablosDuplicadosData(argcontrolador);
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }
        public Result GetXmlCheckControlesData(int argcontrolador)
        {
            //return ObjImportar.GetXmlCheckControlesData(iXmlDA, argcontrolador);
            result.value = iXmlDA.CheckControlesDuplicadosData(argcontrolador);
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }
        public Result GetXmlCheckPartosData(int argcontrolador)
        {
            // return ObjImportar.GetXmlCheckPartosData(iXmlDA, argcontrolador);
            result.value = iXmlDA.CheckPartosDuplicadosData(argcontrolador);
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }
        #endregion



        //...cuando paso los chequeos hago las iimportaciones desde XML a CL ...........
        //Una vez Chequeado.. -> Importo las tablas
        public Result GetXmlGanaderiasData(int argControlador)
        {
            //return ObjImportar.GetXmlGanaderiasData( iXmlDA, argControlador);
            result.value = iXmlDA.GetXmlGanaderiasByControlador(argControlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Importada Tabla de Ganaderias";
            return result;
        }
        public Result GetXmlControlesData(string argGanaderia,int argcontrolador,DateTime argFPrevista,string argTipoControl)
        {
            //return ObjImportar.GetXmlControlesData(iXmlDA, argGanaderia, argcontrolador, FPrevista, TipoControl);
            result.value = iXmlDA.GetXmlControlesByGanaderia(argGanaderia, argcontrolador, argFPrevista, argTipoControl).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Importada  Tabla de Controles";

            return result;
        }
        public Result GetXmlInseminacionesData(string argGanaderia, int argcontrolador)
        {
            //return ObjImportar.GetXmlInseminacionesData(iXmlDA, argGanaderia, argcontrolador);
            result.value = iXmlDA.GetXmlInseminacionesByGanaderia(argGanaderia, argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Inseminaciones";
            return result;
        }
        public Result GetXmlNacimientosData(string argGanaderia, int argcontrolador)
        {
            //return ObjImportar.GetXmlNacimientosData(iXmlDA, argGanaderia, argcontrolador);
            result.value = iXmlDA.GetXmlNacimientosByGanaderia(argGanaderia, argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "IMportados Tabla de Nacimientos";
            return result;
        }
        public Result GetXmlRcsData(int argcontrolador)
        {
            //return ObjImportar.GetXmlRcsData(iXmlDA, argcontrolador);
            result.value = iXmlDA.GetXmlRcsByControlador(argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de RCS";
            return result;
        }
        //public Result GetXmlActualizacionesGlobalData(int argcontrolador)
        //{
        //    return ObjImportar.GetXmlActualizacionesGlobalData(iXmlDA, argcontrolador);
        //}
        
        public Result PasarDownToXml(int argcontrolador)
        {
            //return ObjImportar.PasarDownToXml(iXmlDA, argcontrolador);
            result.value = iXmlDA.PasarDownToXmlData(argcontrolador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "pasar de Down a XML";

            return result;
        }
    }
}

