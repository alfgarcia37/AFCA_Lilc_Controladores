namespace Afca.Data.Access
{
    using Afca.Common.DataAccessor;
    using Afca.Common.DataSets;
    using FrameworkForge.Common;
    using FrameworkForge.Data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

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
        public int SetXmlControlesByGanaderia(string argGanaderia,int argControlador )
        {


            int regs = this.ExecuteNonQuery(
                               "sp_ControlesToXMLByEstablo"
                               , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }
        public int SetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                               "sp_InseminacionesToXMLByEstablo"
                               , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }
        public int SetXmlTorosByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                               "sp_TorosToXMLByEstablo"
                               , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
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

       

    }
}
