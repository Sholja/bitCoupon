using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BitCoupon.DAL.validators
{
    /// <summary>
    /// Custom validator for date
    /// </summary>
    class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)  //drugin nacin - malo bolje
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date < DateTime.Now)
                {
                    return new ValidationResult("Date out of range.");
                }
                else if (date > DateTime.Now.AddMonths(12))
                {
                    return new ValidationResult("Date out of range.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Date field empty");
        }
      
    }
   
    public static class ModelStateExtensions
    {
        public class Error
        {
            public Error(string key, string message)
            {
                Key = key;
                Message = message;
            }

            public string Key { get; set; }
            public string Message { get; set; }
        }

        public static IEnumerable<Error> AllErrors(this ModelStateDictionary modelState)
        {
            var result = new List<Error>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                                   .Select(error => new Error(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            return result;
        }
       
    }
}
