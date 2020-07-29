using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AnagramSolver.BusinessLogic.Utils
{
    public class Settings
    {
        public static IConfigurationBuilder _configBuilder { get; set; }
        static Settings()
        {
            CallBuilder();
        }

        private static void CallBuilder()
        {
            string path = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"AnagramSolver.Console");
            string path2 = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console");
            _configBuilder = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\" + @"AnagramSolver.Console")))
            .AddJsonFile("appsettings.json");
        }

        public static string GetAnagramsCount()
        {
            return _configBuilder.Build().GetSection("anagramCount").Value;
        }

        public static int GetMinLength()
        {
            return int.Parse(_configBuilder.Build().GetSection("minLength").Value);
        }

        public static int GetPageSize()
        {
            return int.Parse(_configBuilder.Build().GetSection("pageSize").Value);
        }
    }
    
}
