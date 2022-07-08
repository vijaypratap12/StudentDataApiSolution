using Newtonsoft.Json;

namespace StudentDataApi.Logics
{
    public class UserHelper
    {
        private readonly HttpClient _httpClient;
        
        public UserHelper(HttpClient httpClient)
        {
          
                _httpClient = httpClient;
        }

        
        public bool RegisterUser(object data, string url)
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

        public bool LoginUser(object data, string url)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            using (var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
                // var result = _httpClient.PostAsync(url, content).Result;
                if (result.IsSuccessStatusCode)
                    return true;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
            }
        }
    }
}
