using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using RaGlib.Core;

namespace RaGlib.Grammars {
    public class AttrFunction   // 
    {
        public List<Symbol> LH; ///< Left part of the function
        public List<Symbol> RH; ///< Right part of the function

        public AttrFunction(List<Symbol> L, List<Symbol> R)
        {
            LH = new List<Symbol>(L);
            RH = new List<Symbol>(R);
        }

        public void print()
        {
            for (int i = 0; i < LH.Count; ++i)
            {
                Console.Write(LH[i]);
                if (i != (LH.Count - 1))
                {
                    Console.Write(", ");
                }
            }
            Console.Write(" <- ");
            for (int i = 0; i < RH.Count; ++i)
            {
                Console.Write(RH[i]);
            }
        }

        public void printFunctionWithColor(List<Symbol> Syn, List<Symbol> Inh)
        {
            Console.Write("      ");
            for (int i = 0; i < LH.Count; ++i)
            {
                if (Syn.Contains(LH[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(LH[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(LH[i]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                if (i != (LH.Count - 1))
                {
                    Console.Write(", ");
                }
            }
            Console.Write(" <- ");
            for (int i = 0; i < RH.Count; ++i)
            {
                if (Syn.Contains(RH[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(RH[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(RH[i]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                if (i != (RH.Count - 1))
                {
                    Console.Write(", ");
                }
                //Console.Write(i);
            }
        }
    }

}
