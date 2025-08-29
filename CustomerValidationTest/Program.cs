using System;
using CustomerValidation;  // Reference to your class library namespace

class Program
{
    static void Main(string[] args)
    {
        var validator = new Validator();

        // Replace these with your real values for testing
        string email = "abhishek.tamboli@example.com";
        string phone = "123-456-7890";

        var emailResult = validator.ValidateEmail(email);
        var phoneResult = validator.ValidatePhoneNumber(phone);

        Console.WriteLine("=== Customer Validation Results ===");
        Console.WriteLine($"Email Validation: {(emailResult.IsValid ? "✔ Verified" : "❌ Not Verified")} - {emailResult.Message}");
        Console.WriteLine($"Phone Validation: {(phoneResult.IsValid ? "✔ Verified" : "❌ Not Verified")} - {phoneResult.Message}");
        Console.WriteLine("===================================");
    }
}
