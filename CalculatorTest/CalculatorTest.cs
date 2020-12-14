using Calculator;
using NUnit.Framework;

namespace CalculatorTest
{
    public class CalculatorTest
    {
        private global::Calculator.Calculator calculator;
        private StringParser stringParser;
        private Stack<Actions> actions;
        private Stack<string> operands;
        
        [SetUp]
        public void Setup()
        {
            calculator = new global::Calculator.Calculator();
            stringParser = new StringParser();
        }

        [Test]
        public void CalculateTest_CorrectInfixNotation_CorrectResult()
        {
            string reverseNotation =
                stringParser.GetReverseNotation("cos(-pi-pi)+sqr5/2-log(2,8)", out actions, out operands);

            double expected = 10.5;

            double actual = calculator.Calculate(actions, operands, reverseNotation);
            
            Assert.AreEqual(expected,actual);
        }
    }
}