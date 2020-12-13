using System;
using NUnit.Framework;
using Calculator;

namespace CalculatorTest
{
    public class StackTest
    {
        [Test]
        public void PushTopPopTest_PutRemoveAndGetTwoElements_ReturnTwoAndFive()
        {
            Stack<int> stack = new Stack<int>();
            
            stack.Push(2);
            stack.Push(5);

            bool firstCondition = stack.Top() == 5;
            stack.Pop();
            bool secondCondition = stack.Top() == 2;
            
            Assert.IsTrue(firstCondition & secondCondition);
        }

        [Test]
        public void PopTest_RemoveElementEmptyStack_InvalidOperationException()
        {
            Stack<int> stack = new Stack<int>();

            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }
        
        [Test]
        public void TopTest_GetElementEmptyStack_InvalidOperationException()
        {
            Stack<int> stack = new Stack<int>();

            Assert.Throws<InvalidOperationException>(() => stack.Top());
        }
    }
}