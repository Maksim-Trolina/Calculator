using System;

namespace Calculator
{
    public class Stack<T>
    {
        private T value;

        private Stack<T> prev;

        private Stack<T> tail;
        
        public Stack()
        {
            tail = null;
        }

        public void Push(T newValue)
        {
            if (IsEmpty())
            {
                tail = new Stack<T> { prev = null, value = newValue };
            }
            else
            {
                tail = new Stack<T> { prev = tail, value = newValue };
            }
        }

        public void Pop()
        {
            if (!IsEmpty())
            {
                tail = tail.prev;
            }
            else
            {
                throw new InvalidOperationException("Attempting to remove an item from an empty stack");
            }
        }

        public bool IsEmpty()
        {
            return tail == null;
        }

        public T Top()
        {
            if (!IsEmpty())
            {
                return tail.value;
            }
            
            throw new InvalidOperationException("Attempting to get an item from an empty stack");
        }
        
        public Stack<T> ReverseStack()
        {
            Stack<T> reverseStack = new Stack<T>();
            while (!IsEmpty())
            {
                reverseStack.Push(Top());
                Pop();
            }

            return reverseStack;
        }
    }
}