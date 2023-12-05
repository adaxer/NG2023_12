using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MovieBase.Common;

public class MovieDTO
{
    public int Id { get; set; }

    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Info { get; set; }
}
