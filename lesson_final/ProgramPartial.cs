using System.Drawing;
using Azure;
using lesson_final.Data;
using lesson_final.Models;
using lesson_final.Types;
using lesson_final.ViewModels;


public partial class Program
{
    private static List<ItemView> typeTransactionFilters = new List<ItemView>
    {
        new ItemView { Id = 1, Value = OperationType.Income.ToString() },
        new ItemView { Id = 2, Value = OperationType.Consumption.ToString() },
    };
    
    // 1. Добавление транзакций: Пользователь может добавлять новые транзакции, указывая тип (доход или расход), сумму и описание.
    static void AddTransaction()
    {
        Console.Clear();
        
        Transaction newTransaction = new Transaction();
        newTransaction.UserId = _currentUser.Id;
        
        Console.WriteLine("Specify the transaction type");
        newTransaction.Type = (OperationType)ItemsHelper.MultipleChoice(false, typeTransactionFilters.ToList(), false)-1;

        Console.Clear();
        newTransaction.Sum = InputHelper.GetPositiveDouble("transaction amount");

        Console.Clear();
        Console.WriteLine("Select a category available for this transaction type");
        var CategoriesForTypeTransaction = _categories.GetAllCategoryByType(newTransaction.Type);
        newTransaction.CategoryId = ItemsHelper.MultipleChoice(false, CategoriesForTypeTransaction.Select(e => new ItemView
        {
            Id = e.Id,
            Value = e.Name
        }).ToList(), false);

        Console.Clear();
        Console.WriteLine("Add a description to the transaction or press \\\"Enter\\\" to skip");
        string description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description))
            newTransaction.Description = description;

        try
        {
            UpdateBalance(newTransaction.Type, newTransaction.Sum);
            _transactions.AddTransaction(newTransaction);
            MessageHelper.PrintSuccess("Transaction added successfully");
        }
        catch (Exception e)
        {
            MessageHelper.PrintError(e.Message);
        }
    }
    private static void UpdateBalance(OperationType typeOperation, double sum)
    {
        switch (typeOperation)
        {
            case OperationType.Income:
                _currentUser.UserSetting.Balance += sum;
                break;
            case OperationType.Consumption:
                if (_currentUser.UserSetting.Balance - sum < 0)
                    throw new Exception($"Insufficient funds, current balance is {_currentUser.UserSetting.Balance}");
                _currentUser.UserSetting.Balance -= sum;
                break;

        }
        _users.UpdateUser(_currentUser);
    }
    // 2. Просмотр списка транзакций: Пользователь может просматривать список всех транзакций с указанием их типа, суммы и даты.
    static void SeeAllTransactions()
    {
        Console.Clear();
        var transactions = _transactions.GetAllTransactionsByUser(_currentUser.Id);
        if (transactions.Count > 0)
        {
            foreach (var tr in transactions)
            {
                Console.WriteLine(tr);
            }   
        }
        else
            Console.WriteLine("List transactions empty");
    }
    // 3. Расчет общего дохода и расхода: Пользователь может просматривать общую сумму доходов и расходов за определенный период времени.
    static void CalculationTotalIncomeAndExpenses()
    {
        Console.Clear();
        Console.WriteLine("Period for which you want to view transactions");
        int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
        {
            new ItemView { Id = 1, Value = "All time" },
            new ItemView { Id = 2, Value = "Last month" },
            new ItemView { Id = 3, Value = "Custom period" },
        }, false, true);

        if (result != 0)
        {
            List<Transaction>? allTransactions = null;

            switch (result)
            {
                case 1:
                    allTransactions = _transactions.GetAllTransactionsByUser(_currentUser.Id);
                    break;
                case 2:
                    allTransactions = _transactions.GetAllTransactionsByUserForPeriod(_currentUser.Id, DateTime.Now.AddMonths(-1),DateTime.Now);
                    break;
                case 3:
                    DateTime dateFrom = InputHelper.GetDate("from").ToDateTime(TimeOnly.MinValue);
                    DateTime dateTo = InputHelper.GetDate("to").ToDateTime(TimeOnly.MinValue);
                    allTransactions = _transactions.GetAllTransactionsByUserForPeriod(_currentUser.Id, dateFrom, dateTo);
                    break;
            }
            
            if (allTransactions == null)
            {
                Console.WriteLine("No transactions found for the selected period");
                return;
            }
        
            double totalIncome = CalculateTotalByType(allTransactions, OperationType.Income);
            double totalConsumption = CalculateTotalByType(allTransactions, OperationType.Consumption);

            Console.WriteLine($"Total: income({totalIncome}), expenses({totalConsumption})");   
        }
    }
    
    private static double CalculateTotalByType(List<Transaction> transactions, OperationType type)
    {
        return transactions.Where(e => e.Type == type).Sum(e => e.Sum);
    }
    
    // 4. Фильтрация транзакций: Пользователь может фильтровать транзакции по типу (доход или расход) и периоду времени.
    static void FilterTransactions()
    {
        Console.Clear();
        Console.WriteLine("Select by what criteria to sort the processing");

        var typeTransactionFiltersInner = typeTransactionFilters.ToList();
        typeTransactionFiltersInner.Insert(0, new ItemView { Id = 3, Value = "All types" });
        int typeTransactions = ItemsHelper.MultipleChoice(true, typeTransactionFiltersInner, false, true);

        if (typeTransactions != 0)
        {
            int periodTransactions = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "All time" },
                new ItemView { Id = 2, Value = "Last month" },
                new ItemView { Id = 3, Value = "Custom period" },
                new ItemView { Id = 4, Value = "Only From" },
                new ItemView { Id = 5, Value = "Only To" },
            }, true, true);

            if (periodTransactions != 0)
            {
                DateTime? dateFrom = null;
                DateTime? dateTo = null;
        
                switch (periodTransactions)
                {
                    case 1:
                        break;
                    case 2:
                        dateFrom = DateTime.Now.AddMonths(-1);
                        dateTo = DateTime.Now;
                        break;
                    case 3:
                        dateFrom = InputHelper.GetDate("from").ToDateTime(TimeOnly.MinValue);
                        dateTo = InputHelper.GetDate("to").ToDateTime(TimeOnly.MinValue);
                        break;
                    case 4:
                        dateFrom = InputHelper.GetDate("from").ToDateTime(TimeOnly.MinValue);
                        break;
                    case 5:
                        dateTo = InputHelper.GetDate("to").ToDateTime(TimeOnly.MinValue);
                        break;
                }

                
                OperationType? opType = null;
                if (typeTransactions == 1)
                {
                    opType = OperationType.Income;
                }
                else if (typeTransactions == 2)
                {
                    opType = OperationType.Consumption;
                }
                
                var filteredTransactions = _transactions.GetAllTransactionsByUserWithFilters(_currentUser.Id, opType, dateFrom, dateTo);

                if (filteredTransactions.Any())
                {
                    foreach (var transaction in filteredTransactions)
                    {
                        Console.WriteLine(transaction);
                    }
                }
                else
                {
                    Console.WriteLine("No transactions found for the selected filters.");
                }
            }
        }
    }

    // 5. Отчет о состоянии финансов: Пользователь может получать отчет о текущем состоянии своих финансов, включая общий доход, расход, баланс и статистику по категориям транзакций.
    static void Report()
    {
        Console.Clear();
        var allTransactions = _transactions.GetAllTransactionsByUser(_currentUser.Id);
        
        double totalIncome = CalculateTotalByType(allTransactions, OperationType.Income);
        double totalConsumption = CalculateTotalByType(allTransactions, OperationType.Consumption);
        Console.WriteLine($"Current balance {_currentUser.UserSetting.Balance}");
        Console.WriteLine($"Total: income({totalIncome}), expenses({totalConsumption})");

        var statisticByCategories = allTransactions
            .GroupBy(e => e.CategoryId)
            .Select(g => new {
                CategoryName = g.FirstOrDefault().Category.Name,
                TotalSpentReceived = g.Sum(e => e.Sum),
                TotalTransaction = g.Count(),
                Type = g.FirstOrDefault().Category.Type
            }).ToList();

        foreach (var category in statisticByCategories)
        {
            string operation = "received"; 
            if (category.Type == OperationType.Consumption)
                operation = "spent";
            
            Console.WriteLine($"Category name: {category.CategoryName}, Total amount {operation}: {category.TotalSpentReceived}, Total number transactions {category.TotalTransaction}");
        }
    }
};
