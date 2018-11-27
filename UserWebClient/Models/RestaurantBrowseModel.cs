using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLibrary;

namespace UserWebClient.Models
{
    public class RestaurantBrowseModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public SelectList CategoryList { get; set; }

        [Display(Name = "Zip Code")]
        public int SelectedZipCode { get; set; }

        [Display(Name = "Category")]
        public int SelectedRestaurantCategoryId { get; set; }

        public int Page { get; set; }
        public int Amount { get; set; }
    }
}