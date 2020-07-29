using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Interfaces.Services
{
    public interface IRequestService
    {
        public List<string> GetAnagramRequest(string word);
    }
}
