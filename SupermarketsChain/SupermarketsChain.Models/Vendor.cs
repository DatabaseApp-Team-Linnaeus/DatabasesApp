namespace SupermarketsChain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vendor
    {
        private ICollection<Product> products;

        private ICollection<Expence> expences;

        public Vendor()
        {
            this.products = new HashSet<Product>();
            this.expences = new HashSet<Expence>();
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

        public virtual ICollection<Expence> Expences
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
