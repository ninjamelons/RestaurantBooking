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
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z ]$*", ErrorMessage = "Invalid Name")] //{3,20}
        public string Name { get; set; }

        [MaxLength(150)]
        [MinLength(3)]
        [RegularExpression("^[0-9 a-zA-Z]*", ErrorMessage = "Invalid Description")] //{3,150}
        public string Description { get; set; }

        [RegularExpression("^[0-9/.]*", ErrorMessage = "Invalid Price")]
        public double Price { get; set; }

        public Price PriceObj { get; set; }

        public List<Menu> Menus { get; set; }

        public int Id { get; set; }

        public int MenuId { get; set; }
        [Required]
        public int ItemCatId { get; set; }
    }
}
