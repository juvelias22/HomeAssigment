using System.ComponentModel.DataAnnotations;

namespace HomeAssigment.Validation
{
    public class StringQualityOptions : ValidationAttribute
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