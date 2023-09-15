using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaGlib.Core;
using RaGlib;

namespace RaGlib.Grammars {
    public class AttributeGrammar
    {

        //Атрибутная грамматика
        public List<Symbol> V; //Нетерминальные символы
        public List<Symbol> T; //Терминальные символы
        public List<List<Production>> R; //Множество правил (Правила продукции + семантические правила)
        public Symbol S; //Начальный символ
        
        public AttributeGrammar(List<Symbol> V, List<Symbol> T, List<List<Production>> R, Symbol S)
        {
            this.V = V;
            this.T = T;
            this.R = R;
            this.S = S;
        }

        public void AddRule(Production productionRule, List<Production> semanticRules)
        {
            R.Add(new List<Production>()
            {
                productionRule
            });
            for(int i = 0; i < semanticRules.Count; i++)
            {
                R[R.Count - 1].Add(semanticRules[i]);
            }
        }

        public string PrintRules()
        {
            string result = "R:\n";
            string helper;
            for(int i = 0; i < R.Count; i++)
            {
                helper = "\n";
                helper += R[i][0].LHS.symbol + " -> ";
                for (int j = 0; j < R[i][0].RHS.Count; j++)
                {
                    helper += R[i][0].RHS[j].symbol;
                }
                result += helper + "\n";
                for (int j = 1; j < R[i].Count; j++)
                {
                    helper = "";
                    helper += R[i][j].LHS.symbol + " <- ";
                    for (int k = 0; k < R[i][j].RHS.Count; k++)
                    {
                        helper += R[i][j].RHS[k].symbol;
                    }
                    result += helper + "\n";
                }
            }
            return result;
        }

        public string Print()
        {
            string result = "Входная грамматика:\n";
            string helper;
            helper = "V = {";
            for(int i = 0; i < V.Count - 1; i++)
            {
                helper += V[i].symbol + ", ";
            }
            helper += V[V.Count - 1].symbol + "}\n";
            result += helper; 
            helper = "T = {";
            for (int i = 0; i < T.Count - 1; i++)
            {
                helper += T[i].symbol + ", ";
            }
            helper += T[T.Count - 1].symbol + "}\n";
            result += helper + "\n";
            result += PrintRules() + "\n";
            result += "S0 = " + S.symbol + "\n";
            return result;
        }

    } // end AttributeGrammar

}
