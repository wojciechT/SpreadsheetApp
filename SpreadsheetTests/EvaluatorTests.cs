using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet;

namespace SpreadsheetTests
{
    [TestClass]
    public class EvaluatorTests
    {
        [TestMethod]
        public void Parse_OneOperationInfixString_RpnString()
        {
            //Arrange
            var evaluator = new Evaluator();
            var expression = "2+2";

            //Act
            var result = evaluator.Parse(expression);

            //Assert
            Assert.AreEqual("2 2 +", result);
        }

        [TestMethod]
        public void Parse_3OperationsInfixString_RpnString()
        {
            //Arrange
            var evaluator = new Evaluator();
            var expression = "1+2*3-4";

            //Act
            var result = evaluator.Parse(expression);

            //Assert
            Assert.AreEqual("1 2 3 * + 4 -", result);
        }

        [TestMethod]
        public void Evaluate_3OperationsRpnString_ValidResult()
        {
            //Arrange
            var evaluator = new Evaluator();
            var expression = "1 2 3 * + 4 -";

            //Act
            var result = evaluator.Evaluate(expression);

            //Assert
            Assert.AreEqual(3, result, double.Epsilon);
        }
    }
}
 