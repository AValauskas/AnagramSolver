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
            _configBuilder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"AnagramSolver.Console"))
            //.SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
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
    }
    
}
