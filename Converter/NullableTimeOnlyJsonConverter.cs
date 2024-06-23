using Newtonsoft.Json;

namespace CompanyApp.Converter;

public class NullableTimeOnlyJsonConverter : JsonConverter<TimeOnly?>
{
    private readonly string _timeFormat;

    public NullableTimeOnlyJsonConverter(string timeFormat = "HH:mm:ss")
    {
        _timeFormat = timeFormat;
    }

    public override void WriteJson(JsonWriter writer, TimeOnly? value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value.ToString(_timeFormat));
        }
        else
        {
            writer.WriteNull();
        }
    }

    public override TimeOnly? ReadJson(JsonReader reader, Type objectType, TimeOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var timeString = reader.Value?.ToString();

        if (string.IsNullOrEmpty(timeString))
        {
            return null;
        }

        if (TimeOnly.TryParseExact(timeString, _timeFormat, null, System.Globalization.DateTimeStyles.None, out var timeOnly))
        {
            return timeOnly;
        }

        throw new JsonException($"Unable to convert \"{timeString}\" to TimeOnly.");
    }
}
