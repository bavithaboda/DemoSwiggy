using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Models
{
    public class Items
    {[Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public int quantity { get; set; }
        public int Total { get; set; }
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        public Order order { get; set; }

    }
}
