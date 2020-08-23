using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramicaClient : IAnagramSolver
    {
        private const string URL = "http://www.anagramica.com";
        private HttpClient client;
        public AnagramicaClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
        }
        public async Task<List<WordModel>> GetAnagrams(string word)
        {
            var URLParameter = "/all/" + word;

            var response = await client.GetAsync(URL + URLParameter).ConfigureAwait(false);
           
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var wordsList = JsonConvert.DeserializeObject<AnagramicaObject>(dataObjects);
                return FormWordModelList(wordsList);
            }
            return new List<WordModel>();
        }

        private List<WordModel> FormWordModelList(AnagramicaObject wordsList)
        {
            var wordModelList = new List<WordModel>();
            foreach (var item in wordsList.all)
            {
                var wordModel = new WordModel() { Word = item };
                wordModelList.Add(wordModel);
            }
            return wordModelList;
        }

    }
}