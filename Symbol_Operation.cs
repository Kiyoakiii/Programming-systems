using SDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RaGlib.Core
{

    public class Symbol_Operation : Symbol
    {

        public List<int> Parameters { get; private set; }

        public OPSymbolOperatrion function { set; get; } = null;

        public List<Symbol> Fattr;
        public Symbol_Operation(string s, List<Symbol> a, List<Symbol> L, List<Symbol> R) :
            base(s, a)
        {
            Fattr = a;
            function = new OPSymbolOperatrion(L, R);
            //      Console.WriteLine("############################### OPSymbol " );
        }

        public Symbol_Operation(string s, List<Symbol> a) : base(s, a) { }

        public Symbol_Operation(string s) : base(s) { }

        public Symbol_Operation(string s, List<int> parameters) : base(s)
        { 
            this.Parameters = parameters;
        }

        public static implicit operator Symbol_Operation(string expression)
        {
            var match = Regex.Match(expression, @"(\w+)(?:\{([\d,\s]*)\})?");
            var opSymbol = match.Groups[1].Value;
            var parameters = new List<int>();

            if (match.Groups[2].Success)
                foreach (var param in match.Groups[2].Value.Split(','))
                    if (int.TryParse(param.Trim(), out var value))
                        parameters.Add(value);

            return new Symbol_Operation(opSymbol, parameters);
        }

      

        public override void print()
        {
            Console.Write(this.symbol + ".");
            foreach (var a in attr)
                Console.Write(a.symbol);
            Console.Write(" ");
        }
        public void printFunctionWithColor(List<Symbol> Syn, List<Symbol> Inh)
        {
            Console.Write(this.symbol);
            foreach (var a in attr)
                if (Syn.Contains(a))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("." + a.symbol);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("." + a.symbol);
                }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(":    ");
            function.printWithColor(Syn, Inh);
        }


        public void printWithColor(List<Symbol> Syn, List<Symbol> Inh)
        {

            Console.Write(this.symbol);

            if (attr == null)
                return;

            foreach (var a in attr)
                if (Syn.Contains(a))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("." + a.symbol);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("." + a.symbol);
                }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

}
