using CSM.MedicalInsurance.Assignment.Utility;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace CSM.MedicalInsurance.Assignment.Web
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this TempDataDictionary tempData, T value) where T : class
        {
        }

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[typeof(T).FullName + key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData) where T : class
        {
            object o;
            tempData.TryGetValue(typeof(T).FullName, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>(o.ParseToText());
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(typeof(T).FullName + key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>(o.ParseToText());
        }
        public static bool TryFetch<T>(this ITempDataDictionary tempData, string key, out T result) where T : class
        {
            object o;
            result = default;
            if (tempData.TryGetValue(typeof(T).FullName + key, out o))
            {
                result = JsonConvert.DeserializeObject<T>(o.ParseToText());
                return true;
            }
            return false;
        }
    }
}
