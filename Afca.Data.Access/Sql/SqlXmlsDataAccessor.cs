using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Afca.Data.Access
{
    using FrameworkForge.Common;
    using FrameworkForge.Data;
    using Afca.Common;
    using Afca.Common.DataAccessor;
    using System;
    using System.Data;
  
    using System.Data.SqlClient;
    using Afca.Common.DataSets;

    public class SqlXmlsDataAccessor : SqlDataAccessor, IXmlsDataAccessor
    {
        //GANADERIAS
        public int SetXmlGanaderiasByControlador(int argControlador)
        {


             int regs = this.ExecuteNonQuery(
                                "sp_GanaderiasToXMLByControlador"
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs;
        }
        //CONTROLES
        public int SetXmlControlesByGanaderia(string argGanaderia)
        {


            int regs = this.ExecuteNonQuery(
                               "sp_ControlesToXMLByEstablo"
                               , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia)});
            return regs;
        }





        public XmlData GetXmlGanaderiasByID(int controlador)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                                , "sp_LecturaControl_select"
                                , CommandType.StoredProcedure
                                , null
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
//        public XmlData GetXmlControlesByEstablo(string argEstablo)
//        {
//            return (XmlData)this.ExecuteDataSet(new XmlData()
//                               , "sp_LecturaControl_selectByID"
//                               , CommandType.StoredProcedure
//                               , new SqlParameter[]{
//                                        new SqlParameter("@CodigoControlador",argEstablo)
//}
//                               , new string[] { "LecturaControl" });
//        }
//        public XmlData SetXmlControlesByEstablo(string argEstablo)
//        {
//            return (XmlData)this.ExecuteDataSet(new XmlData()
//                                , "sp_LecturaControl_selectByID"
//                                , CommandType.StoredProcedure
//                                , new SqlParameter[]{
//                                        new SqlParameter("@Establo",argEstablo)
//}
//                                , new string[] { "LecturaControl" });
//        }
//        public int DeleteXmlControlByEstablo(string argEstablo)
//        {
//            return this.ExecuteNonQuery(
//                                "sp_LecturaControl_deleteByID"
//                                , new SqlParameter[]{
//                                        new SqlParameter("@Establo",argEstablo)
//});
//        }

    }
}
