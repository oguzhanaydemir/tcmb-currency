using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Core.Application.Dtos
{
    public class CurrencyDTO
    {
        public string Currency { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Rate { get; set; }
    }
}
