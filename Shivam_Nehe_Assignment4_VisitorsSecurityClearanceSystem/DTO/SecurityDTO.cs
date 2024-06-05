using Newtonsoft.Json;

namespace SecurityClearanceSystem.DTO
{
    public class SecurityDTO
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; } // Unique identifier for the security personnel
        
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "badgeNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string BadgeNumber { get; set; }

        [JsonProperty(PropertyName = "department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]

        public string Role { get; set; }
    }
}
