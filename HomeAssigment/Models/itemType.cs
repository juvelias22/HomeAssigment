using System.ComponentModel.DataAnnotations;

namespace HomeAssigment.Models
{
    public class ItemType
    {
        ///<summary>
        ///id  is the primary key.. it should be named id
        ///
        ///

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item Category")]
        public int CategoryId { get; set; }
        public Categories category { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        [MinLength(2, ErrorMessage = "min char is two ")]
        public string ItemName { get; set; }



        [Required]
        [Display(Name = "Item Image")]
        public string ItemImage { get; set; }



    }
}