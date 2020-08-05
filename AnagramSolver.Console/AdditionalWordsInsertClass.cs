using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Data;
using AnagramSolver.EF.DatabaseFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Console
{
    public class AdditionalWordsInsertClass
    {
    
        public static void InsertWords()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AnagramSolverDBContext>(options => options.UseSqlServer(Settings.ConnectionString));
            var serviceProvider = services.BuildServiceProvider();

            IWordRepository fileWords = new WordRepository();
            IWordRepository databasewords = new DatabaseWordRepository();
            
           // IWordRepository eFdatabasewords = new AnagramSolver.EF.DatabaseFirst.Repositories.WordRepository(serviceProvider.GetService<AnagramSolverDBContext>());
            var words = fileWords.GetAllWords();
            foreach (var word in words)
            {
               // eFdatabasewords.AddWordToDataSet(word.Word, word.LanguagePart);
            }
            

        }
  
    }
}
