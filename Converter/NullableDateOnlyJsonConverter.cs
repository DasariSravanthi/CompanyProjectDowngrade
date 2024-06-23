using Newtonsoft.Json;

namespace CompanyApp.Converter;

public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
{
    private readonly string _dateFormat;

    public NullableDateOnlyJsonConverter(string dateFormat = "yyyy-MM-dd")
    {
        _dateFormat = dateFormat;
    }

    public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value.ToString(_dateFormat));
        }
        else
        {
            writer.WriteNull();
        }
    }

    public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        // Read the date string from the JSON
        var dateString = reader.Value?.ToString();

        // Ensure dateString is not null before attempting to parse
        if (string.IsNullOrEmpty(dateString))
        {
            return null;
        }

        // Attempt to parse the string to a DateOnly object
        if (DateOnly.TryParseExact(dateString, _dateFormat, null, System.Globalization.DateTimeStyles.None, out var dateOnly))
        {
            return dateOnly;
        }

        throw new JsonException($"Unable to convert \"{dateString}\" to DateOnly.");
    }
}
