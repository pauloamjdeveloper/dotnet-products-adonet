using System.ComponentModel.DataAnnotations;

namespace Products.ADO.NET.WebAPI.Utilities
{
    public class DecimalRangeAttribute : ValidationAttribute
    {
        private readonly decimal _minimum;
        private readonly decimal _maximum;

        public DecimalRangeAttribute()
        {
            _minimum = 0.01m;
            _maximum = decimal.MaxValue;
        }

        public override bool IsValid(object value)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue >= _minimum && decimalValue <= _maximum;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The field {name} must be between {_minimum} and {_maximum}.";
        }
    }
}
