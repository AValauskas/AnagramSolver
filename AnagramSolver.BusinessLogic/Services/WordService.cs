﻿using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepositoryEF _wordRepository;
        private readonly IMapper _mapper;
        public WordService(IWordRepositoryEF wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddWordToDataSet(string word, string languagePart)
        {
            var foundWord = await _wordRepository.GetWordByName(word);
            if (foundWord != null)            
                return false;            

            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            var wordEntity = new WordEntity()
            {
                Word = word,
                Category= languagePart,
                SortedWord = sortedWord
            };
            await _wordRepository.AddWordToDataSet(wordEntity);
            return true;
        }

        public async Task<IEnumerable<WordModel>> GetAllWords()
        {
            var repoWords = await _wordRepository.GetAllWords();

            var words = _mapper.Map<List<WordModel>>(repoWords);
            return words;
        }

        public async Task<int> GetTotalWordsCount(string searchedWord)
        {
            if (!String.IsNullOrEmpty(searchedWord))
            {
                return await _wordRepository.GetWordsCountBySerachedWord(searchedWord);
            }
            return await _wordRepository.GetTotalWordsCount();
        }

        public async Task<Dictionary<string, List<WordModel>>> GetWords()
        {
            var dictionary = new Dictionary<string, List<WordModel>>();
            var dictionaryRepo = await _wordRepository.GetWords();
           
            foreach (var words in dictionaryRepo)
            {
                var wordsList = words.Value;
                var mappedWord = _mapper.Map<List<WordModel>>(wordsList);

                dictionary.Add(words.Key, mappedWord);
            }
            return dictionary;
        }

        public async Task<IEnumerable<WordModel>> GetWordsByRange(int pageIndex, int range)
        {
            var repoWords = await _wordRepository.GetWordsByRange(pageIndex, range);          
            var words = _mapper.Map<List<WordModel>>(repoWords);           
            return words;
        }

        public async Task<FileStreamResult> GetDictionaryFile()
        {
            return await Task.Run(() => {
                string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, Settings.FilePath));
                var stream = File.OpenRead(path);
                if (stream == null)
                {
                    throw new BusinessException("File doesn't Exist");
                }
                var file = new FileStreamResult(stream, "application/octet-stream")
                {
                    FileDownloadName = "Zodynas.txt"
                };

                return file;
            });
            
        }

        public async Task<IEnumerable<WordModel>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            var repoWords = await _wordRepository.SearchWordsByRangeAndFilter(pageIndex, range, searchedWord);
            var words = _mapper.Map<List<WordModel>>(repoWords);
            return words;
        }

        public async Task DeleteWordByName(string word)
        {
            await _wordRepository.DeleteWordByName(word);
        }
        public async Task<WordModel> GetWordByName(string word)
        {
            var wordRepo = await _wordRepository.GetWordByName(word);
            var wordModel = _mapper.Map<WordModel>(wordRepo);
            return wordModel;
        }

        public async Task<bool> UpdateWord(string word, string languagePart, int id)
        {
            var foundWord = await _wordRepository.GetWordByName(word);
            if (foundWord!=null)                      
                if (foundWord.Id != id)            
                    return false;               
                       

            await _wordRepository.UpdateWord(word, languagePart,id);          
            return true;
        }

        public async Task<WordModel> GetWordByID(int id)
        {
            var wordRepo = await _wordRepository.GetWordById(id);
            var wordModel = _mapper.Map<WordModel>(wordRepo);
            return wordModel;
        }

        public async  Task<int> FillDataBase()
        {
           var count = await _wordRepository.FillDataBase();
            return count;
        }
    }
}
