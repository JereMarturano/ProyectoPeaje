using System;
using System.Text.RegularExpressions;
using TollSystem.Domain.Exceptions;

namespace TollSystem.Domain.ValueObjects
{
    public class LicensePlate
    {
        public string Value { get; }

        // Regex supports formats like "AA 123 BB" and "AAA 123"
        private static readonly Regex MercosurRegex = new Regex(@"^([A-Z]{2}\s\d{3}\s[A-Z]{2})|([A-Z]{3}\s\d{3})$", RegexOptions.IgnoreCase);

        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !MercosurRegex.IsMatch(value))
            {
                throw new InvalidLicensePlateFormatException($"The license plate '{value}' has an invalid format.");
            }
            Value = value.ToUpper();
        }

        public override string ToString() => Value;

        // Implicit conversion to string for convenience
        public static implicit operator string(LicensePlate plate) => plate.Value;
    }
}
