
namespace Afca.Common.ServicesInterfaces
{
    using FrameworkForge.Common.Interfaces;
    using FrameworkForge.Common.DataStructures;
    using Afca.Common.DataSets;


    public interface IGanaderiaService : IService
    {
        GanaderiaDomainData LoadGanaderiaDomainData();
    }
}
