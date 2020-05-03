using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyWebSolution.Models
{
    public class Item
    {
        ///<summary>
        ///id  is the primary key.. it should be named id
        ///
        ///

        [Key]
        public int ItemId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "your ")]
        public string ItemName { get; set; }




    }
}