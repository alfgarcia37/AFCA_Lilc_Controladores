using Afca.Common.DataAccessor;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
using dO=FrameworkForge.Common.DataOperations;
using FrameworkForge.Common.DataStructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Afca.Common;
using FrameworkForge.Common.Interfaces;
using Afca.Common.Models;

namespace Afca.Services
{
    public class AgmService : IAgmService
    {
        IAgmDataAccessor agmDA;
        IAgmWebApiDataAccessor agmWebAPI;

        public AgmService()
        {
            agmDA = Afca.Common.AfcaDataAccessorProvider.GetAgmDataAccessor();
        }
        void InitializeAgmWebApiDA()
        {
            if(agmWebAPI==null) 
                agmWebAPI = Afca.Common.AfcaDataAccessorProvider.GetAgmWebApiDataAccessor("IAgmWebApiDataAccessor");

        }

        #region Envios
        public void PrepareAgmEnvios(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmEnviosPrincipales":
                     agmDA.PrepareAgmEnviosPrincipales();
                    break;
                case "AgmEnviosLacHistorico":
                    agmDA.PreparaAgmEnviosLactacionesHistorico();
                    break;
            }
        }
        public void PrepareAgmRecibosEnvios(string remesaID)
        {
            agmDA.PrepareAgmEnviosRecibos(remesaID);
        }
        public void PrepareAgmCalificacionesEnvios(string ganaderiaCodigoSelected)
        {
            agmDA.PrepareAgmEnviosCalificaciones(ganaderiaCodigoSelected);
        }
        public AgmEnvioData GetAgmEnvioDataByParent(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmEnviosPrincipales":
                    return agmDA.GetAgmEnviosData();
                case "AgmEnviosRecibos":
                    return agmDA.GetAgmEnviosSecundariosData("Recibos");
                    case "AgmEnviosLacHistorico":
                    return agmDA.GetAgmEnviosSecundariosData("LactacionesHistorico");
                default:
                    return null;
            }
        }
        public AgmEnvioData GetAgmEnviosPrincipalesToSendData()
        {
            return getAgmEnviosToSendData();
        }
        public AgmEnvioData GetAgmEnviosToSendData(string entidad)
        {
            return getAgmEnviosToSendData(entidad);
        }
        AgmEnvioData getAgmEnviosToSendData(string entidad=null)
        {
            string[] entidades = entidad==null?
                new string[] { 
                "Controles", "Lecturas","Censo","Genomica",
                "Inseminaciones", "Lactaciones", "Calificaciones",
                "Parametros", "Partos","Rcs","Nacimientos","Recesivos","LaboratorioTanque","laboratorioMensual" }
                : new string[] { entidad};

            AgmEnvioData data = entidades.Length==1?
                agmDA.GetAgmEnviosSecundariosData(entidad)
                :agmDA.GetAgmEnviosData();

            foreach(DataRow gan in data.Ganaderia.Rows)
            {
                gan["Status"] = "PE";
                gan["Selected"] = true;

                foreach(string ent in entidades)
                {
                    if (!dO.EsNulo(gan, ent)
                            && Convert.ToInt32(gan[ent])>0)
                    {
                        DataRow detail = data.Detail.NewRow();
                        dO.ImportPrimaryKeysValues(gan, detail);
                        detail["Entidad"] = ent;
                        detail["Count"] = Convert.ToInt32(gan[ent]);
                        detail["Selected"] = true;
                        detail["Status"] = "PE";
                        data.Detail.Rows.Add(detail);
                    }
                }
            }
            return data;
        }
        public string GenerateJsonMessageEnvio(string ganaderiaID, ArrayList entidadesIncluded)
        {
            string jsonMessage = "";

            AgmEnvioData data = agmDA.GetEnvioMessageData(ganaderiaID, entidadesIncluded);

            foreach(DataRow entidadMsg in data.MessageToSend.Rows)
            {
                if (!dO.EsNulo(entidadMsg, "Message")
                        && entidadMsg["Message"].ToString().Length>2)
                {
                    string partialJson = entidadMsg["Message"].ToString();
                    //remove { at beginning
                    partialJson = partialJson.Remove(0, 1);
                    //remove } at end
                    partialJson = partialJson.Remove(partialJson.Length - 1, 1);
                    jsonMessage = string.Format("{0}{1}{2}"
                        ,jsonMessage
                        ,jsonMessage.Length>0?",":""
                        ,partialJson);
                }
            }

            if (jsonMessage.Length > 0)
                jsonMessage = string.Format("{0}{1}{2}","{",jsonMessage,"}");
            return jsonMessage;
        }
        public async Task<Result> SendJsonMessageEnvio(string ganaderiaID, string jsonMessage)
        {
            InitializeAgmWebApiDA();
            return await agmWebAPI.UploadData(ganaderiaID,jsonMessage).ConfigureAwait(false);
        }
        public async Task<Result> ExecuteIndicadores()
        {
            InitializeAgmWebApiDA();
            return await agmWebAPI.ExecuteIndicadores().ConfigureAwait(false);
        }

        public bool MarkAgmEnviosAsSended(string ganaderiaID, ArrayList entidadesIncluded,out string message)
        {
            message = "";
            try {
                agmDA.MarkAgmEnviosAsSended(ganaderiaID, entidadesIncluded);
                return true;
            }
            catch(System.Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        public bool MarkAgmEnvioAsNotSended(string ganaderiaID,string entidad,string error,out string message)
        {
            message = "";
            try
            {
                agmDA.MarkAgmEnvioAsNotSended(ganaderiaID,entidad, error);
                return true;
            }
            catch (System.Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        #endregion

        #region Descargas
        public AgmDownloadData GetAgmDescargaDataByParent(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmDescargasPrincipales":
                    return agmDA.GetDownloadedData();
                default:
                    return null;
            }
        }
        public async Task<AgmDownloadData> DownloadAgmPrincipalData(string entidad)
        {
            InitializeAgmWebApiDA();
            return await agmWebAPI.DownloadAgmData(entidad);
        }
        public  Task<Result> SaveJsonDownloadedData(string entidad,DateTime fechaDescarga, string message)
        {
            try
            {
                int rowsSaved = agmDA.SaveDowloadedData(entidad, fechaDescarga, message);
                return Task.FromResult(new Result
                {
                    success=true
                    ,value=rowsSaved.ToString()
                });
            }
            catch(System.Exception e)
            {
                return Task.FromResult(new Result{
                    success = false
                    , value = e.Message 
                });
            }
        }
        public async Task<Result> NotifyDescargaAsRecieved(string entidad=null)
        {
            try
            {
                //cojo los descargados en GandmTemp que no están confirmados en AZ
                IAgmDataAccessor agmDataAccessor = AfcaDataAccessorProvider.GetAgmDataAccessor();
                AgmDownloadData downloadedData =agmDataAccessor.GetDownloadedData(entidad,true);

                string message = "";

                if (downloadedData.EntidadData.Rows.Count > 0) {
                    message += "{";
                    foreach(var row in downloadedData.EntidadData)
                    {
                        if (!dO.EsNulo(row,"Message"))
                        {
                            string entidadMsg = row["Message"].ToString();

                            entidadMsg = entidadMsg.Substring(1);
                            entidadMsg = entidadMsg.Substring(0,entidadMsg.Length-1); 
                            message += message.Length>1 ? $"{message},{entidadMsg}":entidadMsg;
                        }
                    }
                    message += "}";
                    var res = await agmWebAPI.NotifyDescargaAsRecieved(message);
                    return res;
                }
                else                
                    return 
                        new Result {
                            success = false 
                        ,value="No pending data to be notified"
                        };                            
            } 
            catch(Exception e)
            {
                return new Result(){ success = false, value = e.Message };
            }
        }

        public async Task<Result> InsertDownloadedData()
        {
            try
            {
                var regs = await agmDA.InsertDownloadedData();
                return (new Result
                {
                    success = true,
                    value = regs.value
                }); ;
            }
            catch (System.Exception e)
            {
                return (new Result
                {
                    success = false,
                    value = e.Message
                });
            }
        }

        #endregion
    }
}
