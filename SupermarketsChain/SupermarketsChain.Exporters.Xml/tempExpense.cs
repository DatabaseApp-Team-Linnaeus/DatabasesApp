namespace XmlExporterConsoleClient
{
    using System;

    public class tempExpense
    {
        public tempExpense(DateTime date, decimal expenseValue)
        {
            this.Date = date;
            this.ExpenseValue = expenseValue;
        }

        public DateTime Date { get; set; }

        public decimal ExpenseValue { get; set; }
    }
}
