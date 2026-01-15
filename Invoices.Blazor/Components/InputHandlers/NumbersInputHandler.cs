using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Invoices.Blazor.Components.InputHandlers
{
    public static class NumbersInputHandler
    {
        public static string Handle(string rawInput, int[] blocks)
        {
            if (string.IsNullOrEmpty(rawInput))
                return string.Empty;

            // Remove all non-digits and spaces
            var digits = new string(rawInput.Where(char.IsDigit).ToArray());

            // Apply block formatting
            return ApplyBlocks(digits, blocks);
        }

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