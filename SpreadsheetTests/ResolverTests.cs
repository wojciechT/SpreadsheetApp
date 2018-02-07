using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet;

namespace SpreadsheetTests
{
    [TestClass]
    public class ResolverTests
    {
        [TestMethod]
        public void ResolveInExpression_OneAddress_Value()
        {
            //Arrange
            var resolver = new Resolver();
            var spreadsheet = new Dictionary<string, string>
            {
                {"A1", "5"}
            };
            var expression = "A1";

            //Act
            var result = resolver.ResolveInExpression(expression, spreadsheet);

            //Assert
            Assert.AreEqual("5", result);
        }

        [TestMethod]
        public void ResolveInExpression_3Addresses_3Values()
        {
            //Arrange
            var resolver = new Resolver();
            var spreadsheet = new Dictionary<string, string>
            {
                {"A1","1"},
                {"A2","2"},
                {"A3","3"}
            };
            var expression = "A1+A2+A3";

            //Act
            var result = resolver.ResolveInExpression(expression, spreadsheet);

            //Assert
            Assert.AreEqual("1+2+3", result);
        }

        [TestMethod]
        public void ResolveInSpreadsheet_Address_Value()
        {
            //Arrange
            var resolver = new Resolver();
            var spreadsheet = new Dictionary<string, string>
            {
                {"A1", "1"},
                {"A2", "A1"}
            };

            //Act
            var result = resolver.ResolveInSpreadsheet(spreadsheet["A2"], "A2", new List<string>(), spreadsheet);

            //Assert
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void ResolveInSpreadsheet_AddressToAddress_Value()
        {
            //Arrange
            var resolver = new Resolver();
            var spreadsheet = new Dictionary<string, string>
            {
                {"A1", "1"},
                {"A2", "A1"},
                {"A3", "A2"}
            };

            //Act
            var result = resolver.ResolveInSpreadsheet(spreadsheet["A3"], "A3", new List<string>(), spreadsheet);

            //Assert
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void ResolveInSpreadsheet_OperationOnAddresses_OperationOnValues()
        {
            //Arrange
            var resolver = new Resolver();
            var spreadsheet = new Dictionary<string, string>
            {
                {"A1", "1"},
                {"A2", "A1"},
                {"A3", "A2"},
                {"A4", "A1+A2+A3" }
            };

            //Act
            var result = resolver.ResolveInSpreadsheet(spreadsheet["A4"], "A4", new List<string>(), spreadsheet);

            //Assert
            Assert.AreEqual("1+1+1", result);
        }

        [TestMethod]
        public void ResolveInSpreadsheet_NestedCircularReference_Exception()
        {
            //Arrange
            var resolver = new Resolver();
            var spreadsheet = new Dictionary<string, string>
            {
                {"A1", "A2"},
                {"A2", "A1"},
                {"A3", "A1+A2"}
            };

            //Assert
            Assert.ThrowsException<CircularReferenceException>(() =>
                resolver.ResolveInSpreadsheet(spreadsheet["A3"], "A3", new List<string>(), spreadsheet));
        }
    } 
    
}
