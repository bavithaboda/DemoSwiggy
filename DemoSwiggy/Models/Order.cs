using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(40)]
        public string City { get; set; }
        [MaxLength(300)]
        public string State { get; set; }
        public string Country { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Items> Items { get; set; }
    }
}
