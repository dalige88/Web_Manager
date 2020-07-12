using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YL.Base.Validation
{
    /// <summary>
    /// 数量验证
    /// </summary>
    public class NumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.GetType() == typeof(decimal))
            {
                decimal v = (decimal)value;
                if (v <= 0 || v > 999999)
                {
                    return new ValidationResult(ErrorMessage + " 必须大于 0,小于 999999 的正整数");
                }
            }
            else if (value.GetType() == typeof(int))
            {
                int v = (int)value;
                if (v <= 0 || v > 999999)
                {
                    return new ValidationResult(ErrorMessage + " 必须大于 0,小于 999999 的正整数");
                }
            }
            else if (value.GetType() == typeof(long))
            {
                long v = (long)value;
                if (v <= 0 || v > 999999)
                {
                    return new ValidationResult(ErrorMessage + " 必须大于 0,小于 999999 的正整数");
                }
            }
            return ValidationResult.Success;
        }
    }
}
