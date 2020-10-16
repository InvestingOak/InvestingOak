using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvestingOak.Converters
{
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                if (DateTimeOffset.TryParse(reader.GetString(), out DateTimeOffset dt))
                    return dt;

            return default;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
