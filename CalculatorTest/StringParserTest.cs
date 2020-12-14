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
            Stack<Actions> actions = new Stack<Actions>();
            Stack<string> operands = new Stack<string>();

            string actual = stringParser.GetReverseNotation(infixNotation,out actions,out operands);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithFuncions_ReverseNotation()
        {
            string expected = "90 45 - cos 3 ln * 2 4 log 3 ^ 2 / + ";
            string infixNotation = "((cos(90-45)*ln3) + log(2 4)^3/2)";
            Stack<Actions> actions = new Stack<Actions>();
            Stack<string> operands = new Stack<string>();

            string actual = stringParser.GetReverseNotation(infixNotation,out actions,out operands);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithConstants_ReverseNotation()
        {
            string expected = "pi 45 - cos 3 sqrt * 56 tg 2 4 log e ^ 2 / / + ";
            string infixNotation = "(cos(pi-45)*sqrt3) + tg(56)/log(2 4)^e/2";
            Stack<Actions> actions = new Stack<Actions>();
            Stack<string> operands = new Stack<string>();

            string actual = stringParser.GetReverseNotation(infixNotation,out actions,out operands);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithUnaryMinus_ReverseNotation()
        {
            string expected = "pi - 45 - cos 3 sqr * 56 ctg 2 4 log - e ^ 2 / / + ";
            string infixNotation = "(cos(-pi-45)*sqr3)+ ctg(56)/-log(2 4)^e/2";
            Stack<Actions> actions = new Stack<Actions>();
            Stack<string> operands = new Stack<string>();

            string actual = stringParser.GetReverseNotation(infixNotation,out actions,out operands);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithDoubleNumber_ReverseNotation()
        {
            string expected = "pi - 4.56 - cos 3 sqrt * 56.4 ln 2.0 4 log - e ^ 2.1 - / / + ";
            string infixNotation = "(cos(-pi-4.56)*sqrt3)+ ln(56.4)/-log(2.0,4)^e/-2.1";
            Stack<Actions> actions = new Stack<Actions>();
            Stack<string> operands = new Stack<string>();

            string actual = stringParser.GetReverseNotation(infixNotation,out actions,out operands);

            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void GetReverseNotationTest_CorrectInfixNotationWithDoubleNumber_ActionsAndOperandsStacks()
        {
            string infixNotation = "(cos(-pi-4.56)*sqrt3)+ ln(56.4)/-log(2.0,4)^e/-2.1";
            Stack<Actions> actions = new Stack<Actions>();
            Stack<string> operands = new Stack<string>();
            Stack<Actions> expectedActions = new Stack<Actions>();
            expectedActions.Push(new Actions("+",false,1));
            expectedActions.Push(new Actions("/",false,2));
            expectedActions.Push(new Actions("/",false,2));
            expectedActions.Push(new Actions("-",false,4));
            expectedActions.Push(new Actions("^",false,3));
            expectedActions.Push(new Actions("-",false,4));
            expectedActions.Push(new Actions("log",true,-1));
            expectedActions.Push(new Actions("ln",true,-1));
            expectedActions.Push(new Actions("*",false,2));
            expectedActions.Push(new Actions("sqrt",true,-1));
            expectedActions.Push(new Actions("cos",true,-1));
            expectedActions.Push(new Actions("-",false,1));
            expectedActions.Push(new Actions("-",false,4));
            Stack<string> expectedOperands = new Stack<string>();
            expectedOperands.Push("2.1");
            expectedOperands.Push("e");
            expectedOperands.Push("4");
            expectedOperands.Push("2.0");
            expectedOperands.Push("56.4");
            expectedOperands.Push("3");
            expectedOperands.Push("4.56");
            expectedOperands.Push("pi");

            string line = stringParser.GetReverseNotation(infixNotation,out actions,out operands);

            Assert.IsTrue(AreEqualStackActions(actions,expectedActions) &
                          AreEqualStackString(operands,expectedOperands));
        }

        private bool AreEqualStackString(Stack<string> stack1, Stack<string> stack2)
        {
            while (!stack1.IsEmpty() && !stack2.IsEmpty() && stack1.Top().CompareTo(stack2.Top()) == 0)
            {
                stack1.Pop();
                stack2.Pop();
            }

            if (!stack1.IsEmpty() || !stack2.IsEmpty())
            {
                return false;
            }

            return true;
        }

        private bool AreEqualStackActions(Stack<Actions> stack1, Stack<Actions> stack2)
        {
            while (!stack1.IsEmpty() && !stack2.IsEmpty() && stack1.Top().Name.CompareTo(stack2.Top().Name) == 0)
            {
                stack1.Pop();
                stack2.Pop();
            }

            if (!stack1.IsEmpty() || !stack2.IsEmpty())
            {
                return false;
            }

            return true;
        }
        
    }
}