using Microsoft.VisualBasic;

namespace Calculator
{
    public class StringParser
    {
        private string[] functionsName;
        private string[] constantsName;

        public StringParser()
        {
            functionsName = new[] {"cos", "sin", "tg", "ctg", "ln", "log", "sqrt", "sqr"};
            constantsName = new[] {"pi", "e"};
        }
        public string GetReverseNotation(string line)
        {
            string result = "";
            
            Stack<Actions> functions = new Stack<Actions>();

            for (int i = 0; i < line.Length; i++)
            {
                if (IsSplite(line[i]))
                {
                    continue;
                }
                if (IsDigit(line[i]) && (i==0 || line[i-1]!='.'))
                {
                    AddDigits(ref result,line,ref i);
                    continue;
                }

                if (line[i] == '(')
                {
                    functions.Push(new Actions("(",false,-1));
                    continue;
                }

                if (line[i] == ')')
                {
                    AddFunctionsAndOperations(functions,ref result);
                    continue;
                }

                if (IsBinaryOperation(line,i))
                {
                    AddOperationToStack(functions,line,i,ref result);
                    continue;
                }

                if (IsConstant(line, ref i,ref result))
                {
                    continue;
                }
                
                AddFunctionToStack(functions,line,ref i);
            }

            while (!functions.IsEmpty())
            {
                result += functions.Top().Name;
                result += ' ';
                functions.Pop();
            }
            
            return result;
        }

        private bool IsConstant(string line, ref int curIndex,ref string result)
        {
            string constant = "";

            int index = curIndex;
            
            while (index<line.Length && !IsDigit(line[index]) && !IsSplite(line[index])
            && !IsBinaryOperation(line,index) && !IsFunction(constant))
            {
                constant += line[index++];
                for (int i = 0; i < constantsName.Length; i++)
                {
                    if (constant == constantsName[i])
                    {
                        result += constant;
                        result += ' ';
                        curIndex = index-1;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsSplite(char c)
        {
            return c == ' ' || c == ',';
        }

        private bool IsDigit(char c)
        {
            return c == '0' || c == '1' || c == '2'
                   || c == '3' || c == '4' || c == '5'
                   || c == '6' || c == '7' || c == '8'
                   || c == '9';
        }

        private bool IsBinaryOperation(string line,int curIndex)
        {
            return line[curIndex] == '+'
                   || line[curIndex] == '-'
                   || line[curIndex] == '*' 
                   || line[curIndex] == '/' 
                   || line[curIndex] == '^';
        }
        
        private bool IsFunction(string function)
        {
            for (int i = 0; i < functionsName.Length; i++)
            {
                if (function == functionsName[i])
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsUnaryOperation(string line,int index,Stack<Actions> functions)
        {
            return index == 0 || IsBinaryOperation(line,index-1)
                              || line[index-1] == '('
                              || !functions.IsEmpty() && functions.Top().IsFunction;
        }
        
        private int GetPriorityOperation(string op)
        {
            if (op == "+" || op == "-")
            {
                return 1;
            }

            if (op == "*" || op == "/")
            {
                return 2;
            }

            if (op == "^")
            {
                return 3;
            }

            return -1;
        }

        private void AddDigits(ref string result,string line,ref int curIndex)
        {
            AddNum(ref result,line,ref curIndex);

            if (curIndex < line.Length && line[curIndex] == '.')
            {
                result += line[curIndex++];

                AddNum(ref result,line,ref curIndex);
            }

            result += ' ';
            curIndex--;
        }

        private void AddNum(ref string result, string line, ref int curIndex)
        {
            while (curIndex<line.Length && IsDigit(line[curIndex]))
            {
                result += line[curIndex++];
            }
        }
        

        private void AddFunctionToStack(Stack<Actions> functions, string line, ref int curIndex)
        {
            string fun = "";

            while (curIndex<line.Length && !IsFunction(fun))
            {
                fun += line[curIndex++];
            }

            if (curIndex < line.Length && fun == "sqr" && line[curIndex]=='t')
            {
                fun += 't';
                curIndex++;
            }

            curIndex--;
            
            
            functions.Push(new Actions(fun,true,-1));
        }

        private void AddFunctionsAndOperations(Stack<Actions> functions, ref string result)
        {
            while (!functions.IsEmpty() && functions.Top().Name!="(")
            {
                result += functions.Top().Name;
                result += ' ';
                functions.Pop();
            }

            if (!functions.IsEmpty() && functions.Top().Name == "(")
            {
                functions.Pop();

                if (!functions.IsEmpty() && functions.Top().IsFunction)
                {
                    result += functions.Top().Name;
                    result += ' ';
                    functions.Pop();
                }
            }
            /*else
            {
                //выдать ошибку,тк строка не корректно задана
            }*/
        }

        private void AddOperationToStack(Stack<Actions> functions,string line,int curIndex,ref string result)
        {
            string op = "";
            op += line[curIndex];
            int priopity;
            if (IsUnaryOperation(line, curIndex,functions))
            {
                priopity = 4;
            }
            else
            {
                priopity = GetPriorityOperation(op);
            }

            while (!functions.IsEmpty() && (functions.Top().IsFunction
                                        || priopity<functions.Top().Priority))
            {
                result += functions.Top().Name;
                result += ' ';
                functions.Pop();
            }
            
            functions.Push(new Actions(op,false,priopity));
        }
    }
}