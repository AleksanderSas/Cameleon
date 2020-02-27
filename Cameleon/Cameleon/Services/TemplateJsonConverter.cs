using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Cameleon.Services
{
    public class TemplateJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ITemplate).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            // Using a nullable bool here in case "is_album" is not present on an item
            string templateType = ((string)jo["templateType"])?.ToLower();

            ITemplate item;
            switch (templateType)
            {
                case "":
                default:
                    item = new SimpleTemplate();
                    break;
            }

            serializer.Populate(jo.CreateReader(), item);

            return item;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            base.WriteJson(writer, value, serializer);
        }
    }
}
