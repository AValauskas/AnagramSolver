using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AnagramSolver.BusinessLogic.Utils
{
    public static class Helper
    {
        public static string GetAnagramsCount()
        {
           return Settings.configBuilder.Build().GetSection("anagramCount").Value;
        }

        public static int GetMinLength()
        {
            return int.Parse(Settings.configBuilder.Build().GetSection("minLength").Value);
        }

    }
}
