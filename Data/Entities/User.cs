using System.Text.Json.Serialization;

namespace Data.Entities
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
    }
}
