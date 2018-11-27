using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModelLibrary
{
    public class Item
    {
        [Required]
        [Display(Name = "Item Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not exceed 20 characters")]
        [RegularExpression("^[a-zA-Z ]*", ErrorMessage = "Invalid Name")] //{3,20}
        public string Name { get; set; }
        
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Length too long/short")]
        [RegularExpression("^[0-9 a-zA-Z]*", ErrorMessage = "Invalid Description")] //{3,150}
        public string Description { get; set; }

        /*[RegularExpression("^[0-9/.]*", ErrorMessage = "Invalid Price")]
        public double Price { get; set; }*/

        public int Id { get; set; }

        public Price Price { get; set; }
        
        public ItemCat ItemCat { get; set; }
    }
}
