using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RaGlib.Core
{
    
    public class OPSymbolOperatrion
    {
   

        public List<Symbol> LH; ///< Left part of the function
        public List<Symbol> RH; ///< Right part of the function
        public OPSymbolOperatrion(List<Symbol> L, List<Symbol> R)
        {
            LH = new List<Symbol>(L);
            RH = new List<Symbol>(R);
            //Console.Write(" \nOPSymbolOperatrion\n" );
        }

        public virtual void printWithColor(List<Symbol> Syn, List<Symbol> Inh)
        {
            foreach (var item in LH)
            {
                if (Syn.Contains(item))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(item);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(item);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                if (LH.IndexOf(item) != LH.Count - 1)
                {
                    Console.Write(" ");
                }
            }
            Console.Write(" <- ");
            foreach (var item in RH)
            {
                if (Syn.Contains(item))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(item);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(item);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                if (RH.IndexOf(item) != RH.Count - 1)
                {
                    Console.Write(" ");
                }
                //Console.Write(i);
            }

        }
        public bool execute()
        {
            return true;
        }
    }

}
