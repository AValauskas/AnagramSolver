using AnagramSolver.BusinessLogic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Console.UI
{
    public class UILogic
    {
        public static bool CheckIfLengthCorrect(string myWord)
        {
            if (myWord == null)
                return false;
            int minLength = Settings.GetMinLength();
            if (myWord.Length >= minLength)
                return true;
            return false;
        }
    }
}
