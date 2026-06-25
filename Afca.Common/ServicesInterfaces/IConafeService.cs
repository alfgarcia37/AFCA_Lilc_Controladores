#nullable enable

namespace Afca.Common.ServicesInterfaces
{
    using Afca.Common.DataSets;
    using Afca.Common.Models;
    using FrameworkForge.Common.DataStructures;
    using FrameworkForge.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConafeService : IService
    {
        Task<RespuestaConafe> GetRegistrosConafe();
        Task<RespuestaConafe> GetCensoConafe();
        Task<RespuestaConafe> GetLactacionesConafe(string? vaca);
        Task<RespuestaConafe> GetControlesConafe(string? vaca);
        Task<RespuestaConafe> GetPartosConafe(string? vaca);
        Task<RespuestaConafe> GetCubricionesConafe(string? vaca);
    }
}
