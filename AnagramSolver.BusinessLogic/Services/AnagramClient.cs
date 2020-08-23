using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramClient: IAnagramSolver
    {
        private const string URL = "https://localhost:44321";
        private HttpClient client;
        public AnagramClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
        }
        public async Task<List<WordModel>> GetAnagrams(string word)
        {
            var URLParameter = "/api/Anagram/" + word;

            var response = await client.GetAsync(URL + URLParameter).ConfigureAwait(false);
           
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<WordModel>>(dataObjects);
            }
            return null;
        }
    }
}