namespace Calculator
{
    public struct Actions
    {
        public string Name;

        public bool IsFunction;

        public int Priority;

        public Actions(string name, bool isFunction, int priority)
        {
            Name = name;

            IsFunction = isFunction;

            Priority = priority;
        }
    }
}