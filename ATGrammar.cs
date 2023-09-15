using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using RaGlib.Core;
using RaGlib;
using RaGlib.Grammars;

namespace RaGlib.Grammars {
    public class ATGrammar : Grammar
    {
        public List<Symbol_Operation> OP { set; get; } = null;

        public AttrType Type = new AttrType(); // Типы атрибутов (см класс AttrType)

        private Grammar G;
        private Stack<Tuple<int, int>> Stack;
        public string OutputConfigure = "";

        Dictionary<string, string> NumbersOP = new Dictionary<string, string>();
        Dictionary<string, string> NumbersT = new Dictionary<string, string>();
        Dictionary<string, string> Numbers = new Dictionary<string, string>();
        Dictionary<string, Dictionary<Symbol, string>> H = new Dictionary<string, Dictionary<Symbol, string>>();
        Dictionary<int, int> SizeRule = new Dictionary<int, int>();

        public List<AttrProduction> Rules { set; get; } = new List<AttrProduction>();
        public ATGrammar() { }

        public ATGrammar(List<Symbol> V, List<Symbol> T, List<Symbol_Operation> OP, Symbol S0)
        {
            this.OP = OP;
            this.V = V;
            this.T = T;
            this.S0 = S0;
        }
        public ATGrammar(List<Symbol> V, List<Symbol> T, List<Symbol_Operation> OP, Symbol S0, AttrType Type)
        {
            this.OP = OP;
            this.V = V;
            this.T = T;
            this.S0 = S0;
            this.Type = Type;
        }

        public void LLParser(Grammar grammar)
        {
            this.G = grammar;
            Stack = new Stack<Tuple<int, int>>();

            Console.Write("\n");
            Console.Write("\n");

            Console.WriteLine("Управляющая таблица: ");
            Console.Write("{0, 15}", "");
            foreach (var termSymbol in G.T)
            {
                Console.Write("{0, 15}", termSymbol.symbol);
            }
            Console.Write("\n");

            int count = 0;

            string numV = "";

            for (int i = 0; i < grammar.V.Count; ++i)
            {
                H.Add((string)grammar.V[i].symbol, new Dictionary<Symbol, string>());

                var rules = getRules(grammar.V[i]);

                foreach (var rule in rules)
                {
                    count++;
                    var firstSymbol = rule.RHS[0];

                    if (firstSymbol != "")
                    {
                        string s = count.ToString();
                        s += ".1";

                        for (int j = 0; j < rule.RHS.Count; ++j)
                        {
                            if (rule.RHS[j].symbol == grammar.V[i].symbol)
                            {
                                numV += count.ToString() + ".";
                                numV += (j + 1).ToString() + "|";
                            }
                        }

                        H[rule.LHS.symbol].Add(firstSymbol, s);
                    }
                    else
                    {
                        HashSet<Symbol> currFollowSet = grammar.Follow(rule.LHS);
                        foreach (var currFollowSymb in currFollowSet)
                        {
                            string currFollowSymbFix = (currFollowSymb == "") ? "eps" : currFollowSymb.symbol;
                        }
                    }
                }
            }
            numV = "";

            for (int i = 0; i < grammar.V.Count; ++i)
            {
                count = 0;
                for (int j = 0; j < Rules.Count; ++j)
                {
                    ++count;
                    Production currRule = (Production)Rules[j];

                    if (i == 0)
                    {
                        int size = currRule.RHS.Count;
                        SizeRule.Add(count, size);
                    }
                    for (int index = 0; index < currRule.RHS.Count; ++index)
                    {
                        if (currRule.RHS[index].symbol == grammar.V[i].symbol)
                        {
                            numV += count.ToString() + ".";
                            numV += (index + 1).ToString() + "|";
                        }
                    }
                }
                int x = numV.Length - 1;
                if (numV.Length != 0)
                {
                    numV = numV.Remove(x);
                }
                else
                {
                    numV = "";
                }

                Numbers.Add(grammar.V[i].symbol, numV);
                numV = "";
            }


            string numT = "";
            for (int i = 0; i < T.Count; ++i)
            {
                count = 0;
                for (int j = 0; j < Rules.Count; ++j)
                {
                    count++;
                    Production currRule = (Production)Rules[j];
                    for (int index = 0; index < currRule.RHS.Count; ++index)
                    {
                        if (currRule.RHS[index].symbol == grammar.T[i].symbol)
                        {
                            numT += count.ToString() + ".";
                            numT += (index + 1).ToString() + "|";
                        }
                    }
                }
                int x = numT.Length - 1;
                if (numT.Length != 0)
                {
                    numT = numT.Remove(x);
                }
                else
                {
                    numT = "";
                }

                NumbersT.Add(T[i].symbol, numT);
                numT = "";
            }

            string numOP = "";
            for (int i = 0; i < OP.Count; ++i)
            {
                count = 0;
                for (int j = 0; j < Rules.Count; ++j)
                {
                    ++count;
                    Production currRule = (Production)Rules[j];
                    for (int index = 0; index < currRule.RHS.Count; ++index)
                    {
                        if (currRule.RHS[index].symbol == OP[i].symbol)
                        {
                            numOP += count.ToString() + ".";
                            numOP += (index + 1).ToString() + "|";
                        }
                    }
                }
                int x = numOP.Length - 1;
                if (numOP.Length != 0)
                {
                    numOP = numOP.Remove(x);
                }
                else
                {
                    numOP = "";
                }

                NumbersOP.Add(OP[i].symbol, numOP);
                numOP = "";
            }
            foreach (var t in T)
            {
                H.Add((string)t.symbol, new Dictionary<Symbol, string>());
                H[t.symbol].Add(t.symbol, "ВЫБРОС");
            }
            H.Add((string)"|", new Dictionary<Symbol, string>());
            H["|"].Add("eps", "ДОПУСК");


            for (int i = 1; i < V.Count; i++)
            {
                Console.Write("{0, 15}", Numbers[V[i].symbol]);

                foreach (var t in grammar.T)
                {
                    Console.Write("{0, 15}", (H[V[i].symbol].ContainsKey(t) ? H[grammar.V[i].symbol][t] : ""));
                }
                Console.Write("\n");
            }
            foreach (var t in T)
            {
                if (t.symbol == "eps")
                {
                    break;
                }
                Console.Write("{0, 15}", NumbersT[t.symbol]);

                foreach (var term in grammar.T)
                {
                    Console.Write("{0, 15}", (H[t.symbol].ContainsKey(term) ? H[t.symbol][term.symbol] : ""));
                }
                Console.Write("\n");
            }

            Console.Write("{0, 15}", "|");
            foreach (var term in grammar.T)
            {
                Console.Write("{0, 15}", (H["|"].ContainsKey(term) ? H["|"][term.symbol] : ""));
            }
            Console.Write("\n");

            var opf = new List<Symbol_Operation>();
            foreach (var op in OP)
            {
                H.Add(op.symbol, new Dictionary<Symbol, string>());
                foreach (var t in grammar.T)
                {
                    H[op.symbol].Add(t.symbol, "ВЫДАЧА");
                }
                Console.Write("{0, 15}", NumbersOP[op.symbol]);
                Console.Write("                              ВЫДАЧА(");
                op.print();
                Console.Write(")");
                Console.Write("\n");
            }
        }

