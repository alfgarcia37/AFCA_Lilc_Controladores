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
        private IXmlDataAccessor iXmlDA;
        ILecturaControlService iSrvLecturasControl;
        //IGenerarService iSrvGenerar;
        ClsGanaderiasXML ObjDatosXML;
        public ClsGenerar()
        {
            InitializeServices();
        }
       
        public XmlData SetGanaderiasToXMLganaderias(int argControlador)
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
            return iXmlDA.SetXmlGanaderiasByControladorID(argControlador);

            //bool result = IGenerarService;

            //GANADERIAS
            //a) --lo ideal
            //var dataLecturascontrol(solo lectura) = iSrvGenerar.SetXmlGanaderiasData(controlador);//cargo de Lecturascontrol a XMLGanaderias
            ////dataset.Fill lo hace el Framework???????
            ////duda ... A mi no me hace falta un DataSet de LecturasControl. Me vale con un DataReader.????. En este caso lo que tengo que hacer es rellenar la
            ////tabla xmlDatos y después ir actualizando lo que considere (hago una select de las filas que me interese y "updateo" el XmlData("XmlDatos")
            //    //
            // foreach (DataRow pRow in dataLecturascontrol.Tables["LecturasControl"].Rows)
            //    pRow.Moroso=FuncMoroso(nod.ganaderia)
            // next 
            //Insert dataLecturascontrol into xmlGanaderias
            //b)--lo cuasi real (pseudocodigo)

            //ClsControl Objcontrol=new ClsControl ();
            //ObjDatosXML = new ClsGanaderiasXML();

            //var xmldata2 = iSrvLecturasControl.LoadLecturaControlData(controlador) ...cojo las ganaderias
            //for each nodo in xmldata2
            //var colection=Objcontrol.GetControles(xmldata2.Tables("LecturasControl").Fields("CodigoGanaderia"); ..cojo los controles de esa ganaderia
            //for each nodo in collection
            //    insert (nodo -> xml)
            //next
            //next


            //fer.... paso el data,lo mnipulo si fuera preciso y relleno el datatable DatosXML(ganaderias)


            //CONTROLES


            //...

            // Insert XML's INTO CLM_Down's


        }
        public  void InitializeServices()
        {
            
           
        }

            
       
    }
}
