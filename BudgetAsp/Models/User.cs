using System;
using System.Collections.Generic;

namespace BudgetAsp.Models
{
    public class User : IEntity
    {
        public long Id { get; set; }

        public string AuthId { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Payee> Payees { get; set; }
    }
    
    public interface IUserRepository: IRepository<User>
    {
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BudgetContext context)
        {
            Context = context;
        }
    }
}