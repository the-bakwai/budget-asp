using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BudgetAsp.Models
{
    public class Category : IEntity
    {
        public long Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
    }


    public interface ICategoryRepository: IRepository<Category>
    {
    }

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BudgetContext context)
        {
            Context = context;
        }

        private BudgetContext BudgetContext => Context as BudgetContext;
    }
}