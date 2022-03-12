using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Models
{
    public class SupportEmployee
    {[Key]
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Active { get; set; }
        [ForeignKey("SupportEmployee")]
        [InverseProperty("SupportEmployee")]
        public int? ManagerId { get; set; }
        public virtual SupportEmployee Manager { get; set; }
        public virtual ICollection<SupportEmployee> SupportEmployees { get; set; }
        [ForeignKey("Organization")]
        public int OrgId { get; set; }
        public Organization Organization { get; set; }

    }
}
