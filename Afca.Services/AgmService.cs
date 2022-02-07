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
        public void PrepareAgmEnvios(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmEnviosPrincipales":
                     agmDA.PrepareAgmEnviosPrincipales();
                    break;
                case "AgmEnviosRCS":
                   agmDA.PrepareAgmEnviosRCS();
                    break;
                case "AgmEnviosCenso":
                    agmDA.PrepareAgmEnviosCenso();
                    break;
                case "AgmEnviosGenomica":
                    agmDA.PreparaAgmEnviosGenomica();
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
                case "AgmEnviosRCS":
                    return agmDA.GetAgmEnviosSecundariosData("RCS");
                case "AgmEnviosCalificaciones":
                    return agmDA.GetAgmEnviosSecundariosData("Calificaciones");
                case "AgmEnviosCenso":
                    return agmDA.GetAgmEnviosSecundariosData("Censo");
                case "AgmEnviosGenomica":
                    return agmDA.GetAgmEnviosSecundariosData("Genomica");
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
                "Controles", "Lecturas",
                "Inseminaciones", "Lactaciones", 
                "Parametros", "Partos" }
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


    }
}
