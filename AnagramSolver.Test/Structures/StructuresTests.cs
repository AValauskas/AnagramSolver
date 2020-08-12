
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Structures;
using System;

namespace WordModelSolver.Test.Structures
{
    public class StructuresTests
    {

        [SetUp]
        public void Setup()
        {

        }
       
        [Test]
        [TestCase("Monday")]
        public void MapValueToEnum_MondayInWeekday_returnsModayEnum(string day)
        {
            var result = Generics.MapValueToEnum<Weekday, string>(day);

            Assert.AreEqual(result, Weekday.Monday);
        }

        [Test]
        [TestCase("day")]
        public void MapValueToEnum_wrongweekdayenum_returnsexception(string day)
        {      
            var exception = Assert.Throws<Exception>(()=>Generics.MapValueToEnum<Weekday, string>(day));
            Assert.AreEqual($"Value '{day}' is not part of Weekday enum", exception.Message);
        }

        [Test]
        [TestCase(8)]
        public void MapValueToEnum_wrongweekdayenumInt_returnsexception(int day)
        {
            var exception = Assert.Throws<Exception>(() => Generics.MapValueToEnum<Weekday, int>(day));
            Assert.AreEqual($"Value '{day}' is not part of Weekday enum", exception.Message);
        }

        [Test]
        [TestCase("Female")]
        public void MapValueToEnum_FemaleString_returnsFemale(string sex)
        {
            var result = Generics.MapValueToEnum<Gender, string>(sex);

            Assert.AreEqual(result, Gender.Female);
        }
        [Test]
        [TestCase(2)]
        public void MapValueToEnum_FemaleInt_ReturnsFemale(int sex)
        {
            var result = Generics.MapValueToEnum<Gender, int>(sex);

            Assert.AreEqual(result, Gender.Female);
        }
        [Test]
        [TestCase("dog")]
        public void MapValueToEnum_WrongFemaleString_ReturnsException(string sex)
        {
            var exception = Assert.Throws<Exception>(() => Generics.MapValueToEnum<Gender, string>(sex));
            Assert.AreEqual($"Value '{sex}' is not part of Gender enum", exception.Message);
        }
        [Test]
        [TestCase(5)]
        public void MapValueToEnum_WrongFemaleInt_ReturnsException(int sex)
        {
            var exception = Assert.Throws<Exception>(() => Generics.MapValueToEnum<Gender, int>(sex));
            Assert.AreEqual($"Value '{sex}' is not part of Gender enum", exception.Message);
        }


    }
}