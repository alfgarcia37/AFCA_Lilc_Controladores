using Afca.Common.DataSets;
using Afca.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.DataAccessor
{
    public interface IAgmWebApiDataAccessor
    {
        Task<Result> UploadData(string ganaderiaId,string jsonData);
        Task<Result> ExecuteIndicadores();
        Task<AgmDownloadData> DownloadAgmData(string entidad);
        Task<Result> NotifyDescargaAsRecieved(string entidad, DateTime fechaDescarga);
    }
}
