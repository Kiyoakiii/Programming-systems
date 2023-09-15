using RaGlib;
using System;


namespace OperationsLib
{
    public static class Operations
    {
        public delegate bool SymbolChecker(string symb);
        public delegate (bool, string) Operation(string s);

        public static Operation CheckPatternOperation(SymbolChecker checker)
        {
            return (string s) =>
            {
                string symbol = (s.Length > 0) ? s.Substring(0, 1) : "";

                if (checker(symbol))
                    return (true, s.Substring(1));

                return (false, s);
            };
        }

        public static Operation RepeatOperation(Operation operation, int a = -1, int b = -1)
        {
            if (a == -1 && b == -1)
            {
                a = 1;
                b = 1;
            }

            return (string s) =>
            {
                int steps = 0;
                string chain = s;

                while (steps < b || b == -1)
                {
                    var (result, outChain) = operation(chain);
                    if (!result) break;

                    chain = outChain;
                    steps++;
                }

                if (steps < a) return (false, s);
                return (true, chain);
            };
        }

        public static (bool, string) OperationA(string s, int a, int b)
        {
            return RepeatOperation(
                CheckPatternOperation((string s) => (s.Length > 0) && !Char.IsLetter(s[0]) && !Char.IsDigit(s[0]) && (s[0] != '_')),
                a, b
            )(s);
        }

        public static (bool, string) OperationB(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s == "p")), a, b)(s);
        }

        public static (bool, string) OperationC(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s == "o")), a, b)(s);
        }

        public static (bool, string) OperationD(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s == "#") || (s == "-")), a, b)(s);
        }

        public static (bool, string) OperationE(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s.Length > 0) && Char.IsSeparator(s[0])), a, b)(s);
        }

        public static (bool, string) OperationF(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s.Length > 0) && Char.IsDigit(s[0])), a, b)(s);
        }

        public static (bool, string) OperationG(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s.Length > 0) && (Char.IsSeparator(s[0]) || (s[0] == '-'))), a, b)(s);
        }

        public static (bool, string) OperationH(string s, int a, int b)
        {
            return RepeatOperation(CheckPatternOperation((string s) => (s.Length > 0) && Char.IsDigit(s[0])), a, b)(s);
        }
    }
}
