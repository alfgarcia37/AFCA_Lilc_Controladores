namespace Afca.Services
{
    using System.Data;
    using Afca.Common.DataAccessor;
    using Afca.Common.ServicesInterfaces;
    using Afca.Common.DataSets;
    using FrameworkForge.Common;
    using FrameworkForge.Common.DataStructures;


    public class GanaderiaService : IGanaderiaService
    {

        private IGanaderiaDataAccessor ganaderiaDA;

        public GanaderiaService()
        {
            ganaderiaDA = Afca.Common.AfcaDataAccessorProvider.GetGanaderiaDataAccessor();
        }
        public GanaderiaDomainData LoadGanaderiaDomainData()
        {
            return ganaderiaDA.LoadGanaderiaDomainData();
        }
    }
}
