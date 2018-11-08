﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModelLibrary
{
    public class Restaurant
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9 ]*$", ErrorMessage = "Name does not match regex")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "Name needs to be bewteen 3 and 30")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9 ]*$", ErrorMessage = "Address does not match regex")]
        [StringLength(60, MinimumLength = 3,
            ErrorMessage = "Address needs to be bewteen 3 and 60")]
        public string Address { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "ZipCode does not match regex")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "ZipCode must not exceed 5 characters")]
        public string ZipCode { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "PhoneNo does not match regex")]
        [StringLength(15, ErrorMessage = "PhoneNo must not exceed 15 characters")]
        public string PhoneNo { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9@.]*$", ErrorMessage = "Email does not match regex")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "Email needs to be bewteen 3 and 30")]
        public string Email { get; set; }

        public bool Verified { get; set; }
        
        public int CategoryId { get; set; }
    }
}