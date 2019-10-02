using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Movies.Core.Common.Formatters
{
    public class DoubleValueConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return double.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            if (!value.HasValue) return;
            var roundedValue = Math.Round(value.Value * 2, MidpointRounding.AwayFromZero) / 2;
            writer.WriteStringValue(roundedValue.ToString("F1"));
        }
    }
}