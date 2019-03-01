using QTF.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QTF.Service
{
   public  class BaseService
    {

        public ValidationInformation ManualValidator(object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, context, validationResults, true);
            ValidationInformation mv = new ValidationInformation();
            mv.Success = isValid;

            mv.Validation = validationResults.Select(a => a.ErrorMessage).ToArray();

            return mv;
        }
    }
}
