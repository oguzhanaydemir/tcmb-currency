using Protein.Tech.TCMB.Core.Domain.Common;
using System;

namespace Protein.Tech.TCMB.Core.Domain.Entities
{
    public class CurrencyRate : BaseEntity
    {
        public decimal Rate { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Changes { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
