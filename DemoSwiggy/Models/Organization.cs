using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Models
{
    public class Organization
    {
        [Key]
       public int OrgId { get; set; }
        public string Name { get; set; }
        public string EstablishmentYear { get; set; }
        public int TANNumber { get; set; }
        public string ContactNo { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