        public bool DMPAutomate(List<Symbol> input)
        {
            var tuple0 = Tuple.Create(1, 1);
            Stack.Push(tuple0);

            int i = 0;
            Symbol currInputSymbol = input[0];
            Tuple<int, int> currStackSymbol;
            do
            {
                Console.Write("      (");
                for (int k = 0; k < Stack.Count; ++k)
                {
                    Tuple<int, int>[] tmp = new Tuple<int, int>[Stack.Count];
                    Stack.CopyTo(tmp, 0);
                    Console.Write(tmp[k].Item1 + "." + tmp[k].Item2);
                    if (k < Stack.Count - 1)
                    {
                        Console.Write(";");
                    }
                }
                Console.Write(", ");

                for (int j = i; j < input.Count(); ++j)
                {
                    if (input[j].attr != null)
                    {
                        Console.Write(input[j].symbol + "." + input[j].attr[0].symbol);
                        j++;
                    }
                    else
                    {
                        Console.Write(input[j].symbol);
                    }
                }
                Console.Write(", ");
                Console.Write(OutputConfigure);
                Console.WriteLine(")\n");

                bool flag = false;
                currStackSymbol = Stack.Pop();
                string symbolStack = currStackSymbol.Item1.ToString() + "." + currStackSymbol.Item2.ToString();

                int first = currStackSymbol.Item1;
                int second = currStackSymbol.Item2;
                string temp = "";

                second++;
                var curtuple = Tuple.Create(first, second);

                if (second > SizeRule[first])
                {
                    if (Rules[first - 1].RHS[second - 2].attr != null)
                    {
                        temp = Rules[first - 1].RHS[second - 2].attr[0].symbol;
                    }
                    flag = true;
                }

                foreach (var t in T)
                {
                    if (NumbersT[t.symbol].IndexOf(symbolStack) >= 0)
                    {
                        if (H[t.symbol][currInputSymbol.symbol] == "ВЫБРОС")
                        {
                            if (i != input.Count() && input[i].attr == null)
                            {
                                if (!flag)
                                    Stack.Push(curtuple);
                                if (i != input.Count())
                                {
                                    ++i;
                                }
                            }
                            else
                            {
                                if (!flag)
                                    Stack.Push(curtuple);
                                if (i == input.Count())
                                {
                                    continue;
                                }
                                string leftAttr = "";
                                int COUNTER = 0;

                                for (int j = 0; j < Rules[first - 1].F.Count; ++j)
                                {
                                    if (Rules[first - 1].F[j].RH[0].symbol == Rules[first - 1].RHS[second - 2].attr[0].symbol)
                                    {
                                        leftAttr = Rules[first - 1].F[j].LH[0].symbol;
                                    }

                                    if (Rules[first - 1].F[j].RH[0].symbol == "COUNTER")
                                    {
                                        string searchAttr = Rules[first - 1].F[j].LH[0].symbol;

                                        for (int index = 0; index < Rules[first - 1].RHS.Count; ++index)
                                        {
                                            if (Rules[first - 1].RHS[index].attr != null)
                                            {
                                                for (int k = 0; k < Rules[first - 1].RHS[index].attr.Count; ++k)
                                                {
                                                    if (Rules[first - 1].RHS[index].attr[k].symbol == searchAttr)
                                                    {
                                                        Rules[first - 1].RHS[index].attr[k].symbol = COUNTER.ToString();
                                                        COUNTER++;
                                                    }
                                                }

                                            }

                                        }

                                    }
                                }
                                for (int j = 0; j < Rules[first - 1].RHS.Count; ++j)
                                {
                                    if (Rules[first - 1].RHS[j].attr != null)
                                    {
                                        for (int k = 0; k < Rules[first - 1].RHS[j].attr.Count; ++k)
                                        {
                                            if (Rules[first - 1].RHS[j].attr[k].symbol == leftAttr)
                                            {
                                                Rules[first - 1].RHS[j].attr[k].symbol = input[i].attr[0].symbol;
                                            }
                                        }

                                    }

                                }

                                if (i <= input.Count() - 2)
                                {
                                    i = i + 2;
                                }
                            }
                            currInputSymbol = (i == input.Count()) ? "eps" : input[i];
                        }
                    }
                }
                foreach (var v in V)
                {
                    if (Numbers[v.symbol].IndexOf(symbolStack) >= 0)
                    {
                        if (H[v.symbol].ContainsKey(currInputSymbol.symbol))
                        {
                            if (!flag)
                                Stack.Push(curtuple);

                            string s = H[v.symbol][currInputSymbol.symbol];
                            int newFirst = s[0] - '0';
                            int newSecond = s[2] - '0';

                            var newTuple = Tuple.Create(newFirst, newSecond);
                            Stack.Push(newTuple);

                            if (Rules[newFirst - 1].LHS.attr != null)
                            {
                                for (int j = 0; j < Rules[newFirst - 1].F.Count; ++j)
                                {
                                    if (Rules[newFirst - 1].F[j].RH[0].symbol == Rules[newFirst - 1].LHS.attr[0].symbol)
                                    {
                                        for (int index = 0; index < Rules[newFirst - 1].RHS.Count; ++index)
                                        {
                                            if (Rules[newFirst - 1].RHS[index].attr != null)
                                            {
                                                for (int k = 0; k < Rules[newFirst - 1].RHS[index].attr.Count; ++k)
                                                {
                                                    if (Rules[newFirst - 1].RHS[index].attr[k].symbol == Rules[newFirst - 1].F[j].LH[0].symbol)
                                                    {
                                                        Rules[newFirst - 1].RHS[index].attr[k].symbol = temp;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                foreach (var op in OP)
                {
                    if (NumbersOP[op.symbol].IndexOf(symbolStack) >= 0)
                    {
                        if (H[op.symbol][currInputSymbol.symbol] == "ВЫДАЧА")
                        {
                            OutputConfigure += op.symbol;
                            for (int j = 0; j < op.attr.Count; ++j)
                            {
                                OutputConfigure += Rules[first - 1].RHS[second - 2].attr[j].symbol;
                            }

                            if (!flag)
                                Stack.Push(curtuple);
                        }
                    }
                }

            } while (Stack.Count() > 0);

            Console.Write("      (");
            for (int k = 0; k < Stack.Count; ++k)
            {
                Tuple<int, int>[] tmp = new Tuple<int, int>[Stack.Count];
                Stack.CopyTo(tmp, 0);
                Console.Write(tmp[k].Item1 + "." + tmp[k].Item2);
                if (k < Stack.Count - 1)
                {
                    Console.Write(";");
                }
            }
            Console.Write(", ");

            for (int j = i; j < input.Count(); ++j)
            {
                Console.Write(input[j]);
            }
            Console.Write(", ");
            Console.Write(OutputConfigure);
            Console.WriteLine(")\n");

            if (i != input.Count())
            {
                return false;
            }

            return true;
        }
        public List<Production> getRules(Symbol noTermSymbol)
        {
            List<Production> result = new List<Production>();
            for (int i = 0; i < Rules.Count; ++i)
            {
                Production currRule = (Production)Rules[i];
                if (currRule.LHS.symbol == noTermSymbol)
                {
                    result.Add(currRule);
                }
            }
            return result;
        }
        public void Addrule(Symbol LeftNoTerm, List<Symbol> Right)
        {
            this.Rules.Add(new AttrProduction(LeftNoTerm, Right));
        }

        /// Добавление правила
        public void Addrule(Symbol LeftNoTerm,
            List<Symbol> Right,
            List<AttrFunction> F)
        {

            foreach (var j in V)
            {
                if ((LeftNoTerm == j) && (LeftNoTerm.attr == null))
                {
                    LeftNoTerm = j;
                }
            }

            for (int i = 0; i < Right.Count; ++i)
            {
                foreach (var j in V)
                {
                    if (Right[i] == j && Right[i].attr == null)
                    {
                        Right[i] = j;
                    }
                }

                foreach (var j in T)
                {
                    if (Right[i] == j && Right[i].attr == null)
                    {
                        Right[i] = j;
                    }
                }

                foreach (var j in OP)
                {
                    if (Right[i] == j && Right[i].attr == null)
                    {
                        Right[i] = j;
                    }
                }

            }

            this.Rules.Add(new AttrProduction(LeftNoTerm, Right, F));
        }



        public Symbol Search(string attr)
        {
            List<string> attrib = new List<string>();
            for (int i = 0; i < V.Count; ++i)
            {
                if (V[i].attr != null)
                {
                    for (int j = 0; j < V[i].attr.Count; ++j)
                    {
                        if (attr[0].ToString() == V[i].attr[j].ToString()[0].ToString())
                        {
                            Symbol symbol = V[i];
                            return symbol;

                        }
                    }
                }
            }

            for (int i = 0; i < T.Count; ++i)
            {
                if (T[i].attr != null)
                {
                    for (int j = 0; j < T[i].attr.Count; ++j)
                    {
                        if (attr[0].ToString() == T[i].attr[j].ToString()[0].ToString())
                        {
                            //Console.Write($"\nFind {T[i]}\n");
                            Symbol symbol = T[i];
                            return symbol;

                        }
                    }
                }
            }

            for (int i = 0; i < OP.Count; ++i)
            {
                if (OP[i].attr != null)
                {
                    for (int j = 0; j < OP[i].attr.Count; ++j)
                    {
                        if (attr[0].ToString() == OP[i].attr[j].ToString()[0].ToString())
                        {
                            //Console.Write($"\nFind {OP[i]}\n");
                            Symbol symbol = OP[i];
                            return symbol;

                        }
                    }
                }
            }

            return null;

        }

        public void PrintWithColot(Symbol attr)
        {
            if (Type.Syn.Contains(attr))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(attr);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(attr);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public bool IsLATGrammar()
        {
            bool condition = true;
            if (Rules.Count != 0)
            {
                Console.Write("\n\n-------------------:\nПроверим L-атрибутивность АТ-грамматики\n");
                for (int j = 0; j < Rules.Count; ++j)
                {
                    string[] charArray = new string[100];
                    List<string> right = new List<string>();
                    List<string> left = new List<string>();
                    int x = 1;

                    Console.Write($"\nдля {j + 1}-го правила вывода\n");

                    for (int k = 0; k < Rules[j].F.Count; ++k)
                    {
                        bool symbolIsRight = false;

                        foreach (var rhs in Rules[j].RHS)
                        {
                            if (rhs.attr != null)
                            {
                                foreach (var attr in rhs.attr)
                                {
                                    if (Rules[j].F[k].LH[0] == attr)
                                    {
                                        symbolIsRight = true;
                                        break;
                                    }
                                    else
                                    {
                                        right.Add(attr.ToString());
                                    }
                                }
                            }

                            if (symbolIsRight == true) break;
                        }

                        foreach (var item in Rules[j].F[k].LH)
                        {
                            PrintWithColot(item);

                            if (item != Rules[j].F[k].LH.Last())
                            {
                                Console.Write(", ");
                            }
                        }

                        Console.Write(" <- ");

                        for (int i = 0; i < Rules[j].F[k].RH.Count; ++i)
                        {
                            if (Rules[j].LHS.attr != null)
                            {
                                foreach (var attr in Rules[j].LHS.attr)
                                {
                                    left.Add(attr.ToString());
                                }
                            }

                            Symbol symbolRight = Rules[j].F[k].RH[i];
                            Symbol symbolLeft = Rules[j].F[k].LH[0];

                            Symbol symbol = Search(Rules[j].F[k].LH[0].ToString()); // поиск символа по атрибуту

                            // Условие 1. Аргументами правила вычисления значения унаследованного атрибута символа
                            // из правой части правила вывода могут быть только унаследованные атрибуты
                            // символа из левой части и произвольные атрибуты символов из правой части,
                            // расположенные левее рассматриваемого символа. 
                            if ((!OP.Contains(symbol) && (Type.Inh.Contains(symbolLeft.ToString())) && symbolIsRight == true))
                            {
                                //Console.WriteLine($"\n KUUUU 1 \n");
                                if (!right.Contains(symbolRight.ToString()) && !left.Contains(symbolRight.ToString()))
                                {
                                    condition = false;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{symbolRight}\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write($"НЕ L-атрибутивна(нарушение 1-го условия), так как в правиле для {x}-го символа присутствует атрибут правее: ");
                                    PrintWithColot(symbolRight);
                                }
                                else if (left.Contains(symbolRight.ToString()) && Type.Syn.Contains(symbolRight.ToString()))
                                {
                                    condition = false;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{symbolRight}\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write($"НЕ L-атрибутивна(нарушение 1-го условия), так как в правиле для {symbolLeft} синтезированного атрибута символа присутствует синтезированный атрибут символа слева: ");
                                    PrintWithColot(symbolRight);
                                }
                                else
                                {
                                    PrintWithColot(symbolRight);
                                }
                            }
                            // Условие 2. Аргументами правила вычисления значения синтезированного атрибута символа
                            // из левой части правила вывода являются унаследованные атрибуты этого символа
                            // или произвольные атрибуты символов из правой части. 
                            else if (!OP.Contains(symbol) && (Type.Syn.Contains(symbolLeft.ToString())) && !symbolIsRight)
                            {
                                //Console.WriteLine($"\n KUUUU 2 \n");
                                if (!right.Contains(symbolRight.ToString()) && !left.Contains(symbolRight.ToString()))
                                {
                                    condition = false;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{symbolRight}\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write($"НЕ L-атрибутивна(нарушение 2-го условия), так как справа нет такого атрибута: ");
                                    PrintWithColot(symbolRight + "\n");
                                }
                                else if (Type.Syn.Contains(symbolRight.ToString()) && left.Contains(symbolRight.ToString()))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{symbolRight}\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write($"НЕ L-атрибутивна(нарушение 2-го условия), так как синтезированный атрибут унаследует синтезированный атрибут этого же символа: ");
                                    PrintWithColot(symbolRight);
                                }
                                else
                                {
                                    PrintWithColot(symbolRight);
                                }
                            }
                            // Условие 3. Аргументами правила вычисления значения синтезированного атрибута
                            // операционного символа могут быть только унаследованные атрибуты этого символа.
                            else if (OP.Contains(symbol) && Type.Syn.Contains(symbolLeft.ToString()))
                            {
                                //Console.WriteLine($"\n KUUUU 3 \n");
                                if (Type.Syn.Contains(symbolRight.ToString()) && !right.Contains(symbolRight.ToString()))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{symbolRight}\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write($"НЕ L-атрибутивна(нарушение 3-го условия), так как синтезированный атрибут унаследует синтезированный атрибут этого же OP символа: ");
                                    PrintWithColot(symbolRight);
                                }
                                else
                                {
                                    PrintWithColot(symbolRight);

                                }
                            }
                            else
                            {
                                PrintWithColot(symbolRight);
                            }

                            right.Clear();

                        }
                        if (k != (Rules[j].F.Count - 1))
                            Console.Write("\n");
                    }
                }
                Console.Write("\n");
            }
            return condition;
        }


public void print()
        {
            Console.Write("\nAT-Grammar G = (V, T, OP, P, S)");
            Console.Write("\nSyn = { "); //терминальные
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var a in Type.Syn)
            {
                if (a != Type.Syn.Last())
                    Console.Write($"{a}, ");
                else
                    Console.Write($"{a}");


            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" },");
            Console.Write("\nInh = { "); //терминальные
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var a in Type.Inh)
            {
                if (a != Type.Inh.Last())
                    Console.Write($"{a}, ");
                else
                    Console.Write($"{a}");


            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" },");
            Console.Write("\nV = { "); //нетерминальные символы
            for (int i = 0; i < V.Count; ++i)
            {
                V[i].printWithColor(Type.Syn, Type.Inh);
                if (i != V.Count - 1)
                    Console.Write(", ");
            }
            Console.Write(" },");
            Console.Write("\nT = { "); //терминальные
            for (int i = 0; i < T.Count; ++i)
            {
                T[i].printWithColor(Type.Syn, Type.Inh);
                if (i != T.Count - 1)
                    Console.Write(", ");
            }
            Console.Write(" },");

            var opf = new List<Symbol_Operation>(); //счётчик операционных символов, у которых есть атрибуты
            Console.Write("\nOP = "); //операционные
            Console.Write("{ ");
            for (int l = 0; l < OP.Count; ++l)
            {
                //Search(OP[l].ToString());
                OP[l].printWithColor(Type.Syn, Type.Inh);
                if (OP[l].function != null)
                {
                    opf.Add(OP[l]);
                    //Console.Write("\n!!!" + OP[l].function);

                }

                if (l != OP.Count - 1)
                    Console.Write(", ");

            }
            Console.Write(" },");

            Console.Write("\nS = ");
            S0.print();

            //печать правил атрибутов операционных символов
            //Console.Write($"\nopf.Count = {opf.Count}\n");
            if (opf.Count != 0)
            {
                Console.Write("\n\nOperation Symbols Rules:\n");
                foreach (var op in opf)
                {
                    op.printFunctionWithColor(Type.Syn, Type.Inh);
                    Console.Write("\n");
                }
            }
            //печать правил грамматики
            if (Rules.Count != 0)
            {
                Console.Write("\nGrammar Rules:\n");
                for (int i = 0; i < Rules.Count; ++i)
                {
                    Console.Write($"\n{i + 1}:    ");
                    Rules[i].printWithColor(Type.Syn, Type.Inh);
                    Console.Write("\n");
                }
            }
        }

        // Генерация атрибутов для входной грамматики Воронов
        public void NewAT(List<Symbol> A,List<Symbol> Top,List<Symbol> L) {
      int i = 0;
      foreach (var p in Rules) {
        var F = new List<AttrFunction>();
        var AF = new List<Symbol>();

        Symbol B = p.LHS;
        B.attr=new List<Symbol>() { A[i] };
        ++i;

        foreach (var x in p.RHS) {
          if (T.Contains(x)) {
            if (L.Contains(x)) {
              x.attr=new List<Symbol>() { A[i] };
              ++i;
              AF.Add(x.attr[0]);
            } else {
              if (Top.Contains(x)) {
                AF.Add(x);
              }
            }
          } else {
            x.attr=new List<Symbol>() { A[i] };
            ++i;
            AF.Add(x.attr[0]);
          }

        }
        F.Add(new AttrFunction(B.attr,AF));
        p.F=F;
        i=0;
      }

    } // end NewAT 

        //### Тимофеев перевод цикла с С в Python 10.2        
        // AT-grammar for concret example 
        public void ATG_C_Py(List<Symbol> A, List<Symbol> Sign)
        {// символы атрибутов {a,n,s ...}, знаки операций {+, =, <}
            int i = 0;
            List<Symbol> TmpList = new List<Symbol>();// для нетерминалов из (1) 
            Symbol IdVal = null;
            foreach (var p in Rules)
            {
                var F = new List<AttrFunction>(); // вводимые атрибуты 
                var AF = new List<Symbol>(); // символ которому принадлежат вводимые атрибуты 

                var B = p.LHS;
                Symbol x1 = null, x2 = null;
                B.attr = new List<Symbol>();
                bool f = false;
                foreach (var x in p.RHS)
                {
                    if (T.Contains(x))
                    { // если символ терминальный
                        if (Sign.Contains(x))
                        {// если он является знаком 
                            B.attr = new List<Symbol>() { A[i] };
                            ++i;
                            int ind = p.RHS.IndexOf(x); // то определяем его индекс в списке RHS
                            x2 = p.RHS.ElementAt(ind + 1);// берем символ справа от знака 
                            x1 = p.RHS.ElementAt(ind - 1);// и слева 
                            if (x.symbol == "=")
                            {// если равно то синтезируем атрибуты 
                                f = false;
                                IdVal = p.LHS;
                                x1.attr = new List<Symbol>() { A[i] };
                                ++i;
                                x2.attr = new List<Symbol>() { A[i] };
                                ++i;
                                AF.Add(x1.attr[0]);
                            }
                            else
                            { // иначе синтезируем атрибут у константы а у переменной пораждаем из LHS (1)
                                TmpList.Add(p.LHS);
                                B.attr = new List<Symbol>() { A[i] };
                                ++i;
                                f = true;
                                x1.attr = new List<Symbol>() { A[i] };
                                ++i;
                                AF.Add(x1.attr[0]);
                                x2.attr = new List<Symbol>() { A[i] };
                                ++i;
                            }

                        }
                    }
                    else if (V.Contains(x) && x.symbol != "B")
                    {// если это нетерминал
                        f = false;
                        B.attr = new List<Symbol>();
                        ++i;
                        x.attr = new List<Symbol>();
                        ++i;
                        AF = new List<Symbol>();
                    }
                }

                if (f)
                {// если пораждаем от LHS
                    if (B.attr.Count > 0 && x1.attr.Count > 0)
                    {
                        f = false;
                        F.Add(new AttrFunction(x1.attr, B.attr));
                    }
                }
                else
                {// если пораждаем от RHS
                    if (B.attr.Count > 0 && AF.Count > 0)
                    {
                        F.Add(new AttrFunction(B.attr, AF));
                    }
                }

                p.F = F;
                i = 0;
            }// присвоены атрибуты нетерминалам 

            foreach (var p in Rules)
            { // идем по правилам чтобы найти эл-т из списка совпадающий с эл-том правила

                foreach (var x in p.RHS)
                { // идем по эл-там правила RHS
                    if (x.symbol == IdVal.symbol)
                    {
                        ++i;
                        x.attr = new List<Symbol>() { A[4] };
                        IdVal = x;
                    }
                }
            }
            i = 0;
            foreach (var p in Rules)
            { // идем по правилам чтобы найти эл-т из списка TmpList совпадающий с эл-том правила 

                foreach (var x in p.RHS)
                {   // идем по эл-там правила RHS
                    AttrFunction F1 = null;
                    Symbol tmpX = null;
                    foreach (var item in TmpList)
                    {   // идем по списку нетерминалов которые нужно соединить с D и сравниваем с эл-тами из RHS
                        if (x.symbol == item.symbol)
                        {
                            ++i;
                            x.attr = new List<Symbol>() { A[i] };
                            tmpX = x;
                        }
                    }
                    if (tmpX != null)
                    {
                        F1 = new AttrFunction(tmpX.attr, IdVal.attr);
                        tmpX = null;
                    }
                    if (F1 != null)
                    {
                        p.F.Add(F1);
                    }
                }
            }
        } // end ATG_C_Py 

        //##
        
        /// Печать грамматики
        public void Print()
        {
            Console.Write("\nAT-Grammar G = (V, T, OP, P, S)");
            Console.Write("\nV = { "); //нетерминальные символы
            for (int i = 0; i < V.Count; ++i)
            {
                V[i].print();
                if (i != V.Count - 1)
                    Console.Write(", ");
            }
            Console.Write(" },");
            Console.Write("\nT = { "); //терминальные
            for (int i = 0; i < T.Count; ++i)
            {
                T[i].print();
                if (i != T.Count - 1)
                    Console.Write(", ");
            }
            Console.Write(" },");

            var opf = new List<Symbol_Operation>(); //счётчик операционных символов, у которых есть атрибуты
            Console.Write("\nOP = { "); //операционные
            foreach (var op in OP)
            {
                op.print();
                if (op.attr != null)
                    opf.Add(op);
                Console.Write(", ");
            }
            Console.Write(" },");

            Console.Write("\nS = ");
            S0.print();

            //печать правил атрибутов операционных символов
            if (opf.Count != 0)
            {
                Console.Write("\nOperation Symbols Rules:\n");
                foreach (var op in opf)
                {
                    op.print();
                    Console.Write("\n");
                }
            }
            //печать правил грамматики
            if (Rules.Count != 0)
            {
                Console.Write("\nGrammar Rules:\n");
                for (int i = 0; i < Rules.Count; ++i)
                {
                    Console.Write("\n");
                    Rules[i].print();
                    Console.Write("\n");
                }
            }
        }

        private bool IsOper(string s) {
            return s=="+"||s=="-"||s=="*"||s=="/";
        }
        public void transform()
        {
            Console.WriteLine("\nPress Enter to start\n");
            Console.ReadLine();
            for (int i = 0; i < Rules.Count; ++i)
            {
                for (int j = 0; j < Rules[i].F.Count; ++j)
                { //обработка j-го атрибутного правила i-го правила грамматики
                    string NewOpS = "";
                    var atrs = new List<Symbol>();
                    var atrs_l = new List<Symbol>();
                    for (int k = 0; k < Rules[i].F[j].RH.Count; ++k)
                    { //проверка наличия функции в правой чаcnи правила
                        if (IsOper(Rules[i].F[j].RH[k].symbol))
                        {
                            NewOpS += Rules[i].F[j].RH[k]; //создание имени для нового оперционного символа
                        }
                        else
                        {
                            Symbol newAttr = new Symbol(Rules[i].F[j].RH[k] + "'");
                            atrs.Add(newAttr); //создание дублирующих символов для правил A <- a, но в формате a' <- a.
                            Type.Inh.Add(newAttr); //новый атрибут наследуемый
                            atrs_l.Add(Rules[i].F[j].RH[k]); //список атрибутов, входящих в функцию
                        }
                    }
                    if ((NewOpS.Count()) == 0) // проверка, что нет функций в правй части правила
                        continue;
                    NewOpS += i.ToString() + j.ToString(); //создание уникального имени операционного символа
                    Symbol newAttrAns = new Symbol(atrs[0] + "\'\'");
                    atrs.Add(newAttrAns); // добавление атрибута для результата функции
                    Type.Syn.Add(newAttrAns); //новый атрибут синтезируемый

                    this.OP.Add(new Symbol_Operation("{" + NewOpS + "}", atrs, new List<Symbol>() { new Symbol(atrs[0] + "\'\'") },
                        Rules[i].F[j].RH)); // добавление операционного символа с атрибутами и атрибутным правилом
//  Console.WriteLine("####### before ##########");
                    for (int k = 0; k < atrs.Count - 1; ++k)
                    { //добавление копирующих правил a' <- a  !
                      //Было: Rules[i].F.Add(new AttrFunction(new List<Symbol>() { atrs[k] }, new List<Symbol>() { atrs_l[k] }));
                      //Стало:          
                      Rules[i].F.Add(new AttrFunction(new List<Symbol>() { atrs_l[k] }, new List<Symbol>() { atrs[k] }));
                      // BUG при создании копирующих правил для вычисления атрибутов левая и правая часть при записи менялись местами,
                      //из-за чего возникала неправильная печать и добавлялись некоректные правила.
                    }
                    Rules[i].F.Add(new AttrFunction(new List<Symbol>(Rules[i].F[j].LH), new List<Symbol>() { new Symbol(atrs[0] + "\'\'") }));
                    //добавление правила z1, ... , zm <- p, где p - результат функции операционного символа
                    Rules[i].F.RemoveAt(j); //удаление правила с функцией в правой части
                    j -= 1;
                    for (int k = Rules[i].RHS.Count - 1; k >= 0; --k)
                    {
                        //поиск самой левой позииции для вставки операционного символа,
                        //начиная с самой правой позиции
                        int k1;
                        if (Rules[i].RHS[k].attr == null) //проверка того, что есть атрибуты у к-го символа правой
                                                          //части правила грамматики
                            continue;
                        for (k1 = 0; k1 < Rules[i].RHS[k].attr.Count; ++k1)
                        { //проверка, что у к-го символа нет атрибута, который есть у операционного символа,
                          //если он есть, то дальше мы не двигаемся и вставляем операционный символ перед ним, инче идём дальше до конца
                            if (atrs_l.Contains(Rules[i].RHS[k].attr[k1]))
                                break;
                        }
                        if (k1 < Rules[i].RHS[k].attr.Count)
                        { //нашли такой символ, справа от которого вставляем операционный
                            Rules[i].RHS.Insert(k + 1, new Symbol("{" + NewOpS + "}", atrs));
                            break;
                        }
                        if (k == 0)
                        { //дошли до конца правила и не нашли символа с хотя бы одним атрибутом, совпадающим с атрибутами операционного символа. Такого быть не должно, т.к. это означает, что атрибутные правила содержат атрибуты, отсутствующие у правила грамматики
                            Rules[i].RHS.Insert(k, new Symbol("{" + NewOpS + "}", atrs));
                            break;
                        }
                    }
                }
                //поиск лишних атрибутов в правилах типа
                // a1, ... , am <- k
                // b1, ..., k, ..., bn <- g
                // и замена на b1, ..., a1, ... , am, ..., bn <- g с удалением правила a1, ... , am <- k
                for (int r = 0; r < Rules[i].F.Count; ++r)
                {
                    bool deleted = false;
                    for (int l = r + 1; l < Rules[i].F.Count; ++l)
                    {
                        if (Rules[i].F[l].LH.Contains(Rules[i].F[r].RH[0]))
                        {
                            Rules[i].F[l].LH.Remove(Rules[i].F[r].RH[0]);
                            deleted = true;
                            foreach (var s in (Rules[i].F[r].LH))
                                Rules[i].F[l].LH.Add(s);
                        }
                    }
                    if (deleted)
                    {
                        Rules[i].F.RemoveAt(r);
                        r -= 1;
                    }
                }
                Console.WriteLine("\nChange for " + (i + 1).ToString() + "th rule\n");
                Rules[i].print();
                Console.ReadLine();
            }
        }

    } // and AGrammar

}
