using Calculator;
using NUnit.Framework;

namespace CalculatorTest
{
    public class StringParserTest
    {
        private StringParser stringParser;
        [SetUp]
        public void Set()
        {
            stringParser = new StringParser();
        }
        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithBinOperations_ReverseNotation()
        {
            string expected = "3 4 + 5 * 6 4 2 / - 3 ^ - ";
            string infixNotation = "(3+4)*5-(6-4/2)^3";

            string actual = stringParser.GetReverseNotation(infixNotation);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithFuncions_ReverseNotation()
        {
            string expected = "90 45 - cos 3 ln * 2 4 log 3 ^ 2 / + ";
            string infixNotation = "((cos(90-45)*ln3) + log(2 4)^3/2)";

            string actual = stringParser.GetReverseNotation(infixNotation);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithConstants_ReverseNotation()
        {
            string expected = "pi 45 - cos 3 sqrt * 56 tg 2 4 log e ^ 2 / / + ";
            string infixNotation = "(cos(pi-45)*sqrt3) + tg(56)/log(2 4)^e/2";

            string actual = stringParser.GetReverseNotation(infixNotation);

            Assert.AreEqual(expected,actual);
        }

        /*[Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithUnaryMinus_ReverseNotation()
        {
            string expected = "pi - 45 - cos 3 sqr * 56 ctg 2 4 log - e ^ 2 / / + ";
            string infixNotation = "(cos(-pi-45)*sqr3) + ctg(56)/-log(2 4)^e/2";

            string actual = stringParser.GetReverseNotation(infixNotation);

            Assert.AreEqual(expected,actual);
        }*/

        [Test]
        public void My()
        {
            string expected = "90 cos - ";
            string infixNotation = "-cos90";

            string actual = stringParser.GetReverseNotation(infixNotation);

            Assert.AreEqual(expected,actual);
        }
    }
}