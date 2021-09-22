using System;
using System.Collections.Generic;
using BudgetAsp.Models;

namespace BudgetAsp.ViewModels
{
    public class TransactionsIndexViewModel
    {
        public TransactionsIndexViewModel(Account account, List<TransactionViewModel> transactions)
        {
            Account = account;
            Transactions = transactions;
        }

        public Account Account { get; }
        public List<TransactionViewModel> Transactions { get; }
    }

    public class TransactionViewModel
    {
        public TransactionViewModel(long id, DateTime date, string memo, string category, string payee, decimal amount, string cleared, string reconciled)
        {
            Id = id;
            Date = date;
            Memo = memo;
            Category = category;
            Payee = payee;
            Amount = amount;
            Cleared = cleared;
            Reconciled = reconciled;
        }

        public long Id { get; }
        public DateTime Date { get; }
        public string Memo { get; }
        public string Category { get; }
        public string Payee { get; }
        public decimal Amount { get; }
        public string Cleared { get; }
        public string Reconciled { get; }
    }
}