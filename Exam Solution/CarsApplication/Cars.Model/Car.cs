namespace Cars.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        [MaxLength(20)]
        public string Model { get; set; }

        public virtual TransmissionType Transmission { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public virtual Dealer Dealer { get; set; }
    }
}
