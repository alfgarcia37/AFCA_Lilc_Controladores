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
        void PrepareAgmRecibosEnvios(string remesaID);
        void PrepareAgmCalificacionesEnvios(string ganaderiaCodigoSelected);

        AgmEnvioData GetAgmEnvioDataByParent(ItemData itemParent);
        AgmEnvioData GetAgmEnviosPrincipalesToSendData();
        AgmEnvioData GetAgmEnviosToSendData(string entidad);
        string GenerateJsonMessageEnvio(string ganaderiaID, ArrayList entidadesIncluded);
        Task<Result> SendJsonMessageEnvio(string ganaderiaID, string jsonMessage);
        Task<Result> ExecuteIndicadores();
        bool MarkAgmEnviosAsSended(string ganaderiaID, ArrayList entidadesIncluded,out string message);
        bool MarkAgmEnvioAsNotSended(string ganaderiaID,string entidad, string error,out string message);

        Task<AgmDownloadData> DownloadAgmPrincipalData(string entidad);
        Task<Result> SaveJsonDownloadedData(string entidad,DateTime fechaDescarga, string message);
        Task<Result> NotifyDescargaAsRecieved(string entidad=null);
        Task<Result> InsertDownloadedData();
    }
}
