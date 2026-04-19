using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Invoices.Blazor.Utils.Converters
{
    /// <summary>
    /// JSON converter for <see cref="DateOnly"/> values.
    /// Serializes dates using the ISO format "yyyy-MM-dd" and deserializes
    /// from the same format. Required because System.Text.Json does not
    /// provide native support for DateOnly.
    /// </summary>
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        /// <summary>
        /// Reads a JSON string and converts it into a <see cref="DateOnly"/> value.
        /// The input must be in ISO format (yyyy-MM-dd).
        /// </summary>
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        /// <summary>
        /// Writes a <see cref="DateOnly"/> value as a JSON string
        /// using the ISO format "yyyy-MM-dd".
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
