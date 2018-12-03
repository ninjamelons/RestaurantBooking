using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public int RoleId { get; set; }

        public List<OrderHistory> orderHistory { get; set; }
    }
}
