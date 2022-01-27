using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Models
{
    public class LoginResponse : BaseResponse
    {
        public LoginResponse(string token, bool success, string message) : base(success, message)
        {
            AuthToken = token;
        }
        public string AuthToken { set; get; }
    }
}
