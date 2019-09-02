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
            if (result.value.Length > 0)
                result.success = false;
            else result.success = true;

            return result;
        }


        public Result GetXmlGanaderiasData(IXmlsDataAccessor Ixmlda,int argControlador)
        {
            iXmlDA = Ixmlda;
            result.value= iXmlDA.SetXmlGanaderiasByControlador(argControlador).ToString();
            if (Convert.ToInt32(result.value) >0)
                result.success = true;
            result.value = "Exportada Tabla de Ganaderias";

            return result;
        }
        public Result GetXmlControlesData(IXmlsDataAccessor Ixmlda, string argGanaderia,int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlControlesByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Controles";

            return result;
        }
        public Result GetXmlInseminacionesData(IXmlsDataAccessor Ixmlda, string argGanaderia, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlTorosByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Inseminaciones";

            return result;
        }
        public Result GetXmlNacimientosData(IXmlsDataAccessor Ixmlda, string argGanaderia, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlTorosByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Toros";

            return result;
        }
        public Result GetXmlActualizacionesGlobalData(IXmlsDataAccessor Ixmlda,  int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlActualizacionesGlobales(controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Actualizaciones globales";

            return result;
        }


       
        public  void InitializeServices()
        {
           
        }
       
    }
}
