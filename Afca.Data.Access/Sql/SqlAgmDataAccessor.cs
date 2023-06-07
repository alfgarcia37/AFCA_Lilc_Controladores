using Afca.Common;
using Afca.Common.DataAccessor;
using Afca.Common.DataSets;
using Afca.Common.Models;
using FrameworkForge.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Data.Access
{
    public class SqlAgmDataAccessor : SqlDataAccessor, IAgmDataAccessor
    {

        // fon 03/05/2023
        // Ahora este SP es el maestro. Se le llama todos los dias para pasar los datos (Controles,Lactaciones,partos...)
        // desde CL y Reg a GandmTemp
        // Posteriormente cargaremos desde Temp A Azure
        // Hay que deshabilitar (no Los borres) los SP de abajo :
        // sp_GDM_CL_RCS... SP_GDM_REG_Calificacion.. etc...
        // Hay posibilidad de crear un seleccionador en pantalla para poner a 1 ó 0 los sqlParameters (@controles,@lactaciones......)?
        public void PrepareAgmEnviosPrincipales()
        {
            SqlParameter[] parameters = new SqlParameter[12];

            parameters[0] = new SqlParameter("@controles",SqlDbType.Bit);
            parameters[0].Value = 0;
            parameters[1] = new SqlParameter("@lactaciones", SqlDbType.Bit);
            parameters[1].Value = 0;
            parameters[2] = new SqlParameter("@inseminaciones", SqlDbType.Bit);
            parameters[2].Value = 0;

            parameters[3] = new SqlParameter("@reproductivo", SqlDbType.Bit);
            parameters[3].Value = 0;
            parameters[4] = new SqlParameter("@partos", SqlDbType.Bit);
            parameters[4].Value = 0;
            parameters[5] = new SqlParameter("@rcs", SqlDbType.Bit);
            parameters[5].Value = 0;

            parameters[6] = new SqlParameter("@censo", SqlDbType.Bit);
            parameters[6].Value = 1;
            parameters[7] = new SqlParameter("@genomica", SqlDbType.Bit);
            parameters[7].Value = 0;
            parameters[8] = new SqlParameter("@calificacion", SqlDbType.Bit);
            parameters[8].Value = 0;

            parameters[9] = new SqlParameter("@ico", SqlDbType.Bit);
            parameters[9].Value = 0;
            parameters[10] = new SqlParameter("@recesivos", SqlDbType.Bit);
            parameters[10].Value = 0;
            parameters[11] = new SqlParameter("@encuestas", SqlDbType.Bit);
            parameters[11].Value = 0;
            //this.ChangeDataBase("ControlLechero");
            this.ExecuteNonQuery("SP_GDM_CL_Controles", parameters);
           // this.ExecuteNonQuery(CommandType.StoredProcedure, "sp_GDM_CL_Controles", parameters);
        }
        public void PrepareAgmEnviosRecibos(string remesaID)
        {
           // this.ExecuteNonQuery("sp_GDM_CL_Recibos",new SqlParameter[] { new SqlParameter("@RemesaID",remesaID)});

        }
        public void PrepareAgmEnviosRCS()
        {
         //   this.ExecuteNonQuery("sp_GDM_CL_RCS", null);
        }
        public void PrepareAgmEnviosCalificaciones(string codigoGanaderia)
        {
            //this.ChangeDataBase("reggen");
            //this.ExecuteNonQuery("SP_GDM_REG_Calificacion", new SqlParameter[1] {new SqlParameter("@ESTABLO",codigoGanaderia) });
        }
        public void PrepareAgmEnviosCenso()
        {
            //this.ChangeDataBase("reggen");
            //this.ExecuteNonQuery("SP_GDM_REG_Censo", null);
        }
        public void PreparaAgmEnviosGenomica()
        {
            //this.ChangeDataBase("reggen");
            //this.ExecuteNonQuery("SP_GDM_REG_Genomica", null);
        }
        public void PreparaAgmEnviosLactacionesHistorico()
        {
            //this.ChangeDataBase("reggen");
            //this.ExecuteNonQuery("SP_GDM_REG_LactacionesHistorico", null);
        }
        public AgmEnvioData GetAgmEnviosData()
        {
            this.ChangeDataBase("gandmTemp");
           
            return (AgmEnvioData)this.ExecuteDataSet(new AgmEnvioData()
                                 , "sp_AgmEnvios_select"
                                 , CommandType.StoredProcedure
                                 , null
                                 , new string[] { "Ganaderia" });
        }
        public AgmEnvioData GetAgmEnviosSecundariosData(string entidad)
        {
            this.ChangeDataBase("gandmTemp");
            return (AgmEnvioData)this.ExecuteDataSet(new AgmEnvioData()
                                 , "sp_AgmEnvios_select"
                                 , CommandType.StoredProcedure
                                 , new SqlParameter[1]
                                 {
                                     new SqlParameter("@Entidades",entidad)
                                 }
                                 , new string[] { "Ganaderia" });
        }

        public AgmEnvioData GetEnvioMessageData(string ganaderiaID, ArrayList entidadesIncluded)
        {
            this.ChangeDataBase("gandmTemp");
            DataSet data = new AgmEnvioData();
            data.EnforceConstraints = false;

            string entidades = "";

            foreach (string entidad in entidadesIncluded)
                entidades = string.Format("{0}{1}{2}"
                    ,entidades
                    ,entidades.Length>0?";":""
                    ,entidad);

            return (AgmEnvioData)this.ExecuteDataSet(data
                                 , "sp_AgmEnvioMessage_select"
                                 , CommandType.StoredProcedure
                                 , new SqlParameter[2]
                                 {
                                     new SqlParameter("@GanaderiaID",ganaderiaID),
                                      new SqlParameter("@Entidades",entidades)
                                 }
                                 , new string[] { "MessageToSend" });
        }

        public int MarkAgmEnviosAsSended(string ganaderiaID, ArrayList entidadesIncluded)
        {
            this.ChangeDataBase("gandmTemp");
           
            string entidades = "";

            foreach (string entidad in entidadesIncluded)
                entidades = string.Format("{0}{1}{2}"
                    , entidades
                    , entidades.Length > 0 ? ";" : ""
                    , entidad);

            return this.ExecuteNonQuery("sp_AgmEnvios_state_update"
                                 , new SqlParameter[3]
                                 {
                                      new SqlParameter("@GanaderiaID",ganaderiaID),
                                      new SqlParameter("@StatusBuzon","UP"),
                                      new SqlParameter("@Entidades",entidades)
                                 });
        }

        public int MarkAgmEnvioAsNotSended(string ganaderiaID,string entidad,string error)
        {
            this.ChangeDataBase("gandmTemp");


            return this.ExecuteNonQuery("sp_AgmEnvios_state_update"
                                 , new SqlParameter[4]
                                 {
                                      new SqlParameter("@GanaderiaID",ganaderiaID),
                                      new SqlParameter("@StatusBuzon","ER"),
                                      new SqlParameter("@Entidades",entidad),
                                      new SqlParameter("@Mensaje",error)
                                 });
        }

        public void RecalculaIndicadores()
        {
           

        }

        public int SaveDowloadedData(string entidad, DateTime fechaDescarga, string message)
        {
            //Guardo de AZ a GandnTemp
            this.ChangeDataBase("gandmTemp");
            return this.ExecuteNonQuery("sp_AgmSaveDownloadedData", new SqlParameter[3]
                                 {
                                     new SqlParameter("@Entidad",entidad),
                                     new SqlParameter("@FechaDescarga",fechaDescarga),
                                      new SqlParameter("@jsonData",message)
                                 });
        }
        public AgmDownloadData GetDownloadedData(string entidad=null,bool includeMessage=false)
        {
            // Get registros descargados 'PDTE'  && FechaIncorporacion ==null 
            // Subiré dichos registros para notificar a la Api que están incorporados
            // Pondré el campo FechaIncorporación con GetDate()
            this.ChangeDataBase("gandmTemp");

            DataSet data = new AgmDownloadData();

            SqlParameter param1 = new SqlParameter("@Entidad",SqlDbType.Char);
            param1.Value = entidad;
            SqlParameter param2 = new SqlParameter("@IncludeMessage",SqlDbType.Bit);
            param2.Value = includeMessage?1:0;

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = param1;
            parameters[1] = param2;

            var datos= (AgmDownloadData)this.ExecuteDataSet(data
                , "sp_AgmGetDownloadedData"
                , CommandType.StoredProcedure
                , parameters
                , new string[] { "EntidadData" });



            return datos;
        }


        public Task<Result> InsertDownloadedData()
        {
            // inserto los registros descargados 'PDTE' en CL,Reggen y los marco como 'DWN'
            this.ChangeDataBase("gandmTemp");
            //ExecuteScalar: Ejecuta la consulta y devuelve la primera columna de la primera fila
            //del conjunto de resultados devueltos por la consulta. Las demás columnas o filas no se tienen en cuenta.
            //var res = this.ExecuteScalar( CommandType.StoredProcedure, "sp_AgmInsertDownloadedData");
           var res = this.ExecuteNonQuery(CommandType.StoredProcedure, "sp_AgmInsertDownloadedData");
            return Task.FromResult(new Result
            {
                success = true,
                value = res.ToString()
            });
        }
    }
   
}
