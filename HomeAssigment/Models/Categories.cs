﻿using System.ComponentModel.DataAnnotations;

namespace HomeAssigment.Models
{
    public class Categories
    {
        ///<summary>
        ///id  is the primary key.. it should be named id
        ///
        ///

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "min char is two ")]
        public string Category { get; set; }




    }
}