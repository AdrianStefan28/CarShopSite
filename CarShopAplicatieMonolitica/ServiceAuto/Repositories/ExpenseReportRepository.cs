using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class ExpenseReportRepository : BaseRepository<ExpenseReport>, IExpenseReportRepository
    {
        public ExpenseReportRepository(DbContext dbContext) : base(dbContext) { }

        public ExpenseReport GetExpenseReportByServiceId(int id)
        {
            return dbContext.Set<ExpenseReport>().Where(e => e.ServiceId.Equals(id)).FirstOrDefault();
        }

        public ExpenseReport CalculateProfitForExpenseReport(ExpenseReport expenseReport)
        {
            expenseReport.ExpenseReportProfit = expenseReport.ExpenseReportIncome - expenseReport.ExpenseReportExpense;
            var item = dbContext.Set<ExpenseReport>().Update(expenseReport);
            dbContext.SaveChanges();
            return item.Entity;
        }

    }
}
