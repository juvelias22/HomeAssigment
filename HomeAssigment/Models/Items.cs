using System.ComponentModel.DataAnnotations;

namespace HomeAssigment.Models
{
    public class Items
    {
        internal object itemname;

        ///<summary>
        ///id  is the primary key.. it should be named id
        ///
        ///

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item type")]
        public int ItemTypeId { get; set; }
        public ItemType itemName { get; set; }




        public string ItemOwner { get; set; }


        [Required]
        [Display(Name = "Item Quality")]

        public int QualityId { get; set; }
        public Quality qualityType { get; set; }

        [Required]
        [Display(Name = "Item Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int ItemQuantity { get; set; }

        [Required]
        [Display(Name = "Item Price")]
        [Range(1, double.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double ItemPrice { get; set; }




    }
}