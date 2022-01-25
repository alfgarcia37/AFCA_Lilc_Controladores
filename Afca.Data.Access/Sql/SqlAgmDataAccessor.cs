using Afca.Common.DataAccessor;
using Afca.Common.DataSets;
using FrameworkForge.Data;
using System;
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
        public AgmEnvioData GetAgmEnviosPrincipalesData()
        {
            this.ChangeDataBase("gandmTemp");
            return (AgmEnvioData)this.ExecuteDataSet(new AgmEnvioData()
                                 , "sp_AgmEnvios_select"
                                 , CommandType.StoredProcedure
                                 , new SqlParameter[]
                                 {
                                     new SqlParameter("@Tipo","P")
                                 }
                                 , new string[] { "Ganaderia" });
        }

        public AgmEnvioData GetAgmEnviosSecundariosData()
        {
            this.ChangeDataBase("gandmTemp");
            return (AgmEnvioData)this.ExecuteDataSet(new AgmEnvioData()
                                 , "sp_AgmEnvios_select"
                                 , CommandType.StoredProcedure
                                 , new SqlParameter[]
                                 {
                                     new SqlParameter("@Tipo","S")
                                 }
                                 , new string[] { "Ganaderia" });
        }
        public void PrepareAgmEnviosPrincipales()
        {           
            this.ExecuteNonQuery(CommandType.StoredProcedure, "sp_GDM_CL_Controles");
        }

        public void PrepareAgmEnviosSecundarios()
        {
            this.ChangeDataBase("gandmTemp");
            throw new NotImplementedException();
        }
    }
}
