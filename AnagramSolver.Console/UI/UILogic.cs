using AnagramSolver.BusinessLogic.Utils;

namespace AnagramSolver.Console.UI
{
    public class UILogic
    {
        public static bool CheckIfLengthCorrect(string myWord)
        {
            if (myWord == null)
                return false;
            int minLength = Settings.MinLength;
            if (myWord.Length >= minLength)
                return true;
            return false;
        }
    }
}
