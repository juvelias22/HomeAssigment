using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeAssigment.Models
{
    public class Items
    {
        ///<summary>
        ///id  is the primary key.. it should be named id
        ///
        ///

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        [MinLength(2, ErrorMessage = "min char is two ")]
        public string ItemName { get; set; }

        public string ItemOwner { get; set; }


        [Required]
        [Display(Name = "Item Quality")]
        public string ItemQuality { get; set; }

        [Required]
        [Display(Name = "Item Category")]
        public string ItemCategory { get; set; }

        [Required]
        [Display(Name = "Item Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int ItemQuantity { get; set; }

        [Required]
        [Display(Name = "Item Price")]
        [Range(1, double.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double ItemPrice { get; set; }

        [Required]
        [Display(Name = "Item Image")]
        public string ItemImage { get; set; }


        public class StringRangeAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {

                if (value.ToString() == "Excellent" || value.ToString() == "Good" || value.ToString() == "Poor" || value.ToString() == "Bad")
                {
                    return ValidationResult.Success;
                }


                return new ValidationResult("Please enter a correct value");
            }
        }
    }
}