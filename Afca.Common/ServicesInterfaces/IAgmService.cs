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
        AgmEnvioData GetAgmEnvioPrincipalesToSendData();
        string GenerateJsonMessageEnvioPrincipal(string ganaderiaID, ArrayList entidadesIncluded);
        bool SendJsonMessageEnvioPrincipal(string ganaderiaID, string jsonMessage);
    }
}
