//namespace XmlExporterConsoleClient
//{
//    using System;
//    using System.Collections.Generic;

//    public class tempVendor
//    {
//        private readonly List<tempExpense> expenses = new List<tempExpense>(); 

//        public tempVendor(string name)
//        {
//            this.Name = name;
//        }

//        public string Name { get; set; }

//        public IEnumerable<tempExpense> Expenses
//        {
//            get { return this.expenses; }
//        }

//        public tempVendor WithExpenses(DateTime date, decimal amount)
//        {
//            this.AddExpense(new tempExpense(date, amount));
//            return this;
//        }

//        private void AddExpense(tempExpense expense)
//        {
//            this.expenses.Add(expense);
//        }
//    }
//}

