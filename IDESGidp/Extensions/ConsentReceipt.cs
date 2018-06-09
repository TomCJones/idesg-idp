// ConsentReceipt helper clases (c) 2018 tom jones

using System;
using Newtonsoft.Json;

namespace IDESGidp.Services
{
    public class ConsentReceipt
    {
        [JsonObject]
        public class ProfileResponse
        {
            [JsonProperty]
            public string version { get; set; }
            [JsonProperty]
            public string jurisdiction { get; set; }
            [JsonProperty]
            public string consentTimestamp { get; set; }
            [JsonProperty]
            public string collectionMethod { get; set; }
            [JsonProperty]
            public string consentReceiptID { get; set; }
            [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
            public string publicKey { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string language { get; set; }
            [JsonProperty]
            public string piiPrincipalId { get; set; }
            [JsonProperty]
            public Array piiControllers { get; set; }
            [JsonProperty]
            public string policyUrl { get; set; }
            [JsonProperty]
            public Array services { get; set; }
            [JsonProperty]
            public string sensitive { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string spiCat { get; set; }
        }

        [JsonObject]
        public class jsonController
        {
            public static jsonController Create(string json)
            {
                return new jsonController(json);
            }
            public static string Write(jsonController options)
            {
                return JsonConvert.SerializeObject(options);
            }
            public jsonController()
            { }
            public jsonController(string json)
            {
                try
                {
                    JsonConvert.PopulateObject(json, this);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error deserializing json:'{0}', into '{1}.", json, GetType()), ex);
                }
            }
            [JsonProperty]
            public string piiController { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string onBehalf { get; set; }
            [JsonProperty]
            public string contact { get; set; }
            [JsonProperty]
            public string address { get; set; }
            [JsonProperty]
            public string email { get; set; }
            [JsonProperty]
            public string phone { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string fax { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string piiControllerURL { get; set; }
        }

        [JsonObject]
        public class jsonService
        {
            [JsonProperty]
            public string service { get; set; }
            [JsonProperty]
            public Array purposes { get; set; }
        }

        [JsonObject]
        public class jsonPurpose
        {
            [JsonProperty]
            public string purpose { get; set; }
            [JsonProperty]
            public string[] purposeCategory { get; set; }
             [JsonProperty]
            public string consentType { get; set; }
            [JsonProperty]
            public string[] piiCategory { get; set; }
            [JsonProperty]
            public bool primaryPurpose { get; set; }
            [JsonProperty]
            public string termination { get; set; }
            [JsonProperty]
            public bool thirdPartyDisclosure { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string thirdPartyName { get; set; }
        }
    }
}
