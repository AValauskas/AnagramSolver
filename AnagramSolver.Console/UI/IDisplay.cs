using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static AnagramSolver.Console.UI.DisplayWithEvents;

namespace AnagramSolver.Console.UI
{
    
    public interface IDisplay
    {
       public Task ProcessAnagramManager();
     //   public void FormattedPrint(FormPrint form, List<string> input); //-> Delegate
        public void FormattedPrint(Action<List<string>> form, List<string> input);  //-> Action
    }
}
