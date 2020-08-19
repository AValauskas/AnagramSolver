using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AnagramSolver.WebService
{
    [ServiceContract]
    public interface IAnagramService
    {
        [OperationContract]
        Task<List<string>> GetAnagrams(string word);
    }
}
