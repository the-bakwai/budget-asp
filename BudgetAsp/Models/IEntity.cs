using System;

namespace BudgetAsp.Models
{
    public interface IEntity
    {
        public long Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}