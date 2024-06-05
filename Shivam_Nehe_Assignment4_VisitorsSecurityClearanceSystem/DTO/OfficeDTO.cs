using Newtonsoft.Json;

namespace SecurityClearanceSystem.DTO
{
    public class OfficeDTO
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "officeName", NullValueHandling = NullValueHandling.Ignore)]

        public string OfficeName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]

        public string Address { get; set; }

        [JsonProperty(PropertyName = "licensenumber", NullValueHandling = NullValueHandling.Ignore)]
        public string Licensenumber { get; set; }


        [JsonProperty(PropertyName = "contactdetails", NullValueHandling = NullValueHandling.Ignore)]
        public string Contactdetails { get; set; }
    }
}
