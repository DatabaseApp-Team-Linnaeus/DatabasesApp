namespace SupermarketsChain.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Measure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
