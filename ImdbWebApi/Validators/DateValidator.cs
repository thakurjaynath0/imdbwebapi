using ImdbWebApi.Exceptions;
using System;

namespace ImdbWebApi.Validators
{
    public class DateValidator
    {
        private readonly DateTime _date;
        private readonly string _parameterName;

        private DateValidator(string parameterName, DateTime date)
        {
            _parameterName = parameterName;
            _date = date;
        }


        public static DateValidator Validate(string parameterName, DateTime date)
        {
            return new DateValidator(parameterName, date);
        }

        public DateValidator MinDate(DateTime minDate)
        {
            if (DateTime.Compare(_date, minDate) < 0)
            {
                throw new BadRequestException($"{_parameterName} should not be less than {minDate.Year}-{minDate.Month}-{minDate.Day}.");
            }

            return this;
        }

        public DateValidator MaxDate(DateTime maxDate)
        {
            if (DateTime.Compare(_date, maxDate) > 0)
            {
                throw new BadRequestException($"{_parameterName} should not be more than today.");
            }

            return this;
        }
    }
}
