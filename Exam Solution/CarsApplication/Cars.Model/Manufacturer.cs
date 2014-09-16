namespace Cars.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Manufacturer
    {
        private ICollection<Car> cars;

        public Manufacturer()
        {
            this.cars = new HashSet<Car>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(11)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars
        {
            get
            {
                return this.cars;
            }

            set
            {
                this.cars = value;
            }
        }
    }
}
