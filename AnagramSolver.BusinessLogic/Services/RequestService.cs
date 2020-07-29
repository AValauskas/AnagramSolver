using AnagramSolver.Contracts.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AnagramSolver.BusinessLogic.Services
{
    public class RequestService: IRequestService
    {
        const string URL = "https://localhost:44321";

        public List<string> GetAnagramRequest(string word)
        {
            var URLParameter = "/api/Anagram/" + word;

            var response = ProcessGetRequest(URLParameter);
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(dataObjects.Result);
            }
            return null;
        }
        private HttpResponseMessage ProcessGetRequest(string URLParameter)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            return client.GetAsync(URL + URLParameter).Result;
        }
    }
}
