// WebAuthN helper clases (c) 2018 tom jones

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IDESGidp.Services
{
    public class WebAuthnHelperExt
    {
        [JsonObject]
        public class RegResponse {

            [JsonProperty] 
            public string RegistrationData { get; set; }
            [JsonProperty]
            public string AppId { get; set; }
            [JsonProperty]
            public string Challenge { get; set; }
            [JsonProperty]
            public string Version { get; set; }
            [JsonProperty]
            public string ClientData { get; set; }
        }

        [JsonObject]
        public class PublicKeyCredentialCreationOptions {

            public static PublicKeyCredentialCreationOptions Create(string json)
            {
                return new PublicKeyCredentialCreationOptions(json);
            }
            public static string Write(PublicKeyCredentialCreationOptions options)
            {
                return JsonConvert.SerializeObject(options);
            }
            public PublicKeyCredentialCreationOptions()
            { }
            public PublicKeyCredentialCreationOptions(string json)
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
            public string PublicKeyCredentialRpEntity { get; set; }
            [JsonProperty]
            public string PublicKeyCredentialUserEntity { get; set; }
            [JsonProperty]
            public string ChallengeBuffer { get; set; }
            [JsonProperty]
            public long Timeout { get; set; }
            [JsonProperty]
            public string AuthenticatorSelectionCriteria { get; set; }
            [JsonProperty]
            public string AttestationConveyancePreference { get; set; }
            [JsonProperty]
            public IDictionary<string, object> AuthenticationExtensionsClientInputs { get; set; }
        }
    }
}
