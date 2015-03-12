namespace SupermarketsChain.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductTax
    {
        [ForeignKey("Product")]
        public int Id { get; set; }

        public int TaxValue { get; set; }

        public virtual Product Product { get; set; }
    }
}
