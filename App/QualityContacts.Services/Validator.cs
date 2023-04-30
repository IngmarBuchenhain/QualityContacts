using System;
using System.Text.RegularExpressions;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Services;


namespace QualityContacts.Services
{
	public class Validator : IContactValidator
	{
		public Validator()
		{
		}

        // TODO: Implement full regex pattern.
        private const string PHONE_NUMBER_PATTERN = "^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$"; // @"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}";
        //private const string TEST = @"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}";
        //private const Regex regex = new Regex(PHONE_NUMBER_PATTERN);

        public IValidationResult Validate(string input)
        {
            //var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            ////Console.WriteLine(input);

            //if (String.IsNullOrEmpty(input)) return false;

            ////
            ////
            ////if (input.Length < 4) return false;
            //var number = phoneNumberUtil.Parse(input, "DE");
            //var validation = phoneNumberUtil.IsValidNumber(number);
            //if (!validation) return false;
            List<ValidationError> errors = new List<ValidationError>();
            List<ValidationWarning> warnings = new List<ValidationWarning>();
            //return true;
            if (input == null || input.Length < 6 || input.Length > 20) return new ValidationResult(false, true, errors, warnings);

            bool isValid = false;
            bool hasWarnings = false;
            

            Regex rg = new Regex(PHONE_NUMBER_PATTERN);

            isValid = rg.IsMatch(input);

            // TODO: Add Checks for warnings

            // TODO: Add search for specific errors.

            Console.WriteLine("{0} {1} a valid mobile number.", input,
                    isValid ? "is" : "is not");

            ValidationResult result = new ValidationResult(isValid, hasWarnings, errors, warnings);
            return result;
        }

        public IValidationResult Validate(IContact contact)
        {
            throw new NotImplementedException();
        }
    }
}

