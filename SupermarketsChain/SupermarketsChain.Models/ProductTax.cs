namespace SupermarketsChain.Models
{
    public class ProductTax
    {
        public int Tax { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
