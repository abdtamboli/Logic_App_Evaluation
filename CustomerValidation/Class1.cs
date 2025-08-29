using System;
using System.Text.RegularExpressions;

namespace CustomerValidation
{
    /// <summary>
    /// Provides methods for validating customer data such as Email and Phone Number.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Represents the result of a validation check.
        /// </summary>
        public class ValidationResult
        {
            /// <summary>
            /// True if the value is valid, false otherwise.
            /// </summary>
            public bool IsValid { get; set; }

            /// <summary>
            /// A message describing the validation outcome.
            /// </summary>
            public string Message { get; set; }
        }

        /// <summary>
        /// Validates whether the given email string is in a correct format.
        /// </summary>
        /// <param name="email">The email address to validate (e.g., abhishek.tamboli@example.com).</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Email cannot be empty."
                };
            }

            // Regex pattern for standard email validation.
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isValid = Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);

            return new ValidationResult
            {
                IsValid = isValid,
                Message = isValid ? $"Email '{email}' is valid." : $"Email '{email}' is not in a valid format."
            };
        }

        /// <summary>
        /// Validates whether the given phone number string matches a US format.
        /// Supported formats include:
        /// - (123) 456-7890
        /// - 123-456-7890
        /// - 123.456.7890
        /// - 1234567890
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate (e.g., +1 (732) 555-1234).</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Message = "Phone number cannot be empty."
                };
            }

            // Regex for US phone numbers with optional country code (+1).
            string pattern = @"^(\+1\s?)?(\([0-9]{3}\)|[0-9]{3})[-.\s]?[0-9]{3}[-.\s]?[0-9]{4}$";
            bool isValid = Regex.IsMatch(phoneNumber, pattern);

            return new ValidationResult
            {
                IsValid = isValid,
                Message = isValid ? $"Phone number '{phoneNumber}' is valid." : $"Phone number '{phoneNumber}' is not valid."
            };
        }
    }
}
