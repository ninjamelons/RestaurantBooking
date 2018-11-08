using System.ComponentModel.DataAnnotations;

namespace ModelLibrary
{
    public class Item
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid number")]
        public string Id { get; set; }

        [RegularExpression("^[0-9A-Za-z ]{1,30}$", ErrorMessage = "Invalid name")]
        public string Name { get; set; }

        [RegularExpression("^[0-9A-Za-z ]{1,500}$", ErrorMessage = "Invalid description")]
        public string Description { get; set; }

        [RegularExpression("^[0-9A-Za-z ]{1,30}$", ErrorMessage = "Invalid category name")]
        public string Category { get; set; }
    }
}