using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserWebClient.Models
{
    [Table("Restaurants")]
    public class Search
    {
        [Key]
        public string name { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public int phoneNo { get; set; }
    }
}