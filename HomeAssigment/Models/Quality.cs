using System;
using System.ComponentModel.DataAnnotations;

namespace HomeAssigment.Models
{
    public class Quality
    {
        ///<summary>
        ///id  is the primary key.. it should be named id
        ///
        ///

        [Key]
        public int Id { get; set; }

        public String QualityType { get; set; }


    }
}

