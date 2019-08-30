using System;
using System.Collections.Generic;


namespace Program
{
    class Program
    {
        public static byte GetPriority(char c)
        {
            switch (c)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': 
                case '/': 
                    return 4;  
                case '^': return 5;              
                default: return 6;
            }
        }
        public static bool IsOp(char c)
        {
            if (("+-/*^()".IndexOf(c) != -1))
            {
                return true;
            } else 
            {
                return false;
            }                
    
        }
        public static string Rpn (string str)

        {
            string result = string.Empty;
            Stack<char> opStack = new Stack<char>();
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i])) 
                {
                    while (!IsOp(str[i]) && str[i] != ' ')
                    {
                        result += str[i];
                        i++;
                        if (i == str.Length)
                        {
                            break;
                        }
                    }
                    result += " ";
                    i--;
                }
                if (IsOp(str[i]))
                {
                    if (str[i] == '(')
                    {
                        opStack.Push(str[i]);
                    } else if (str[i] == ')')
                    {
                        char s = opStack.Pop();
                        while (s != '(')
                        {
                            result += s.ToString() + " ";
                            s = opStack.Pop();
                        }
                    } else
                    {
                        if (opStack.Count > 0) {
                            if (GetPriority(str[i]) <= GetPriority(opStack.Peek()))   
                            {
                                result += opStack.Pop() + " ";
                            }                          
                        }                                         
                        opStack.Push(char.Parse(str[i].ToString()));           
                    }
                }
            }
            while (opStack.Count > 0)  {
                result += opStack.Pop() + " "; 
            }                                
            return result;
        }
        public static double Calculate (string str)
        {
            double result = 0;
            Stack<double> decision = new Stack <double>();
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    string a = string.Empty;
                    while (str[i] != ' '&& !IsOp(str[i])) 
                    {
                        a += str[i];
                        i++;
                        if (i == str.Length)
                        {
                            break;
                        }
                    }
                    decision.Push(Double.Parse(a));
                    i--;
                } else if (IsOp(str[i]))
                {
                    double x = decision.Pop();
                    double y = decision.Pop();
                    switch (str[i])
                    {
                        case '+': result = y + x; break;
                        case '-': result = y - x; break;
                        case '/': result = x != 0 ? y / x  : throw new DivideByZeroException(); break;
                        case '*': result = y * x; break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(y.ToString()), double.Parse(x.ToString())).ToString()); break;
                    }
                    decision.Push(result);
                }
            }
            return decision.Pop();
        }
        static void Main(string[] args)
        {
            string str = "((2+25+1)-3)*(1+2)/ 2";
            //string str = "((2+25+1)-3)*(1+2)/0";
            Console.Clear();
            //Console.Write("Введите выражение: ");
            //string str = Console.ReadLine().Replace(" ", String.Empty);
            Console.WriteLine("Исходное выражение: {0}", str);
            Console.WriteLine("Выражение в обратной польской записи: {0}", Rpn(str));
            Console.WriteLine("Ответ: {0}", Calculate(Rpn(str)));
            
        }        
    }
}