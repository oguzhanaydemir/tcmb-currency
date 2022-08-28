using System;

namespace Protein.Tech.TCMB.Core.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
