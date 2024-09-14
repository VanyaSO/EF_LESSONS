using lesson_final.Data;
using lesson_final.Interfaces;
using lesson_final.Models;
using lesson_final.Repository;

public partial class Program
{
    public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();

    public static ITransaction _transactions = new TransactionRepository();
    public static ICategory _categories = new CategoryRepository();
    public static IUser _users = new UserRepository();
    
    public static User _currentUser = _users.GetUserByEmail("ushachovg324@gmail.com");
    
    enum PersonalFinanceManagementApp
    {
        AddTransaction, SeeAllTransactions, TotalIncomeAndExpenses, FilterTransactions, Report, Exit
    }
    
    static void Main()
    {
        Initialize();
        int input = new int();

        do
        {
            input = ConsoleHelper.MultipleChoice(true, new PersonalFinanceManagementApp());
            switch ((PersonalFinanceManagementApp)input)
            {
                case PersonalFinanceManagementApp.AddTransaction:
                    AddTransaction();
                    break;
                case PersonalFinanceManagementApp.SeeAllTransactions:
                    SeeAllTransactions();
                    break;
                case PersonalFinanceManagementApp.TotalIncomeAndExpenses:
                    CalculationTotalIncomeAndExpenses();
                    break;
                case PersonalFinanceManagementApp.FilterTransactions:
                    FilterTransactions();
                    break;
                case PersonalFinanceManagementApp.Report:
                    Report();
                    break;
                case PersonalFinanceManagementApp.Exit:
                    break;
            }
            
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        } while ((PersonalFinanceManagementApp)input != PersonalFinanceManagementApp.Exit);
    }

    static void Initialize()
    {
        new DbInit().Init(DbContext());
    }
};
