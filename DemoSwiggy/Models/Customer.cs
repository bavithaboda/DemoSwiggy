using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        
        public ICollection<Order> Orders { get; set; }
        [ForeignKey("Organization")]
        public int OrgId { get; set; }
        public Organization Organization{ get; set; }

    }
}
