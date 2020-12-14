using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string infixNotation = Console.ReadLine();
            
            Stack<Actions> actions = new Stack<Actions>();
            
            Stack<string> operands = new Stack<string>();

            string reverseNotation = new StringParser().GetReverseNotation(infixNotation, out actions, out operands);
            
            Console.WriteLine($"Обратная нотация: {reverseNotation}");

            double result = new Calculator().Calculate(actions, operands, reverseNotation);
            
            Console.WriteLine($"Результат вычислений: {result}");
        }
    }
}