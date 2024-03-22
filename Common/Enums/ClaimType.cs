using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Common.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ClaimType
{
    Id,
    IsAdmin
}
