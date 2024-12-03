using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class ExpenseReportService : IExpenseReportService
    {
        private readonly IExpenseReportRepository expenseReportRepository;
        public ExpenseReportService(IExpenseReportRepository expenseReportRepository)
        {
            this.expenseReportRepository = expenseReportRepository;
        }

        public IEnumerable<ExpenseReport> GetExpenseReports()
        {
            return expenseReportRepository.GetAll();
        }

        public ExpenseReport GetExpenseReport(int id)
        {
            return expenseReportRepository.Get(id);
        }

        public ExpenseReport AddExpenseReport(ExpenseReport expenseReport)
        {
            return expenseReportRepository.Add(expenseReport);
        }

        public ExpenseReport UpdateExpenseReport(ExpenseReport expenseReport)
        {
            return expenseReportRepository.Update(expenseReport);
        }

        public void DeleteExpenseReport(int id)
        {
            expenseReportRepository.Remove(id);
        }

        public ExpenseReport GetExpenseReportByServiceId(int id)
        {
            return expenseReportRepository.GetExpenseReportByServiceId(id);
        }

        public ExpenseReport UpdateExpenseReportProfit(ExpenseReport expenseReport)
        {
            return expenseReportRepository.CalculateProfitForExpenseReport(expenseReport);
        }
    }
}
