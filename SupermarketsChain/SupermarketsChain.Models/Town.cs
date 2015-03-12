namespace SupermarketsChain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        private ICollection<Supermarket> supermarkets;

        public Town()
        {
            this.supermarkets = new HashSet<Supermarket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Supermarket> Supermarkets
        {
            get
            {
                return this.supermarkets;
            }

            set
            {
                this.supermarkets = value;
            }
        }
    }
}
