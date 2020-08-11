using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Services
{
    public interface IRestrictionService
    {
        Task<bool> CheckIfActionCanBePerformed();
    }
}
