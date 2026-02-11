
namespace Afca.Common.ServicesInterfaces
{
    using Afca.Common.DataSets;
    using Afca.Common.Models;
    using FrameworkForge.Common.DataStructures;
    using FrameworkForge.Common.Interfaces;
  using System;
  using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IMuestrasService : IService
    {
      Task<List<MuestraCruzada>> GetMuestrasCruzadas(string establo);
      Task<List<MuestraIndustria>> GetMuestrasIndustria(string Establo,DateTime? FDesde,DateTime? FHasta);
      Task<List<MuestraIndustria>> GetMediasIndustria(string Establo, DateTime? FDesde, DateTime? FHasta);
     
  }
}
