using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.CodeFirst.Models;

namespace AnagramSolver.Data
{
    public class WordRepository : IWordRepository
    {
        private readonly string filePath;
        private readonly Dictionary<string, List<WordEntity>> anagrams; 
        public WordRepository()
        {
            anagrams = new Dictionary<string, List<WordEntity>>();
            filePath = Settings.FilePath;
            ReadFile();

        }
        public WordRepository(Dictionary<string, List<WordEntity>> anagram)
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

        public async Task <Dictionary<string, List<WordEntity>>> GetWords()
        {
                return anagrams;
        }
        public async Task<IEnumerable<WordEntity>> GetAllWords()
        {
            return anagrams.Values.ToList().SelectMany(x => x).ToList();
        }

        public async Task<IEnumerable<WordEntity>> GetWordsByRange(int pageIndex, int range)
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
                if (anagrams[sortedWord].Select(x=>x.Word).Contains(word))
                {
                    return false;
                }
                anagrams[sortedWord].Add(
                    new WordEntity()
                    {
                        Word = word,
                        Category = languagePart,
                        SortedWord= sortedWord
                    });
            }
            else
            {
                anagrams.Add(
                    sortedWord, new List<WordEntity>() {
                        new WordEntity() {
                            Word = word,
                            Category = languagePart,
                            SortedWord= sortedWord
                        }});
            }
            return true;
        }

        public async Task<IEnumerable<WordEntity>> FindSingleWordAnagrams(string sortedWord)
        {
            var allWords = await GetWords();
            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord];
            }
            return null;
        }

        public async Task<IEnumerable<WordEntity>> SearchWords(string word)
        {
            var allWords = await GetAllWords();

            var words = allWords
               .Where(x => x.Word.Contains(word));
            return words;
        }

        public async Task<IEnumerable<WordEntity>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            var allWords = await GetAllWords();

            var skip = (pageIndex - 1) * range;
            var words = allWords
                .Where(x => x.Word.StartsWith(searchedWord))
                .Skip(skip)
                .Take(range);
            return words;
        }

        public async Task<int> GetWordsCountBySerachedWord(string searchedWord)
        {
            var words = await GetAllWords();
           
            var count = words
               .Where(x => x.Word.Contains(searchedWord))
               .Count();
            return count;
        }

    }
}
