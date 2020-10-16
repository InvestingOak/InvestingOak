using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvestingOak.Converters
{
    public class Int32Converter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                if (int.TryParse(reader.GetString(), out int d))
                    return d;

            return default;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
