using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Invoices.Blazor.Utils.Converters
{
    public class UpperCaseEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return Enum.Parse<T>(value, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToUpperInvariant());
        }
    }
}
