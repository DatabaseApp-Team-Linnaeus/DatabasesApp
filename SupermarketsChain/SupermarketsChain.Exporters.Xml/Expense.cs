﻿namespace XmlExporterConsoleClient
{
    using System;

    public class Expense
    {
        public Expense(DateTime date, decimal expenseValue)
        {
            this.Date = date;
            this.ExpenseValue = expenseValue;
        }

        public DateTime Date { get; set; }

        public decimal ExpenseValue { get; set; }
    }
}
