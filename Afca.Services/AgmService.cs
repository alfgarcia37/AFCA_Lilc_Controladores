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
                case "AgmEnviosSecundarios":
                   throw new Exception("No implementado");                    
            }
        }
        public AgmEnvioData GetAgmEnvioDataByParent(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmEnviosPrincipales":
                    return agmDA.GetAgmEnviosPrincipalesData();
                case "AgmEnviosRecibos":
                    return agmDA.GetAgmEnviosSecundariosData("Recibos");
                default:
                    return null;
            }
        }
        public AgmEnvioData GetAgmEnvioPrincipalesToSendData()
        {
            string[] entidades = new string[] { 
                "Controles", "Lecturas",
                "Inseminaciones", "Lactaciones", 
                "Parametros", "Partos" };

            AgmEnvioData data = agmDA.GetAgmEnviosPrincipalesData();

            foreach(DataRow gan in data.Ganaderia.Rows)
            {
                gan["Status"] = "PE";
                gan["Selected"] = true;

                foreach(string entidad in entidades)
                {
                    if (!dO.EsNulo(gan, entidad)
                            && Convert.ToInt32(gan[entidad])>0)
                    {
                        DataRow detail = data.Detail.NewRow();
                        dO.ImportPrimaryKeysValues(gan, detail);
                        detail["Entidad"] = entidad;
                        detail["Count"] = Convert.ToInt32(gan[entidad]);
                        detail["Selected"] = true;
                        detail["Status"] = "PE";
                        data.Detail.Rows.Add(detail);
                    }
                }
            }
            return data;
        }
        public string GenerateJsonMessageEnvioPrincipal(string ganaderiaID, ArrayList entidadesIncluded)
        {
            string jsonMessage = "";

            AgmEnvioData data = agmDA.GetEnvioPrincipalMessageData(ganaderiaID, entidadesIncluded);

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
        public async Task<Result> SendJsonMessageEnvioPrincipal(string ganaderiaID, string jsonMessage)
        {
            InitializeAgmWebApiDA();
            return await agmWebAPI.UploadPrincipalData(ganaderiaID,jsonMessage).ConfigureAwait(false);
        }

        public bool MarkAgmEnviosPrincipalesAsSended(string ganaderiaID, ArrayList entidadesIncluded,out string message)
        {
            message = "";
            try {
                agmDA.MarkAgmEnviosPrincipalesAsSended(ganaderiaID, entidadesIncluded);
                return true;
            }
            catch(System.Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        public void PrepareAgmRecibosEnvios(string remesaID)
        {
            agmDA.PrepareAgmEnviosRecibos(remesaID);
        }
        public AgmEnvioData GetAgmEnviosSecundariosToSend(string entidad)
        {
           AgmEnvioData data = agmDA.GetAgmEnviosSecundariosData(entidad);

            foreach (DataRow gan in data.Ganaderia.Rows)
            {
                gan["Status"] = "PE";
                gan["Selected"] = true;

                foreach (string e in new string[] {entidad })
                {
                    if (!dO.EsNulo(gan, e)
                            && Convert.ToInt32(gan[e]) > 0)
                    {
                        DataRow detail = data.Detail.NewRow();
                        dO.ImportPrimaryKeysValues(gan, detail);
                        detail["Entidad"] = e;
                        detail["Count"] = Convert.ToInt32(gan[e]);
                        detail["Selected"] = true;
                        detail["Status"] = "PE";
                        data.Detail.Rows.Add(detail);
                    }
                }
            }
            return data;
        }

    }
}
