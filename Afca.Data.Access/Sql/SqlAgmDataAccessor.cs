using Afca.Common.DataAccessor;
using Afca.Common.DataSets;
using FrameworkForge.Data;
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
        public void PrepareAgmEnviosPrincipales()
        {
            this.ExecuteNonQuery(CommandType.StoredProcedure, "sp_GDM_CL_Controles");
        }
        public void PrepareAgmEnviosRecibos(string remesaID)
        {
            this.ExecuteNonQuery( 
                "sp_GDM_CL_Recibos"
                ,new SqlParameter[] { new SqlParameter("@RemesaID",remesaID)});

        }
        public void PrepareAgmEnviosRCS()
        {
            this.ExecuteNonQuery(
               "sp_GDM_CL_RCS"
               , null);
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
            DataSet data = new AgmEnvioData();
            data.EnforceConstraints = false;

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

    }
}
