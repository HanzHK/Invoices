using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Invoices.Api.Converters
{
    /// <summary>
    /// JSON konvertor pro typ DateOnly.
    /// Umožňuje serializaci a deserializaci hodnot ve formátu "yyyy-MM-dd".
    /// </summary>
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.ParseExact(value!, format);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}
