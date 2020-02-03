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
        public SqlXmlsDataAccessor() :base()
        { }
        public string CheckToXMLData(string argGanaderia)
        {


            var regs = this.ExecuteScalar(
                               "sp_CheckToXML"
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

            try
            {
                int regs = this.ExecuteNonQuery(
                                   "sp_ActualizacionesglobalesToXMLBycontrolador"
                                   , new SqlParameter[]{

                                        new SqlParameter("@controlador",argControlador)
                                            });
                return regs;
            }
            catch(System.Exception e)
            {
                throw e;
            }
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
        public DataSet PasarDownToXmlData_(int argControlador)
        {
            DataSet data = new LecturaControlData();
            data.EnforceConstraints = false;

            return this.ExecuteDataSet(data, "SP_PasarDownToXML", CommandType.StoredProcedure
                , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)}, new string[] { "Incidencia" });

            //int regs = this.ExecuteNonQuery(
            //                   "SP_PasarDownToXML"
            //                   , new SqlParameter[]{
            //                            new SqlParameter("@Controlador",argControlador)});
            //return regs;
        }
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


        public string CheckFromControlXmlDuplicados(int argControlador)//establos ya leidos //new SqlParameter("@FPrevista",argFPrevista)});
        {
            try
            {
                string establo =(string) this.ExecuteScalar(
                                 "SP_CheckFromControlXmlDuplicados"
                                 , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)});
            return establo;
            }
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en SP_CheckFromControlXmlDuplicados",
                          e.Message);
                return "0";
            }


        }
        public DataSet CheckFromXMLData(int argControlador)//establos ya leidos //new SqlParameter("@FPrevista",argFPrevista)});
        {
            DataSet data = new LecturaControlData();
            data.EnforceConstraints = false;
            try
            {
                return this.ExecuteDataSet(data, "SP_CheckFromXML", CommandType.StoredProcedure
                    , new SqlParameter[]{
                                        new SqlParameter("@Controlador",argControlador)}, new string[] { "Incidencia" });
            }
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en SP_CheckFromXML",
                          e.Message);
                return data;
            }
        }
        //Una vez realizo los Check... -> Inserto las tablas de XML a ControlLechero
        public int GetXmlGanaderiasByEstablo(string argEstablo)
        {
            try
            {
                int regs = this.ExecuteNonQuery(
                                    "sp_GanaderiasFromXMLByEstablo"
                                    , new SqlParameter[]{
                                        new SqlParameter("@strEstablo",argEstablo)});
                return regs;
            }
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en sp_GanaderiasFromXMLByEstablo",
                          e.Message);
                return 0;
            }
        }
        public int GetXmlControlesByGanaderia(string argGanaderia, int argControlador,DateTime argFPrevista,string argTipocontrol)
        {
            try
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
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en sp_ControlesFromXMLByEstablo",
                          e.Message);
                return 0;
            }
        }
        public int GetXmlInseminacionesByGanaderia(string argGanaderia, int argControlador)
        {

            try
            {
                int regs = this.ExecuteNonQuery(
                                    "sp_InseminacionesFromXMLByEstablo"
                                    , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                            });
                return regs;
            }
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en sp_InseminacionesFromXMLByEstablo",
                          e.Message);
                return 0;
            }
        }
        public int GetXmlNacimientosByGanaderia(string argGanaderia, int argControlador)
        {
            try
            {

                int regs = this.ExecuteNonQuery(
                                    "sp_NacimientosFromXMLByEstablo"
                                    , new SqlParameter[]{
                                        new SqlParameter("@Establo",argGanaderia),
                                        new SqlParameter("@controlador",argControlador)
                                            });
                return regs;
            }
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en sp_NacimientosFromXMLByEstablo",
                          e.Message);
                return 0;
            }
        }
        public int GetXmlRcsByControlador(int argControlador)
        {
            try
            {
                int regs = this.ExecuteNonQuery(
                                    "sp_RcsFromXMLByControlador"
                                    , new SqlParameter[]{
                                        new SqlParameter("@controlador",argControlador)
                                            });
                return regs;
            }
            catch (System.Exception e)
            {

                DialogsManager.ShowErrorDialog("Error en sp_RcsFromXMLByControlador",
                          e.Message);
                return 0;
            }
           
        }
        public int GetXmlActualizacionesGlobales(int argControlador)
        {
            try
            {
                int regs = this.ExecuteNonQuery(
                               "sp_ActualizacionesglobalesFromXMLBycontrolador"
                               , new SqlParameter[]{

                                        new SqlParameter("@controlador",argControlador)
                                        });
                return regs;
            }
            catch (System.Exception e)
            {
                
              DialogsManager.ShowErrorDialog("Error en sp_ActualizacionesglobalesFromXMLBycontrolador",
                        e.Message);
                return 0;
                    
                
            }
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
