using Newtonsoft.Json;
using System.Text;

namespace PF.Tests.Integration.Helpers
{
    public static class JsonHelper
    {
        public static StringContent ToStringContent(object rawObject)
        {
            return new StringContent(
                JsonConvert.SerializeObject(
                    rawObject,
                    Formatting.Indented,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                Encoding.UTF8, "application/json");
        }
    }
}
