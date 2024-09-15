using lesson_final.Data;
using lesson_final.Interfaces;
using lesson_final.Models;
using lesson_final.Types;
using Microsoft.EntityFrameworkCore;

namespace lesson_final.Repository;

public class TransactionRepository : ITransaction
{
    public List<Transaction> GetAllTransactionsByUser(int id)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Transactions
                .Where(e => e.UserId == id)
                .Include(e => e.Category)
                .ToList();
        }
    }

    public List<Transaction> GetAllTransactionsByUserForPeriod(int id, DateTime dateFrom, DateTime dateTo)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Transactions
                .Where(e => e.UserId == id && e.CreatedAt >= dateFrom && e.CreatedAt <= dateTo)
                .ToList();
        }
    }

    public List<Transaction> GetAllTransactionsByUserWithFilters(int id, OperationType? opType, DateTime? dateFrom, DateTime? dateTo)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            var filterQuery = db.Transactions
                .Include(e => e.Category)
                .Where(e => e.UserId == id);

            if (opType.HasValue)
            {
                filterQuery = filterQuery.Where(e => e.Type == opType.Value);
            }
            
            if (dateFrom.HasValue)
            {
                filterQuery = filterQuery.Where(e => e.CreatedAt >= dateFrom);
            }
            
            if (dateTo.HasValue)
            {
                filterQuery = filterQuery.Where(e => e.CreatedAt <= dateTo);
            }

            return filterQuery.ToList();
        }
    }

    public void AddTransaction(Transaction transaction)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }
    }
}