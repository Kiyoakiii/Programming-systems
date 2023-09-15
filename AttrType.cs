using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaGlib.Core;
using RaGlib;

namespace RaGlib.Grammars
{
    public class AttrType   // множество Type - множество синтезируемых и наследуемых атрибутов
    {
        public List<Symbol> Syn = new List<Symbol>();   // множество синтезируемых атрибутов
        public List<Symbol> Inh = new List<Symbol>();   // множество наследуемых атрибутов

        public AttrType() { }

        public AttrType(List<Symbol> Syn, List<Symbol> Inh)
        {
            this.Syn = Syn;
            this.Inh = Inh;
        }
    }   //Синюков Мезенин М8О-206Б-21
}
