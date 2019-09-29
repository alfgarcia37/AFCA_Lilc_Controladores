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
        //SET XML's
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
        public int SetXmlNacimientosByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                               "sp_NacimientosToXMLByEstablo"
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
        public int PasarXmlToUPData(int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                               "SP_PasarXMLToUP"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs;
        }
        //GET XML's.=========================================================================================================
        public int PasarDownToXmlData(int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                               "SP_PasarDownToXML"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs;
        }
        public XmlData LoadXMLDatosDataByControlador(int argControlador)
        {
            return (XmlData)this.ExecuteDataSet(new XmlData()
                                , "[sp_XMLDATOS_selectByControlador]"
                                , CommandType.StoredProcedure
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador) }
                                , new string[] { "XMLDatos" });
        }
        public string CheckEstablosDuplicadosData(int argControlador)//establos ya leidos //new SqlParameter("@FPrevista",argFPrevista)});
        {
            var regs = this.ExecuteScalar(
                               "SP_CheckDuplicadosXMLDatos"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador) });
            
                return (regs == null ? "" : regs.ToString());
        }
        public string CheckControlesDuplicadosData(int argControlador)
        {


            var regs = this.ExecuteScalar(
                               "[SP_CheckDuplicadosXMLControl]"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return (regs == null ? "" : regs.ToString());
        }
        public string CheckPartosDuplicadosData(int argControlador)
        {


            var regs = this.ExecuteScalar(
                               "[SP_CheckDuplicadosXMLPartos]"
                               , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return (regs == null ? "" : regs.ToString());
        }
        //Una vez realizo los Check... -> Inserto las tablas de XML a ControlLechero
        public int GetXmlGanaderiasByControlador(int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_GanaderiasFromXMLByControlador"
                                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return regs;
        }
        public int GetXmlControlesByGanaderia(string argGanaderia, int argControlador,DateTime argFPrevista,string argTipocontrol)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_ControlesFromXMLByEstablo"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador),
                                        new SqlParameter("@FPrevista",argFPrevista),
                                        new SqlParameter("@TipoControl",argTipocontrol)
                                        });
            return regs;
        }
        public int GetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_InseminacionesFromXMLByEstablo"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }
        public int GetXmlNacimientosByGanaderia(string argGanaderia, int argControlador)
        {


            int regs = this.ExecuteNonQuery(
                                "sp_NacimientosFromXMLByEstablo"
                                , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                        });
            return regs;
        }
        public int GetXmlRcsByControlador(int argControlador)
        {
            int regs = this.ExecuteNonQuery(
                                "sp_RcsFromXMLByControlador"
                                , new SqlParameter[]{
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
