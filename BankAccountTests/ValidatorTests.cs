using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        [DataRow(0, 0, 10)]
        [DataRow(5, 0, 10)]
        [DataRow(10, 0, 10)]
        [DataRow(10, 10, 10)]
        [DataRow(10.0000001, 0, 10.000001)]
        public void IsWithinRange_ValueWithinRangeInclusive_ReturnsTrue(double value, double min, double max)
        {
            // Arrange
            Validator validator = new Validator();

            // Act
            bool result = validator.IsWithinRange(value, min, max);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsWithinRange_ValueLessThanMinBoundary_ReturnsFalse()
        {
            // Arrange
            Validator validator = new Validator();

            double min = 10;
            double max = 100;
            double valueToCheck = 5;

            // Act
            bool result = validator.IsWithinRange(valueToCheck, min, max);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsWithinRange_ValueGreaterThanMaxBoundary_ReturnsFalse()
        {
            // Arrange
            Validator validator = new Validator();

            double min = 10;
            double max = 100;
            double valueToCheck = 101;

            // Act
            bool result = validator.IsWithinRange(valueToCheck, min, max);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsWithinRange_MinGreaterThanMax_ThrowsArgumentException()
        {
            // Arrange
            Validator validator = new Validator();

            double min = 100;
            double max = 10;
            double valueToCheck = 10;

            // Assert => Act
            Assert.ThrowsException<ArgumentException>(() => validator.IsWithinRange(valueToCheck, min, max));
        }
    }
}