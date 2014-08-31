namespace EntityFramework.Models
{
    using System.Collections.Generic;

    public partial class Employee
    {
        public ICollection<Territory> GetTerritories
        {
            get
            {
                return this.Territories;
            }
        }
    }
}
