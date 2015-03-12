namespace SupermarketsChain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Expence
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        // Maybe this is not the smartest name
        public DateTime DateOfExpence { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
