using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Getnet.Infrastructure.Configurations.Serialization;

public class JsonEnumMemberConverter<T> : JsonConverter<T> where T : struct, Enum
{
    private readonly Dictionary<T, string> _enumToString = new();
    private readonly Dictionary<string, T> _stringToEnum = new(StringComparer.OrdinalIgnoreCase);

    public JsonEnumMemberConverter()
    {
        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var enumValue = (T)field.GetValue(null)!;
            var name = field.Name;
            var enumMemberAttr = field.GetCustomAttribute<EnumMemberAttribute>();
            var stringValue = enumMemberAttr?.Value ?? name;

            _enumToString[enumValue] = stringValue;
            _stringToEnum[stringValue] = enumValue;
        }
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (str != null && _stringToEnum.TryGetValue(str, out var value))
        {
            return value;
        }

        throw new JsonException($"Unable to convert \"{str}\" to Enum \"{typeof(T)}\"");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (_enumToString.TryGetValue(value, out var stringValue))
        {
            writer.WriteStringValue(stringValue);
        }
        else
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}