﻿using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserWebClient.Models
{
    public class RestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }
       // public Dictionary<string, RestaurantCategory> Categories { get; set; }
        public SelectList CategoryList { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}