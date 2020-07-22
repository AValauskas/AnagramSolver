using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnagramSolver.BusinessLogic.Utils
{
    public class Settings
    {
        public static IConfigurationBuilder configBuilder { get; set; }
        public Settings()
        {
            CallBuilder();
        }

        public void CallBuilder()
        {
            configBuilder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
            .AddJsonFile("appsettings.json");
        }


    }
}
