using Afca.Common.DataSets;
using FrameworkForge.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.DataAccessor
{
    public interface IAgmDataAccessor:IDataAccessor
    {
        void PrepareAgmEnviosPrincipales();
        AgmEnvioData GetAgmEnviosData();        
        AgmEnvioData GetEnvioMessageData(string ganaderiaID, ArrayList entidadesIncluded);
        int MarkAgmEnviosAsSended(string ganaderiaID, ArrayList entidadesIncluded);

        void PrepareAgmEnviosRecibos(string remesaID);
        AgmEnvioData GetAgmEnviosSecundariosData(string entidad);
        void PrepareAgmEnviosRCS();
    }
}
