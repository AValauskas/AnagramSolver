using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.Data
{
    public class WordRepository : IWordRepository
    {
        private readonly string filePath;
        private  Dictionary<string, List<Word>> anagrams; 
        public WordRepository()
        {
            anagrams = new Dictionary<string, List<Word>>();
            filePath = Settings.FilePath;
            ReadFile();

        }
        public WordRepository(Dictionary<string, List<Word>> anagram)
        {
            anagrams = anagram;
        }

        private void ReadFile()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
            string lastWord="";
            string sortedWord = "";
     
            using (FileStream fs = File.Open(path, FileMode.Open))            
            using (StreamReader sr = new StreamReader(fs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] row = s.Split("\t");                   
                    sortedWord = String.Concat(row[0].ToLower().OrderBy(c => c));
                    
                    if (sortedWord != lastWord)
                    {
                        AddWord( sortedWord, row[0], row[1]);
                    }
                    lastWord = sortedWord;
                }
            }
        }
        private void WriteFile(string word, string languagePart)
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteAsync("\n"+word+"\t" + languagePart);
            }
        }
        public async Task<bool> AddWordToDataSet(string word, string languagePart)
        {
            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            if (AddWord(sortedWord, word, languagePart))
                WriteFile(word, languagePart);
            else
                return false;

            return true;
        }

        public async Task <Dictionary<string, List<Word>>> GetWords()
        {
                return anagrams;
        }
        public async Task<IEnumerable<Word>> GetAllWords()
        {
            return anagrams.Values.ToList().SelectMany(x => x).ToList();
        }

        public async Task<IEnumerable<Word>> GetWordsByRange(int pageIndex, int range)
        {
            var allWordList = await GetAllWords();
            return allWordList.Skip((pageIndex - 1) * range).Take(range).ToList();
        }

        public async Task<int> GetTotalWordsCount()
        {
            var words = await GetAllWords();
            var count = words.ToList().Count;
            return count;
        }

        private bool AddWord(string sortedWord, string word, string languagePart)
        {
            if (anagrams.ContainsKey(sortedWord))
            {
                if (anagrams[sortedWord].Select(x=>x.Word1).Contains(word))
                {
                    return false;
                }
                anagrams[sortedWord].Add(
                    new Word()
                    {
                        Word1 = word,
                        Category = languagePart,
                        SortedWord= sortedWord
                    });
            }
            else
            {
                anagrams.Add(
                    sortedWord, new List<Word>() {
                        new Word() {
                            Word1 = word,
                            Category = languagePart,
                            SortedWord= sortedWord
                        }});
            }
            return true;
        }

        public async Task<IEnumerable<Word>> FindSingleWordAnagrams(string sortedWord)
        {
            var anagramsCount = Settings.AnagramCount;
            var allWords = await GetWords();
            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord];
            }
            return null;
        }

        public async Task<IEnumerable<Word>> SearchWords(string word)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Word>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetWordsCountBySerachedWord(string searchedWord)
        {
            throw new NotImplementedException();
        }

        public Task AddManyWordsToDataSet(List<Word> words)
        {
            throw new NotImplementedException();
        }

        public Task<Word> GetWordByName(string word)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWordByName(string word)
        {
            throw new NotImplementedException();
        }

        public Task<Word> GetWordById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Word> UpdateWord(Word word)
        {
            throw new NotImplementedException();
        }
    }
}
