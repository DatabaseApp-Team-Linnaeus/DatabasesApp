namespace SupermarketsChain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Expense
    {
        [Key]
        public int Id { get; set; }
        
        // Maybe this is not the smartest name
        [Required]
        public DateTime DateOfExpense { get; set; }
        [Required]
        public decimal ExpenseAmount { get; set; }
        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
