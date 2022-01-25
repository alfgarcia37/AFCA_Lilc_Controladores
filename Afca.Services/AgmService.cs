using Afca.Common.DataAccessor;
using Afca.Common.DataSets;
using Afca.Common.ServicesInterfaces;
using FrameworkForge.Common.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Services
{
    public class AgmService : IAgmService
    {
        IAgmDataAccessor agmDA;
        public AgmService()
        {
            agmDA = Afca.Common.AfcaDataAccessorProvider.GetAgmDataAccessor();
        }
        public void PrepareAgmEnvios(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmEnviosPrincipales":
                     agmDA.PrepareAgmEnviosPrincipales();
                    break;
                case "AgmEnviosSecundarios":
                    throw new Exception("Operación no implementada");
                    break;

            }
        }
        public AgmEnvioData GetAgmEnvioDataByParent(ItemData itemParent)
        {
            switch (itemParent.Name)
            {
                case "AgmEnviosPrincipales":
                    return agmDA.GetAgmEnviosPrincipalesData();
                case "AgmEnviosSecundarios":
                    return agmDA.GetAgmEnviosSecundariosData();
                default:
                    return null;
            }
        }
    }
}
