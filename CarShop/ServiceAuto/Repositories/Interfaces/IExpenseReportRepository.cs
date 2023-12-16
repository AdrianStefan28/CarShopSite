using ServiceAuto.Models;

namespace ServiceAuto.Repositories.Interfaces
{
    public interface IExpenseReportRepository : IBaseRepository<ExpenseReport>
    {
        ExpenseReport? GetExpenseReportByServiceId(int id);
        ExpenseReport? CalculateProfitForExpenseReport(ExpenseReport expenseReport);
    }
}
