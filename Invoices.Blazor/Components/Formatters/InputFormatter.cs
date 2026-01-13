namespace Invoices.Blazor.Components.Formatters
{
    public static class InputFormatter
    {
        /// <summary>
        /// Formats a numeric input by inserting a space after a specified number of digits.
        /// Non-digit characters are removed before formatting.
        /// </summary>
        /// <param name="value">
        /// The raw input value provided by the user. May contain spaces or other characters.
        /// </param>
        /// <param name="position">
        /// The index (1-based) after which a space should be inserted. Must be non-negative.
        /// </param>
        /// <returns>
        /// A formatted string containing only digits, with a space inserted after the specified position
        /// if the input contains enough digits. If the input is null or empty, returns an empty string.
        /// </returns>
        public static string InsertSpaceAfter(string value, int position)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            // Remove everything except digits
            var digits = new string(value.Where(char.IsDigit).ToArray());

            // Insert space after the specified number of digits
            if (digits.Length > position)
                digits = digits.Insert(position, " ");

            return digits;
        }

        /// <summary>
        /// Formats a Czech postal code (PSČ) into the standard "XXX XX" format.
        /// </summary>
        public static string FormatPostalCode(string value)
            => InsertSpaceAfter(value, 3);

        /// <summary>
        /// Formats a Czech phone number into the "XXX XXX XXX" format.
        /// </summary>
        public static string FormatTelephone(string value)
        {
            var formatted = InsertSpaceAfter(value, 3);
            return InsertSpaceAfter(formatted, 7);
        }

      
    }

}
