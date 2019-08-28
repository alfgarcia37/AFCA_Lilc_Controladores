using Afca.Common.ServicesInterfaces;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afca.Common;
using Afca.Common.DataSets;
using dO = FrameworkForge.Common.DataOperations;
using System.Collections;
using Afca.Common.DataAccessor;

namespace Afca.Services.Clases
{
    public class ClsGenerar
    {
         IXmlsDataAccessor iXmlDA;
        private Result result=new Result();
        ILecturaControlService iSrvLecturasControl;
        //IGenerarService iSrvGenerar;
        ClsGanaderiasXML ObjDatosXML;
        public ClsGenerar()
        {
            InitializeServices();
        }
        public Result SetXmlGanaderiasData(IXmlsDataAccessor Ixmlda,int argControlador)
        {
            iXmlDA = Ixmlda;
            result.value= iXmlDA.SetXmlGanaderiasByControlador(argControlador).ToString();
            if (Convert.ToInt32(result.value) >0)
                result.success = true;
            result.value = "Exportada Tabla de Ganaderias";

            return result;
        }

        public Result SetXmlControlesData(IXmlsDataAccessor Ixmlda, string argGanaderia,int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlControlesByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Controles";

            return result;
        }
        public Result SetXmlInseminacionesData(IXmlsDataAccessor Ixmlda, string argGanaderia, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlTorosByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Controles";

            return result;
        }
        public Result SetXmlTorosData(IXmlsDataAccessor Ixmlda, string argGanaderia, int controlador)
        {
            iXmlDA = Ixmlda;
            result.value = iXmlDA.SetXmlTorosByGanaderia(argGanaderia, controlador).ToString();
            if (Convert.ToInt32(result.value) > 0)
                result.success = true;
            result.value = "Exportada Tabla de Controles";

            return result;
        }



        public bool Modelo_de_Ejemplo(int argControlador)
        {//aqui pongo la lógica. Los servicios sólo me devuelven datos. No tienen porqué conocer la interrelación entre ellos.

            XmlData xmlData = new XmlData();

            LecturaControlData lecturaControlData = iSrvLecturasControl.LoadLecturaControlDataByID("");


            foreach (DataRow drLectura in lecturaControlData.LecturaControl.Rows)
            {

                DataRow xmlDatosRow = xmlData.XmlDatos.NewRow();

                xmlDatosRow["ID"] = drLectura["ID"];
                DateTime fecha= (DateTime) dO.cls(drLectura, "FechaPrevista", DateTime.Now.AddMonths(1));
                dO.EsNulo(drLectura, "FechaPrevista");
                dO.GetMaximo(drLectura, new string[] { "LecturaAnterior", "LecturaActual" });
                dO.HasRows(lecturaControlData, "LecturaControl");

                ArrayList exceptions = new ArrayList();
                dO.ImportValues(drLectura, xmlDatosRow, exceptions);
                
                //..... CLsControl.GenrarRegis

                xmlData.XmlDatos.Rows.Add(xmlDatosRow);
            }
            return true;
            //return iXmlDA.SetXmlGanaderiasByControladorID(argControlador);

            //bool result = IGenerarService;

           



        }
        public  void InitializeServices()
        {
            
           
        }

            
       
    }
}
