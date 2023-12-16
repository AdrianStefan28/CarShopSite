namespace ServiceAuto.Models
{
    public class ExpenseReport : ModelEntity
    {
      
        public float? ExpenseReportExpense { get; set; }
        public float? ExpenseReportIncome { get; set; }
        public float? ExpenseReportProfit { get; set; }

        public int ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}
