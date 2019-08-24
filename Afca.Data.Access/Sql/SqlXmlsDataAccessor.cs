using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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


    public class SqlXmlGanaderiasDataAccessor : SqlDataAccessor, IXmlDataAccessor
    {
        //GANADERIAS
        public XmlData GetXmlGanaderiasByID(int controlador)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                                , "sp_LecturaControl_select"
                                , CommandType.StoredProcedure
                                , null
                                , new string[] { "LecturaControl" });
        }
        public XmlData SetXmlGanaderiasByControladorID(int argControlador)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                                , "sp_Carga_desede_LecturasControl_a_XmlGanaderias_ByID"
                                , CommandType.StoredProcedure
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)
}
                                , new string[] { "LecturaControl" });
        }
        public int UpdateXmlData(XmlData data)
        {
            DataUpdateInfo updateInfo = new DataUpdateInfo(data);
            updateInfo.UpdateTable.Add(
                                new DataUpdateTableInfo("LecturaControl"
                                , "sp_LecturaControl_insert"
                                , "sp_LecturaControl_update"
                                , "sp_LecturaControl_delete"));
            return this.UpdateDataSet(updateInfo);
        }
        public int DeleteXmlGanaderiasByID(int argControlador)
        {
            return this.ExecuteNonQuery(
                                "sp_LecturaControl_deleteByID"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argControlador)
});
        }

        //CONTROLES
        public XmlData GetXmlControlesByEstablo(string argEstablo)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                               , "sp_LecturaControl_selectByID"
                               , CommandType.StoredProcedure
                               , new SqlParameter[]{
                                        new SqlParameter("@CodigoControlador",argEstablo)
}
                               , new string[] { "LecturaControl" });
        }
        public XmlData SetXmlControlesByEstablo(string argEstablo)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                                , "sp_LecturaControl_selectByID"
                                , CommandType.StoredProcedure
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argEstablo)
}
                                , new string[] { "LecturaControl" });
        }
        public int DeleteXmlControlByEstablo(string argEstablo)
        {
            return this.ExecuteNonQuery(
                                "sp_LecturaControl_deleteByID"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argEstablo)
});
        }

    }
}
