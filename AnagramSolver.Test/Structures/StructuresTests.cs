
using NUnit.Framework;
using Structures;

namespace WordModelSolver.Test.Structures
{
    public class StructuresTests
    {
       
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void MapValueToEnum_GenderString_ReturnsGender()
        {
            Generics<Weekday, string>.MapValueToEnum("Monday");
        }

    }
}