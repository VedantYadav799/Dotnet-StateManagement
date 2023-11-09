using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;


namespace StateManagement.Helpers
{
    public static class SessionHelper{
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if(value != null){
               return JsonConvert.DeserializeObject<T>(value);
            }
            else
            return default(T) ;
        }


    } 
}