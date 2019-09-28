using Afca.Common.ServicesInterfaces;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afca.Common;
using Afca.Common.DataSets;
using dO = FrameworkForge.Common.DataOperations;
using System.Collections;
using System;
using Afca.Common.DataAccessor;

namespace Afca.Services.Clases
{
    public class ClsImportar
    {
        IXmlsDataAccessor iXmlDA;
        private Result result=new Result();
        ILecturaControlService iSrvLecturasControl;
        //IGenerarService iSrvGenerar;
        ClsGanaderiasXML ObjDatosXML;
        public ClsImportar()
        {
            InitializeServices();
        }
        
        public Result GetXmlCheckGanaderiasData(IXmlsDataAccessor Ixmlda, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.CheckEstablosDuplicadosData(controlador);
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }
        public Result GetXmlCheckControlesData(IXmlsDataAccessor Ixmlda, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.CheckControlesDuplicadosData(controlador);
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }
        public Result GetXmlCheckPartosData(IXmlsDataAccessor Ixmlda, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.CheckPartosDuplicadosData(controlador);
            if (result.value.ToString()!= "0")
                result.success = false;
            else result.success = true;

            return result;
        }


        public Result GetXmlGanaderiasData(IXmlsDataAccessor Ixmlda,int argControlador)
        {
            iXmlDA = Ixmlda;
            result.value= iXmlDA.GetXmlGanaderiasByControlador(argControlador).ToString();
            if (Convert.ToInt32(result.value) >0)
                result.success = true;
            result.value = "Importada Tabla de Ganaderias";

            return result;
        }
        public Result GetXmlControlesData(IXmlsDataAccessor Ixmlda, string argGanaderia,int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.GetXmlControlesByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Importada  Tabla de Controles";

            return result;
        }
        public Result GetXmlInseminacionesData(IXmlsDataAccessor Ixmlda, string argGanaderia, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.GetXmlInseminacionesByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Inseminaciones";

            return result;
        }
        public Result GetXmlNacimientosData(IXmlsDataAccessor Ixmlda, string argGanaderia, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.GetXmlNacimientosByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "IMportados Tabla de Nacimientos";

            return result;
        }
        public Result GetXmlRcsData(IXmlsDataAccessor Ixmlda,  int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.GetXmlRcsByControlador( controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de RCS";

            return result;
        }
        //public Result GetXmlActualizacionesGlobalData(IXmlsDataAccessor Ixmlda,  int controlador)
        //{
        //    iXmlDA = Ixmlda;
        //    result.value = iXmlDA.SetXmlActualizacionesGlobales(controlador).ToString();
        //    if (Convert.ToInt32(result.value) > 0)
        //        result.success = true;
        //    result.value = "Actualizaciones globales";

        //    return result;
        //}

        public Result PasarDownToXml(IXmlsDataAccessor Ixmlda, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.PasarDownToXmlData(controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "pasar de Down a XML";

            return result;
        }

        public  void InitializeServices()
        {
           
        }
       
    }
}
