using Newtonsoft.Json;
using System.Net.Http;
using System;
namespace StudentDataApi.Logics
{
    public class HttpHelper
    {
        public readonly HttpClient _httpClient;
        public HttpHelper(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public bool PostData(object data, string url)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            using (var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
                if (result.IsSuccessStatusCode)
                    return true;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
            }
        }

        public bool PutData(object data, string URL)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            using (var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result =  _httpClient.PutAsync(URL, content).Result;
                if (result.IsSuccessStatusCode)
                    return true;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
              
            }
        }

        public bool DeleteData(string URL)
        {
           
                HttpResponseMessage result = _httpClient.DeleteAsync(URL).Result;
               if (result.IsSuccessStatusCode)
                return true;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
           
        }
    }
}
