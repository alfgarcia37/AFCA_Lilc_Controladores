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
        //GET XML's
        public string CheckMaestroControlesData(string argGanaderia)
        {


            var regs = this.ExecuteScalar(
                               "sp_CheckMaestroControlesToXML"
                               , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia)});
            return regs.ToString();
        }
       
        public int SetXmlGanaderiasByControlador(int argControlador)
        {


             int regs = this.ExecuteNonQuery(
                                "sp_GanaderiasToXMLByControlador"
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs;
        }
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
        public int SetXmlActualizacionesGlobales( int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                               "sp_ActualizacionesglobalesToXMLBycontrolador"
                               , new SqlParameter[]{
                                        
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }

        //SET XML's.=========================================================================================================
        public XmlData LoadXMLDatosDataByControlador(int argControlador)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                                , "sp_LecturaControl_selectByID"
                                , CommandType.StoredProcedure
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador) }
                                , new string[] { "XMLDatos" });
        }
        public string CheckEstablosDuplicadosData(int argControlador)//establos ya leidos //new SqlParameter("@FPrevista",argFPrevista)});
        {
            var regs = this.ExecuteScalar(
                               "[SP_CheckDuplicadosXMLDatos]"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador) });
                                       
            return regs.ToString();
        }
        public string CheckControlesDuplicadosData(int argControlador)
        {


            var regs = this.ExecuteScalar(
                               "[SP_CheckDuplicadosXMLControl]"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs.ToString();
        }
        public string CheckPartosDuplicadosData(int argControlador)
        {


            var regs = this.ExecuteScalar(
                               "sp_GanaderiasToXMLByControlador"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs.ToString();
        }

        //Una vez realizo los Check... -> Inserto las tablas de XML a ControlLechero

        public int GetXmlGanaderiasByControlador(int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_GanaderiasToXMLByControlador"
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs;
        }
        public int GetXmlControlesByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_ControlesToXMLByEstablo"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }
        public int GetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_InseminacionesToXMLByEstablo"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }
       
        //public int GetXmlActualizacionesGlobales(int argControlador)
        //{


        //    int regs = this.ExecuteNonQuery(
        //                       "sp_ActualizacionesglobalesToXMLBycontrolador"
        //                       , new SqlParameter[]{

        //                                new SqlParameter("@controlador",argControlador)
        //                                });
        //    return regs;
        //}

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
