using Newtonsoft.Json;

namespace CompanyApp.Converter;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String && TimeSpan.TryParse((string?)reader.Value, out var timeSpan))
        {
            return new TimeOnly(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        throw new JsonSerializationException("Invalid format for TimeOnly");
    }
}
