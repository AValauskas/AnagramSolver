using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AnagramSolver.BusinessLogic.Utils
{
    public class Settings
    {
        private const int countResults = 10;
        public static IConfigurationBuilder _configBuilder { get; set; }
        public static int AnagramCount { get; private set; }
        public static int MinLength { get; private set; }
        public static int PageSize { get; private set; }
        public static string FilePath { get; private set; }
        static Settings()
        {
            CallBuilder();
        }

        private static void CallBuilder()
        {
            _configBuilder = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(AppContext.BaseDirectory))
            .AddJsonFile("appsettings.json");
            AnagramCount = GetSettingsJsonIntValue("anagramCount", countResults);
            MinLength = GetSettingsJsonIntValue("minLength", countResults);
            PageSize = GetSettingsJsonIntValue("pageSize", countResults);
            FilePath= _configBuilder.Build().GetSection("filePath").Value;
        }

        private static int GetSettingsJsonIntValue(string field, int failureCaseInt)
        {
            var anagramCount = _configBuilder.Build().GetSection(field).Value;
            int result;
            if (!int.TryParse(anagramCount, out result))
                result = failureCaseInt;

            return result;
        }
    }
    
}
