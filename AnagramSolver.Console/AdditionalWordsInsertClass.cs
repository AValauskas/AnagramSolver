using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.DatabaseLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Console
{
    public class AdditionalWordsInsertClass
    {
        public static void InsertWords()
        {
            IWordRepository fileWords = new WordRepository();
            IWordRepository databasewords = new DatabaseWordRepository();
            var words = fileWords.GetAllWords();
          //  databasewords.GetAllWords();
            foreach (var word in words)
            {
                databasewords.AddWordToDataSet(word.Word, word.LanguagePart);
            }
        }
  
    }
}
