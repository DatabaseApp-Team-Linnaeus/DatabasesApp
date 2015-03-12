namespace SupermarketsChain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Measure
    {
        private ICollection<Product> products;

        public Measure()
        {
            this.products = new HashSet<Product>();
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
    }
}
