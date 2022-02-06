using Afca.Common.DataSets;
using FrameworkForge.Common.DataStructures;
using FrameworkForge.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.ServicesInterfaces
{
    public interface IAgmService:IService
    {
        void PrepareAgmEnvios(ItemData itemParent);
        AgmEnvioData GetAgmEnvioDataByParent(ItemData itemParent);
        AgmEnvioData GetAgmEnviosPrincipalesToSendData();
        AgmEnvioData GetAgmEnviosToSendData(string entidad);
        string GenerateJsonMessageEnvio(string ganaderiaID, ArrayList entidadesIncluded);
        Task<Result> SendJsonMessageEnvio(string ganaderiaID, string jsonMessage);
        bool MarkAgmEnviosAsSended(string ganaderiaID, ArrayList entidadesIncluded,out string message);
        void PrepareAgmRecibosEnvios(string remesaID);
    }
}
