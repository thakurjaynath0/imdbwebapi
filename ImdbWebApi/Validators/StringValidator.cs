using ImdbWebApi.Exceptions;

namespace ImdbWebApi.Validators
{
    public class StringValidator
    {
        public readonly string _parameterName;
        public readonly string _value;

        private StringValidator(string parameterName, string value)
        {
            _parameterName = parameterName;
            _value = value;
        }

        public static StringValidator Validate(string parameterName, string value)
        {
            return new StringValidator(parameterName, value);
        }

        public StringValidator Required()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                throw new BadRequestException($"{_parameterName} should not be empty or null.");
            }

            return this;
        }

        public StringValidator MinLength(int minLength)
        {
            if (_value.Length < minLength)
            {
                throw new BadRequestException($"{_parameterName} should be minimum {minLength} characters.");
            }
            return this;
        }

        public StringValidator MaxLength(int maxLength)
        {
            if (_value.Length > maxLength)
            {
                throw new BadRequestException($"{_parameterName} should be maximum {maxLength} characters.");
            }

            return this;
        }
    }
}
