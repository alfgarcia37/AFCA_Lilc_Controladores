using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.DataAccessor
{
    public interface IAgmWebApiDataAccessor
    {
        Task<Result> UploadPrincipalData(string ganaderiaId,string jsonData);
    }
}
