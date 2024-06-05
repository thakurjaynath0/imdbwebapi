using ImdbWebApi.Exceptions;

namespace ImdbWebApi.Validators
{
    public class IntegerValidator
    {
        private readonly int _value;
        private readonly string _parameterName;

        private IntegerValidator(string parameterName, int value)
        {
            _value = value;
            _parameterName = parameterName;
        }

        public static IntegerValidator Validate(string parameterName, int value)
        {
            return new IntegerValidator(parameterName, value);
        }

        public IntegerValidator MinValue(int minValue)
        {
            if (_value < minValue)
            {
                throw new BadRequestException($"{_parameterName} should not be less than {minValue}.");
            }

            return this;
        }

        public IntegerValidator MaxValue(int maxValue)
        {
            if (_value > maxValue)
            {
                throw new BadRequestException($"{_parameterName} should not be more than {maxValue}.");
            }

            return this;
        }
    }
}
