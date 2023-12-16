using ServiceAuto.Models;

namespace ServiceAuto.Services.Interfaces
{
    public interface IExpenseReportService
    {
        IEnumerable<ExpenseReport> GetExpenseReports();
        ExpenseReport GetExpenseReport(int id);
        ExpenseReport AddExpenseReport(ExpenseReport expenseReport);
        ExpenseReport UpdateExpenseReport(ExpenseReport expenseReport);
        void DeleteExpenseReport (int id);
        ExpenseReport GetExpenseReportByServiceId(int id);
    }
}
