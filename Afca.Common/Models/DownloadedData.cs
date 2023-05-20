using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afca.Common.Models
{
    public class DownloadedData
    {
        public string Entidad { get; set; }
        public DateTime FechaDescarga { get; set; }
        public List<object> Data { get; set; }
    }
}
