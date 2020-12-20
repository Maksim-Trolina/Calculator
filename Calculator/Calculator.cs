using System;

namespace Calculator
{
    public class Calculator
    {
        
        public double Calculate(Stack<Actions> actions, Stack<string> operands,string reverseNotation)
        {
            Stack<double> numbers = GetConvertStack(operands);
            Stack<double> result = new Stack<double>();

            for (int i = 0; i < reverseNotation.Length; i++)
            {
                string word = "";
                while (i<reverseNotation.Length && reverseNotation[i]!=' ')
                {
                    word += reverseNotation[i++];
                }

                if (!actions.IsEmpty() && word == actions.Top().Name)
                {
                    ProcessOperation(actions.Top(), result);
                    actions.Pop();
                }
                else
                {
                    result.Push(numbers.Top());
                    numbers.Pop();
                }
            }

            return result.Top();
        }
        

        private Stack<double> GetConvertStack(Stack<string> operands)
        {
            Stack<double> result = new Stack<double>();

            while (!operands.IsEmpty())
            {
                if (!(operands.Top()=="pi" || operands.Top()=="e"))
                {
                    result.Push(double.Parse(operands.Top()));
                }
                else
                {
                    if (operands.Top() == "pi")
                    {
                        result.Push(Math.PI);
                    }
                    else
                    {
                        result.Push(Math.E);
                    }
                }
                operands.Pop();
            }
            
            return result.ReverseStack();
        }

        private void ProcessOperation(Actions action, Stack<double> results)
        {
            switch (action.Name)
            {
                case "+":
                    double num1 = results.Top();
                    results.Pop();
                    double num2 = results.Top();
                    results.Pop();
                    results.Push(num2+num1);
                    return;
                case "/":
                    num1 = results.Top();
                    results.Pop();
                    if (num1 == 0)
                    {
                        throw new Exception("Ошибка деления на нуль");
                    }
                    num2 = results.Top();
                    results.Pop();
                    results.Push(num2/num1);
                    return;
                case "*":
                    num1 = results.Top();
                    results.Pop();
                    num2 = results.Top();
                    results.Pop();
                    results.Push(num2*num1);
                    return;
                case "-" when action.Priority == 1:
                    num1 = results.Top();
                    results.Pop();
                    num2 = results.Top();
                    results.Pop();
                    results.Push(num2-num1);
                    return;
                case "-" when action.Priority == 4:
                    num1 = results.Top();
                    results.Pop();
                    results.Push(-num1);
                    return;
                case "^":
                    num1 = results.Top();
                    results.Pop();
                    num2 = results.Top();
                    results.Pop();
                    results.Push(Math.Pow(num2,num1));
                    return;
                case "cos":
                    num1 = results.Top();
                    results.Pop();
                    results.Push(Math.Cos(num1));
                    return;
                case "sin":
                    num1 = results.Top();
                    results.Pop();
                    results.Push(Math.Sin(num1));
                    return;
                case "tg":
                    num1 = results.Top();
                    if (num1%Math.PI == Math.PI/2)
                    {
                        throw new Exception("Не существующий тангенс");
                    }
                    results.Pop();
                    results.Push(Math.Tan(num1));
                    return;
                case "ctg":
                    num1 = results.Top();
                    if (num1%Math.PI == 0)
                    {
                        throw new Exception("Не существующий катангенс");
                    }
                    results.Pop();
                    results.Push(1/Math.Tan(num1));
                    return;
                case "ln":
                    num1 = results.Top();
                    if (num1 <= 0)
                    {
                        throw new Exception("Не существующий логарифм");
                    }
                    results.Pop();
                    results.Push(Math.Log(num1,Math.E));
                    return;
                case "log":
                    num1 = results.Top();
                    results.Pop();
                    num2 = results.Top();
                    if (num2 <= 0 || num1<=0 || num2==1)
                    {
                        throw new Exception("Не существующий логарифм");
                    }
                    results.Pop();
                    results.Push(Math.Log(num1,num2));
                    return;
                case "sqrt":
                    num1 = results.Top();
                    if (num1 < 0)
                    {
                        throw new Exception("Число под корнем меньше нуля");
                    }
                    results.Pop();
                    results.Push(Math.Sqrt(num1));
                    return;
                case "sqr":
                    num1 = results.Top();
                    results.Pop();
                    results.Push(Math.Pow(num1,2));
                    return;
            }
        }
    }
}