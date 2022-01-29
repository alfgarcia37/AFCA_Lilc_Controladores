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
        AgmEnvioData GetAgmEnviosPrincipalesData();
        void PrepareAgmEnviosSecundarios();
        AgmEnvioData GetAgmEnviosSecundariosData();
        AgmEnvioData GetEnvioPrincipalMessageData(string ganaderiaID, ArrayList entidadesIncluded);
        int MarkAgmEnviosPrincipalesAsSended(string ganaderiaID, ArrayList entidadesIncluded);
    }
}
