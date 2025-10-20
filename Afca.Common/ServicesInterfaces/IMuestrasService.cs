
namespace Afca.Common.ServicesInterfaces
{
    using Afca.Common.DataSets;
    using Afca.Common.Models;
    using FrameworkForge.Common.DataStructures;
    using FrameworkForge.Common.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IMuestrasService : IService
    {
      Task<List<MuestraCruzada>> GetMuestrasCruzadas();
      Task<List<MuestraIndustria>> GetMuestrasIndustria();
  }
}
