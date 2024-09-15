using lesson_final.Models;
using lesson_final.Types;
using Microsoft.EntityFrameworkCore;

namespace lesson_final.Interfaces;

public interface ITransaction
{
    List<Transaction> GetAllTransactionsByUser(int id);
    List<Transaction> GetAllTransactionsByUserForPeriod(int id, DateTime dateFrom, DateTime dateTo);

    List<Transaction> GetAllTransactionsByUserWithFilters(int id, OperationType? opType, DateTime? dateFrom, DateTime? dateTo);

    void AddTransaction(Transaction transaction);
}