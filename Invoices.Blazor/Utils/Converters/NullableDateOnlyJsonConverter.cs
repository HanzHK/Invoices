using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Invoices.Blazor.Utils.Converters
{
    /// <summary>
    /// JSON converter for nullable <see cref="DateOnly"/> values.
    /// Provides serialization and deserialization support for the ISO
    /// date format "yyyy-MM-dd". Required because System.Text.Json does
    /// not natively support <see cref="DateOnly"/> or <see cref="DateOnly?"/>.
    /// </summary>
    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private const string Format = "yyyy-MM-dd";

        /// <summary>
        /// Reads a JSON token and converts it into a nullable
        /// <see cref="DateOnly"/> value. Supports both ISO date strings
        /// and null values.
        /// </summary>
        /// <param name="reader">The JSON reader positioned at the value.</param>
        /// <param name="typeToConvert">The target type (<see cref="DateOnly?"/>).</param>
        /// <param name="options">Serializer options.</param>
        /// <returns>
        /// A <see cref="DateOnly"/> value if the JSON contains a valid date string;
        /// otherwise <c>null</c>.
        /// </returns>
        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return null;

            return DateOnly.Parse(value);
        }

        /// <summary>
        /// Writes a nullable <see cref="DateOnly"/> value to JSON using
        /// the ISO format "yyyy-MM-dd". Writes a JSON null when the value
        /// is <c>null</c>.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">Serializer options.</param>
        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString(Format));
            else
                writer.WriteNullValue();
        }
    }
}
