using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Console.UI
{
    
    public interface IDisplay
    {
       public Task ProcessAnagramManager();
     //   public void FormattedPrint(FormPrint form, List<string> input); //-> Delegate
        public void FormattedPrint(List<string> input);  //-> Action
    }
}
