using System;

namespace Protein.Tech.TCMB.Core.Application.Dtos
{
    public class CurrencyDetailDTO
    {
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
        public string Changes { get; set; }
    }
}
