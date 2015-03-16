namespace SupermarketsChain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vendor
    {
        private ICollection<Product> products;

        private ICollection<Expense> expences;

        public Vendor()
        {
            this.products = new HashSet<Product>();
            this.expences = new HashSet<Expense>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }

            set
            {
                this.products = value;
            }
        }

        public virtual ICollection<Expense> Expences
        {
            get
            {
                return this.expences;
            }

            set
            {
                this.expences = value;
            }
        }
    }
}
