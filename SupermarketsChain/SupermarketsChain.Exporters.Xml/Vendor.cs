namespace XmlExporterConsoleClient
{
    using System;
    using System.Collections.Generic;

    public class Vendor
    {
        private readonly List<Expense> expenses = new List<Expense>(); 

        public Vendor(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public IEnumerable<Expense> Expenses
        {
            get { return this.expenses; }
        }

        public Vendor WithExpenses(DateTime date, decimal amount)
        {
            this.AddExpense(new Expense(date, amount));
            return this;
        }

        private void AddExpense(Expense expense)
        {
            this.expenses.Add(expense);
        }
    }
}
