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
            tail = tail.prev;
        }

        public bool IsEmpty()
        {
            return tail == null;
        }

        public T Top()
        {
            return tail.value;
        }
    }
}