using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Models
{
    public class LoginToken
    {
        long ttl = 108000000;
        public LoginToken() { }
        public LoginToken(string argKey, string argAuthToken)
        {
            Key = argKey;
            AuthToken = argAuthToken;
        }
        public string Key
        {
            get; set;
        }
        public string AuthToken
        {
            get; set;
        }
        public DateTime CreatedAt
        {
            get; set;
        }
        public bool IsExpired()
        {
            var dateTimeExpiration = CreatedAt.AddMilliseconds(ttl);

            return dateTimeExpiration.CompareTo(DateTime.Now) < 0;
        }
    }
}
