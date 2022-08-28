using Protein.Tech.TCMB.Core.Domain.Common;
using System.Collections.Generic;

namespace Protein.Tech.TCMB.Core.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Code { get; set; }

        public virtual ICollection<CurrencyRate> Rates { get; set; }
    }
}
