using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace FullStack.Core.Infrastructure
{
    public class JsonConfig
    {
        #region Public Properties

        public JsonMediaTypeFormatter MediaTypeFormatter => new JsonMediaTypeFormatter
        {
            SerializerSettings = JsonConvert.DefaultSettings()
        };

        #endregion

        #region Public Static Methods

        public static JsonConfig Configure()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                settings.Converters.Add(new StringEnumConverter());
                settings.Converters.Add(new IsoDateTimeConverter());

                return settings;
            };

            return new JsonConfig();
        }

        #endregion
    }
}
