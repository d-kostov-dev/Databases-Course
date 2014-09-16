namespace Cars.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Dealer
    {
        private ICollection<City> cities;
        private ICollection<Car> cars;

        public Dealer()
        {
            this.cities = new HashSet<City>();
            this.cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities
        {
            get
            {
                return this.cities;
            }

            set
            {
                this.cities = value;
            }
        }

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
