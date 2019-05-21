namespace Afca.Common.DataAccessor
{
    using FrameworkForge.Common.Interfaces;
    using Afca.Common.DataSets;


    public interface IGanaderiaDataAccessor : IDataAccessor
    {
        GanaderiaDomainData LoadGanaderiaDomainData();
    }
}
