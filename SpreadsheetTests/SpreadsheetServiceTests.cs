using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet;

namespace SpreadsheetTests
{
    [TestClass]
    public class SreadsheetServiceTests
    {
        [TestMethod]
        public void Evaluate_DoubleValue_SameValue()
        {
            //Arrange
            var spreadsheetService = new SpreadsheetService();

            //Act
            var result = spreadsheetService.Evaluate("2.5", null);

            //Assert
            Assert.AreEqual(2.5, result, double.Epsilon);
        }

        [TestMethod]
        public void Evaluate_Address_ValidResult()
        {
            //Arrange
            var spreadsheet = new Dictionary<string, string> { { "A1", "5" } };
            var spreadsheetService = new SpreadsheetService();

            //Act
            var result = spreadsheetService.Evaluate("A1", spreadsheet);

            //Assert
            Assert.AreEqual(5, result, double.Epsilon);
        }

        [TestMethod]
        public void Evaluate_Operation_ValidResult()
        {
            //Arrange
            var spreadsheetService = new SpreadsheetService();
            //Act
            var result = spreadsheetService.Evaluate("1+9", null);
            //Assert
            Assert.AreEqual(10, result, double.Epsilon);

        }
    }
}
