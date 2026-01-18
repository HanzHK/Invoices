using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Invoices.Blazor.InputHandlers
{
    public static class NumbersInputHandler
    {
        /// <summary>
        /// Cleans raw user input by removing all non-digit characters
        /// and formats the remaining digits into blocks defined by the given lengths.
        /// </summary>
        /// <param name="rawInput">User input that may contain any characters.</param>
        /// <param name="blocks">Block sizes used to format the digits (e.g., 3-3-3).</param>
        /// <returns>Formatted numeric string or an empty string if input is null or empty.</returns>
        public static string Handle(string rawInput, int[] blocks)
        {
            if (string.IsNullOrEmpty(rawInput))
                return string.Empty;

            // Remove all non-digits and spaces
            var digits = new string(rawInput.Where(char.IsDigit).ToArray());

            // Apply block formatting
            return ApplyBlocks(digits, blocks);
        }

        /// <summary>
        /// Splits a digit-only string into blocks defined by the given lengths
        /// and joins them with spaces.
        /// </summary>
        /// <param name="digits">Cleaned numeric input without non-digit characters.</param>
        /// <param name="blocks">Sequence of block sizes (e.g., 3-3-3 for phone numbers).</param>
        /// <returns>
        /// Formatted string with blocks separated by spaces. If blocks are empty
        /// or input has no digits, returns the original digits.
        /// </returns>
        private static string ApplyBlocks(string digits, int[] blocks)
        {
            if (blocks.Length == 0 || digits.Length == 0)
                return digits;

            int index = 0;
            var parts = new List<string>();

            foreach (var block in blocks)
            {
                if (index >= digits.Length)
                    break;

                var length = Math.Min(block, digits.Length - index);
                parts.Add(digits.Substring(index, length));
                index += length;
            }

            // Add remaining digits if any
            if (index < digits.Length)
                parts.Add(digits.Substring(index));

            return string.Join(" ", parts);
        }
    }
}