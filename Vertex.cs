using System;
using System.Collections.Generic;
using System.Linq;
using RaGlib.Grammars;

namespace RaGlib.Core
{
    public class Vertex
    {
        public Symbol Symbol; ///< Символ
        public LinkedList<Vertex> Next; ///< Потомки
        public List<AttrFunction> F;
        public List<int> Calculated;
        public List<Symbol> RealAttrs;
        public int Id; ///< Номер правила
        public bool isOP;
        public int pos;
        public List<Symbol> vals;

        public Vertex(Symbol S, bool op)
        {
            Symbol = S;
            Next = new LinkedList<Vertex>();
            isOP = op;
            int len = (S.attr == null ? 0 : S.attr.Count);
            Calculated = new List<int>(new int[len]);
        }
        public Vertex(Symbol S)
        {
            Symbol = S;
            Next = new LinkedList<Vertex>();
            isOP = false;
            int len = (S.attr == null ? 0 : S.attr.Count);
            Calculated = new List<int>(new int[len]);
        }
        public Vertex(Vertex vertex)
        {
            Symbol = vertex.Symbol;
            Next = vertex.Next;
            isOP = vertex.isOP;
            int len = (vertex.Symbol.attr == null ? 0 : vertex.Symbol.attr.Count);
            Calculated = vertex.Calculated;
        }

        public void Add(Vertex T)
        {
            Next.AddLast(T);
        }
        public Vertex Add(Symbol symbol, bool op)
        {
            Vertex temp = new Vertex(symbol, op);
            Next.AddLast(temp);
            return temp;
        }
        private string PrintList(List<int> l)
        {
            if (l == null)
            {
                return "";
            }
            string res = "";
            for (int i = 0; i < l.Count; ++i)
            {
                res += l.ElementAt(i);
                if (i + 1 < l.Count)
                {
                    res += ", ";
                }
            }

            return res;
        }
        public void Print(int d = 0)
        {
            Console.WriteLine("{0," + (d * 10).ToString() + "}", (isOP ? "{" : "") + Symbol + (Symbol.attr != null && Symbol.attr.Count > 0 ? "_" : "") + PrintList(Calculated) + (this.isOP ? "}" : ""));
            foreach (Vertex child in Enumerable.Reverse(Next))
            {
                child.Print(d + 1);
            }
        }
        public Vertex FindFirst(Vertex node, string need)
        {
            if (!node.Next.Any() && (node.Symbol == need || node.Symbol.ToString().Split("_")[0] == need))
            {
                return node;
            }

            foreach (var child in node.Next)
            {
                var result = FindFirst(child, need);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public int CountLeaves(Vertex node)
        {
            if (node.Next.Count == 0)
            {
                return (node.Symbol.attr == null ? 0 : node.Symbol.attr.Count);
            }

            int res = 0;
            foreach (var child in node.Next)
            {
                res += CountLeaves(child);
            }

            return res;
        }

        public void PrintCrown(Vertex node)
        {
            if (!node.Next.Any() && !node.isOP)
            {
                Console.Write(node.Symbol);
            }

            foreach (var child in node.Next)
            {
                PrintCrown(child);
            }
        }

        public void SetAttr(Vertex node)
        {
            if (node.Next.Count == 0)
            {
                if (node.Symbol.attr == null)
                {
                    return;
                }

                for (int i = 0; i < node.Symbol.attr.Count; ++i)
                {
                    node.Calculated[i] = Int32.Parse(vals.ElementAt(pos).ToString());
                    ++pos;
                }
                return;
            }

            foreach (var child in node.Next)
            {
                SetAttr(child);
            }

            if (node.Symbol.attr == null)
            {
                return;
            }

            for (int i = 0; i < node.Symbol.attr.Count; ++i)
            {
                foreach (var it in node.F)
                {
                    Symbol sym = it.LH[0];
                    if (sym == node.RealAttrs[i])
                    {
                        node.Calculated[i] = -1;
                        for (int CurrentP = 0; CurrentP < it.RH.Count; CurrentP += 2)
                        {

                            foreach (var child in node.Next)
                            {
                                if (child.Symbol.attr == null) continue;
                                bool ok = false;
                                for (int j = 0; j < child.Symbol.attr.Count; ++j)
                                {
                                    if (it.RH[CurrentP] == child.Symbol.attr[j])
                                    {
                                        if (CurrentP == 0) node.Calculated[i] = child.Calculated[j];
                                        else
                                        {
                                            if (it.RH[CurrentP - 1] == "+")
                                            {
                                                node.Calculated[i] += child.Calculated[j];
                                            }
                                            else if (it.RH[CurrentP - 1] == "-")
                                            {
                                                node.Calculated[i] -= child.Calculated[j];
                                            }
                                            else if (it.RH[CurrentP - 1] == "*")
                                            {
                                                node.Calculated[i] *= child.Calculated[j];
                                            }
                                            else if (it.RH[CurrentP - 1] == "/")
                                            {
                                                node.Calculated[i] /= child.Calculated[j];
                                            }
                                        }
                                        ok = true;
                                        break;
                                    }
                                }
                                if (ok)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return;
        }

    }
}