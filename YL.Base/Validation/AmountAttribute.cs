using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YL.Base.Validation
{
    /// <summary>
    /// 金额验证
    /// </summary>
    public class AmountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal v = (decimal)value;
            if (v <= 0 || v > 99999999)
                return new ValidationResult("金额必须大于0,小于99999999");

            return ValidationResult.Success;
        }
    }
}
