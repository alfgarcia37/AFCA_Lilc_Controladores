
namespace Afca.Data.Access
{
    using FrameworkForge.Common;
    using FrameworkForge.Data;
    using Afca.Common.DataSets;
    using Afca.Common.DataAccessor;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;


    public class SqlGanaderiaDataAccessor : SqlDataAccessor, IGanaderiaDataAccessor
    {

        public GanaderiaDomainData LoadGanaderiaDomainData()
        {
            ChangeDataBase("Afca.xps15.reggen");
            return (GanaderiaDomainData)this.ExecuteDataSet(new GanaderiaDomainData()
                    , "sp_GanaderiaDomain_select"
                    , CommandType.StoredProcedure
                    , null
                    , new string[] { "Ganaderia" });
        }
    }
}
