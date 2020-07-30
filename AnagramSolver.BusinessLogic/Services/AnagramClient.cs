using AnagramSolver.Contracts.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramClient: IAnagramClient
    {
        private const string URL = "https://localhost:44321";
        private HttpClient client;
        public AnagramClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
        }
        public async Task<List<string>> GetAnagrams(string word)
        {
            var URLParameter = "/api/Anagram/" + word;

            var response = await client.GetAsync(URL + URLParameter).ConfigureAwait(false);
           
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<string>>(dataObjects);
            }
            return null;
        }
    }
}
//configureawait