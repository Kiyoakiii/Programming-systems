using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.IO;         // for 4.0 Generator so far   
using System.Reflection; // for 4.0 Generator

using RaGlib;
using RaGlib.Core;
using RaGlib.Grammars;
using RaGlib.Automata;
using RaGlib.SetL;
using OperationsLib;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;

namespace RaGsystems
{

    class Program
    {
        static void Dialog()
        {
            Console.WriteLine("Наберите соответствующий номер лабораторной или курсовой и нажмите <Enter>");
            Console.WriteLine("Лабораторные работы:");
            Console.WriteLine("1.0 Построение регулярного выражения с использованием операционных символов");
            Console.WriteLine("1.1  Лемма о накачке рег., КС языки");
            Console.WriteLine("1.2  Составные автоматы");
            Console.WriteLine("2    Спроектировать конечный автомат DFS (lab 2)");
            Console.WriteLine("2.1  Алгоритм построения КА по заданной грамматике");
            Console.WriteLine("2.2  Грамматика с операционными символами и алгоритм  преобразование в КА");
            Console.WriteLine("2.3  Алгоритм построения КА по заданной грамматике + составные автоматы (конкатенация - merge(), объединение - union())");
            Console.WriteLine("2.4 3-стековый автомат для перевода выражения в постфиксную запись и 1-стековая машина для его вычисления");
            Console.WriteLine("3    НДКА ");
            Console.WriteLine("3.1  Convert NDFS to DFS (lab 3)");
            Console.WriteLine("3.2  Convert NDFS to DFS (lab 3) example");
            Console.WriteLine("3.3  Example FA with the extension of the AddRule by Regex [A-Z]  ");
            Console.WriteLine("");
            Console.WriteLine("4.0  Генератор заданий Grammar_generator");
            Console.WriteLine("");
            Console.WriteLine("4.1  Приведение граматики к нормальной форме CFGrammar (lab 4 - 6)");
            Console.WriteLine("6.1  Grammar in Greibach normal form");
            Console.WriteLine("6.2  Grammar in Chomsky normal form");
            Console.WriteLine("7    Алгоритм построения МП автомата по приведенной КС-грамматики PDA (lab 7-8)");
            Console.WriteLine("7.1 КС-грамматика в МП-автомат пример 1");
            Console.WriteLine("7.2 КС-грамматика в МП-автомат пример 2");
            Console.WriteLine("7.3 МП - автомат пример 3");
            Console.WriteLine("7.4 НДМП - автомат пример 4");
            Console.WriteLine("7.5 МП - автомат пример 5");
            Console.WriteLine("7.10 Конфигурируемый МП-автомат: недетерминированный МПА");
            Console.WriteLine("7.11 Расширенный МП-автомат (детерминированный)");
            Console.WriteLine("7.12 Конфигурируемый МП-автомат: недетерминированный РМПА");
            Console.WriteLine("7.13 МП-автомат с альтернативами");


            Console.WriteLine("");
            Console.WriteLine("9.1  Для LL(1) анализатора построить управляющую таблицу M.\n" +
                "     Аналитически написать такты работы LL(1) анализатора для выведенной цепочки.");
            Console.WriteLine("9.2  Для LL(1) анализатора построить управляющую таблицу M.\n" +
                "     Аналитически написать такты работы LL(1) анализатора для выведенной цепочки с подробным разбором.");
            Console.WriteLine("");
            Console.WriteLine("14   Построить каноническую форму множества ситуаций.\n" +
                "     Построить управляющую таблицу для функции перехода  g(х) и действий f(u)");
            Console.WriteLine("16.1 LR(0) using g(X), f(a)");
            Console.WriteLine("16.2 LR(0) using g(X), f(a) example");
            Console.WriteLine("16.3 LR(1) using g(X), f(a)");
            Console.WriteLine("16.4 LR(1) using g(X), f(a) example ");
            Console.WriteLine("17.1 Грамматика простого предшествования пример 1");
            Console.WriteLine("17.2 Грамматика простого предшествования пример 2");
            Console.WriteLine("18.1 Грамматика операторного предшествования");
            Console.WriteLine("18.2 Грамматика операторного предшествования пример");
            Console.WriteLine("");
            Console.WriteLine("Курсовые проеты: I");
            Console.WriteLine("I1   Терия перевода SDTSchemata");
            Console.WriteLine("I2   Преобразование КС-грамматики в транслирующую с операционными символами");
            Console.WriteLine("I3   Grammar to AT-Grammar");
            Console.WriteLine("I4   AT-Grammar");
            Console.WriteLine("I5   AT-Grammar for python vars types");
            Console.WriteLine("I6   AT-grammar for translating for C ++ into Python");
            Console.WriteLine("");
            Console.WriteLine("I7   Chain Translation example");
            Console.WriteLine("I8   L-attribute translation");
            Console.WriteLine("I9   Parse Tree translation");
            Console.WriteLine("");
            Console.WriteLine("I10  SDT schema builder");
            Console.WriteLine("I11  SetL");
            Console.WriteLine("I12  Построение дерева разбора по транслирующей грамматике");
            Console.WriteLine("I18  Преобразование деревьев при помощи СУ-схемы");
            Console.WriteLine("I20  Построение L-атрибутивного ДМП процессора");
            Console.WriteLine("I21  Преобразование атрибутной грамматики в дерево зависимостей");
            Console.WriteLine("I22  Вычисление значений атрибутов по дереву зависимостей");
            Console.WriteLine("I23 Turing on operation symbols");
            Console.WriteLine("I24 Трансляция класса из C++ в Python");
            Console.WriteLine("I25 Перевод транслирующей грамматики методом рекурсивного спуска");
            Console.WriteLine("q ");
        }

        static void Main()
        {
            // "ε", "\u03B5"  Greek alphabet
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            for (; ; )
            {
                Dialog();
                switch (Console.ReadLine())
                {
                    case "q":
                        {
                            Console.WriteLine($"\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nДавайте проверим вашу AT-грамматику на L-атрибутивность:\n");

                            var attgr_3 = new ATGrammar(
                                new List<Symbol>() { "S", new Symbol("E", new List<Symbol>() { "a1" }) ,
                                                          new Symbol("R", new List<Symbol>() { "b1", "b3" }) },
                                new List<Symbol>() { "*", "+", new Symbol("i", new List<Symbol>() { "c1" }) },
                                new List<Symbol_Operation>() { new Symbol_Operation("{:=}", new List<Symbol>() { "d1", "d2" },
                                                                                             new List<Symbol>() { "d2" }, new List<Symbol>() { "d1" } ),
                                                               new Symbol_Operation("{+}", new List<Symbol>() { "e1","e2","e3" },
                                                                                             new List<Symbol>() { "e3" },new List<Symbol>() {"e1" ,"+", "e2" }),
                                                                new Symbol_Operation("{*}", new List<Symbol>() { "f1","f2","f3" },
                                                                                             new List<Symbol>() { "f3" }, new List<Symbol>() { "f1", "*", "f2"})},
                                new Symbol("S"));

                            attgr_3.Type.Inh = new List<Symbol>() { "b1", "b2", "c1", "d1", "d2", "e1", "e2", "e3", "f1", "f2", "f3" };
                            attgr_3.Type.Syn = new List<Symbol>() { "a1", "b3", "b4", "b5", "e4", "e5" };


                            attgr_3.Addrule(new Symbol("S"),
                                            new List<Symbol>() { new Symbol("i"), ":=", new Symbol("E"), new Symbol_Operation("{:=}") },
                                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "d1" }, new List<Symbol> { "c1" }),
                                                                       new AttrFunction(new List<Symbol>() { "d2" }, new List<Symbol> { "a1" }) });
                            attgr_3.Addrule(new Symbol("E"),
                                            new List<Symbol>() { new Symbol("i"), new Symbol("R") },
                                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "b1" }, new List<Symbol> { "c1" }),
                                                                       new AttrFunction(new List<Symbol>() { "a1" }, new List<Symbol> { "b3" }) });

                            attgr_3.Addrule(new Symbol("R"),
                                            new List<Symbol>() { "+", new Symbol("i"), new Symbol("{+}"), new Symbol("R", new List<Symbol>() { "b2", "b4" }) },
                                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "b4", "e3" }, new List<Symbol> { "GETNEW" }),
                                                                       new AttrFunction(new List<Symbol>() { "e1" }, new List<Symbol> { "b3" }),
                                                                       new AttrFunction(new List<Symbol>() { "e2" }, new List<Symbol> { "c1" }),
                                                                       new AttrFunction(new List<Symbol>() { "c1" }, new List<Symbol> { "b4" }),  //case1
                                                                       new AttrFunction(new List<Symbol>() { "c1" }, new List<Symbol> { "b3" }),  //case1
                                                                       new AttrFunction(new List<Symbol>() { "b3" }, new List<Symbol> { "b5" }),  //case2
                                                                       new AttrFunction(new List<Symbol>() { "b3" }, new List<Symbol> { "b1" }), //case2 
                                                                       new AttrFunction(new List<Symbol>() { "e4" }, new List<Symbol> { "e5" }),  //case3
                                                                       new AttrFunction(new List<Symbol>() { "b1" }, new List<Symbol> { "b2" })});
                            attgr_3.Addrule(new Symbol("R"),
                                            new List<Symbol>() { "*", new Symbol("i"), new Symbol("{*}"), new Symbol("R", new List<Symbol>() { "b2", "b4" }) },
                                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "b4", "f3" }, new List<Symbol> { "GETNEW" }),
                                                                       new AttrFunction(new List<Symbol>() { "f1" }, new List<Symbol> { "b3" }),
                                                                       new AttrFunction(new List<Symbol>() { "f2" }, new List<Symbol> { "c1" }),
                                                                       new AttrFunction(new List<Symbol>() { "b1" }, new List<Symbol> { "b2" })});

                            attgr_3.Addrule(new Symbol("R"),
                                           new List<Symbol>() { new Symbol("e") },
                                           new List<AttrFunction>() { new AttrFunction(new List<Symbol>() { "d1" }, new List<Symbol> { "b3" }) });


                            attgr_3.print();

                            // Проверяем, является ли ATGrammar L-атрибутивной
                            // Выводим результат
                            if (attgr_3.IsLATGrammar())
                            {
                                Console.WriteLine("\nATGrammar is L-attributive.");
                            }
                            else
                            {
                                Console.WriteLine("\nATGrammar is not L-attributive.");
                            }
                            Console.WriteLine($"\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");



                            break;
                        }


                    case "1.0":
                        {
                            var D = new Dictionary<Symbol_Operation, Func<string, int, int, (bool, string)>>() {
                                {"{a1}", Operations.OperationA},
                                {"{a2}", Operations.OperationB},
                                {"{a3}", Operations.OperationC},
                                {"{a4}", Operations.OperationD},
                                {"{a5}", Operations.OperationE},
                                {"{a6}", Operations.OperationF},
                                {"{a7}", Operations.OperationG},
                                {"{a8}", Operations.OperationH},
                            };

                            var D2 = new Dictionary<Symbol_Operation, Delegate>();

                            foreach (var item in D)
                            {
                                D2.Add(item.Key, item.Value);
                            }

                            var automate = new FSAutomateWithOpSymbols(
                                new List<Symbol>() { "A0" },
                                new List<Symbol>() { },
                                new List<Symbol_Operation>() { "{a1}", "{a2}", "{a3}", "{a4}", "{a5}", "{a6}", "{a7}" },
                                new List<Symbol>() { "qf" },
                                "A0",
                                D2
                            );

                            Console.WriteLine("Операционные символы:");
                            Console.WriteLine("\t{a1} (\\W) - проверка символа на то, что он не является символом латиницы, цифрой или нижним подчёркиванием");
                            Console.WriteLine("\t{a2} (p) - проверка символа на соответствие символу 'p'");
                            Console.WriteLine("\t{a3} (o) - проверка символа на соответствие символу 'p'");
                            Console.WriteLine("\t{a4} ([#-]) - проверка символа на соответствие символу '#' или '-'");
                            Console.WriteLine("\t{a5} (\\s) - проверка символа на то, что он является пробельным");
                            Console.WriteLine("\t{a6} (\\d) - проверка символа на то, что он является цифрой");
                            Console.WriteLine("\t{a7} ([\\s-]) - проверка символа на то, что он является пробельным или равен '-'");
                            Console.Write("Примечание : Операционные символы имеют параметры, представляющие");
                            Console.WriteLine(" собой пару чисел a, b. Эти числа обозначают собой диапазон кол-ва повторений паттерна.");
                            Console.WriteLine("Пример: набор операционных символов {a1{0, 1}} соответствует паттерну \\W{0, 1}");
                            Console.WriteLine();

                            Console.Write("Входная цепока символов: ");
                            string p = Console.ReadLine();

                            Console.Write("Набор операционных символов: ");
                            string s = Console.ReadLine();
                            var x = Regex.Matches(s, @"((\w+)(?:\{([\d,\s]*)\})?)");
                            int i = 0;

                            foreach (Match match in x)
                            {
                                automate.AddRule($"A{i}", match.Value, $"A{i + 1}");
                                i++;
                            }

                            automate.F = new List<Symbol>() { new Symbol($"A{i}") };

                            automate.Execute(p);
                            Console.ReadLine();
                        }
                        break;

                    case "1.1": // Pumping lemma

                        var check_pumping = new Pumping();
                        check_pumping.Dialog();

                        break;
                    case "1.2": // Compound automata  Lab 1.
                        FSAutomate[] automats = new FSAutomate[] {
                new FSAutomate(
                        new List<Symbol>() { "S01", "A1", "B1", "C1", "qf1" },
                        new List<Symbol>() { "0" },
                        new List<Symbol>() { "qf1" },
                        "S01"
                    ),
                new FSAutomate(
                        new List<Symbol>() { "S02", "A2", "B2", "C2", "D2", "qf2" },
                        new List<Symbol>() { "0", "1", "(", ")", "+" },
                        new List<Symbol>() { "qf2" },
                        "S02"
                    ),
                new FSAutomate(
                        new List<Symbol>() { "S03", "qf3" },
                        new List<Symbol>() { "0", "1" },
                        new List<Symbol>() { "qf3" },
                        "S03"
                    ),
            };
                        //deltas
                        automats[0].AddRule("S01", "0", "A1");
                        automats[0].AddRule("A1", "0", "B1");
                        automats[0].AddRule("B1", "0", "C1");
                        automats[0].AddRule("C1", "0", "A1");
                        automats[0].AddRule("C1", "0", "qf1");

                        automats[1].AddRule("S02", "(", "A2");
                        automats[1].AddRule("A2", "0", "B2");
                        automats[1].AddRule("B2", "+", "C2");
                        automats[1].AddRule("C2", "1", "D2");
                        automats[1].AddRule("D2", ")", "qf2");

                        automats[2].AddRule("S03", "0", "S03");
                        automats[2].AddRule("S03", "0", "qf3");
                        automats[2].AddRule("S03", "1", "S03");
                        automats[2].AddRule("S03", "1", "qf3");
                        automats[2].AddRule("S03", "", "qf3");

                        var dka1 = new FSAutomate(); // правильно
                        dka1.BuildDeltaDKAutomate(automats[0], false); // правильно
                        var dka2 = new FSAutomate();
                        dka2.BuildDeltaDKAutomate(automats[1], false);
                        var dka3 = new FSAutomate();
                        dka3.BuildDeltaDKAutomate(automats[2], false);

                        // правильно
                        var automats1 = new FSAutomate[] { dka1, dka2, dka3, };
                        //merge
                        var merged12 = Compound_FSAutomate.Merge2(dka1, dka2); // неправильно
                        var merged23 = Compound_FSAutomate.Merge2(dka2, dka3);
                        var merged123 = Compound_FSAutomate.Merge(automats1);
                        var union12 = Compound_FSAutomate.Union2(dka1, dka2);
                        var union1 = new FSAutomate();
                        union1.BuildDeltaDKAutomate(union12, false);
                        var union123 = Compound_FSAutomate.Union(automats1);
                        var union2 = new FSAutomate();
                        union2.BuildDeltaDKAutomate(union123, false);

                        var exectionOrder = new FSAutomate[] { union1, union2, merged12, merged23, merged123 };
                        string[] names = { "объединение КА1, КА2", "объединение КА1, КА2, КА3", "КА1+КА2", "КА2+КА3", "КА1+КА2+КА3" };

                        Console.WriteLine();

                        Console.WriteLine("Были построены составные автоматы:");
                        Console.WriteLine("1. объединение КА1, КА2;");
                        Console.WriteLine("2. объединение КА1, КА2, КА3;");
                        Console.WriteLine("3. КА1 +КА2;");
                        Console.WriteLine("4. КА2+КА3;");
                        Console.WriteLine("5. КА1+КА2+КА3;");

                        Console.WriteLine();
                        Console.WriteLine("Примеры цепочек для постоенных составных автоматов 1, 2, 3, 4, 5: ");
                        Console.WriteLine("0, 1, 01, 0000, 0000000, (0+1), 0000(0+1), 0000(0+1)0, 0000(0+1)1");
                        for (; ; )
                        {
                            Console.WriteLine();
                            Console.WriteLine("Введите цепочку (или выйти stop):");
                            string s = Console.ReadLine();
                            Console.WriteLine();
                            if (s == "stop") break;

                            for (int i = 0; i < exectionOrder.Length; i++)
                            {
                                if (exectionOrder[i].Execute_FSA(s))
                                {
                                    Console.WriteLine("Автомат " + names[i] + " распознал цепочку " + s);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Автомат " + names[i] + " не распознал цепочку " + s);
                                    if (i == exectionOrder.Length - 1)
                                        Console.WriteLine("Данная цепочка не была распознана");
                                    //  Console.ReadLine();
                                }
                            }
                        }
                        break;
                    case "2":
                        FSAutomate[] automates = new FSAutomate[] {
            new FSAutomate(
                new List<Symbol>() { "S0","A","B","C","D","E","F","G","H","I","J","K","L","M","N","qf" },
                new List<Symbol>() { "s", "t", "o", "c", "k", "i", "p", "#", "&", " " },
                new List<Symbol>() { "qf" },
                "S0"
            ),
            new FSAutomate(
                new List<Symbol>() { "S0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "N1", "N2", "M", "qf" },
                new List<Symbol>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "." },
                new List<Symbol>() { "qf" },
                "S0"
            )
          };

                        // First automate
                        automates[0].AddRule("S0", "#", "S");
                        automates[0].AddRule("S", "s", "A");
                        automates[0].AddRule("A", "t", "B");
                        automates[0].AddRule("B", "o", "C");
                        automates[0].AddRule("C", "c", "D");
                        automates[0].AddRule("D", "k", "E");
                        automates[0].AddRule("E", "t", "G");
                        automates[0].AddRule("E", " ", "E1");
                        automates[0].AddRule("E1", "t", "G");
                        automates[0].AddRule("E1", " ", "E2");
                        automates[0].AddRule("E2", "t", "G");
                        automates[0].AddRule("E2", " ", "E3");
                        automates[0].AddRule("E3", "t", "G");
                        automates[0].AddRule("G", "i", "H");
                        automates[0].AddRule("H", "p", "I");
                        automates[0].AddRule("I", "s", "K");
                        automates[0].AddRule("K", "&", "qf");

                        //second automate

                        automates[1].AddRule("S0", "1", "A");
                        automates[1].AddRule("A", "9", "B");
                        automates[1].AddRule("B", "2", "C");
                        automates[1].AddRule("C", ".", "D");
                        automates[1].AddRule("D", "1", "E");
                        automates[1].AddRule("E", "6", "F");
                        automates[1].AddRule("F", "8", "G");
                        automates[1].AddRule("G", ".", "H");
                        automates[1].AddRule("H", "1", "I");
                        automates[1].AddRule("I", ".", "J");
                        string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        foreach (string curNumber in numbers)
                        {
                            automates[1].AddRule("J", curNumber, "N1");
                            automates[1].AddRule("N1", curNumber, "N2");
                            automates[1].AddRule("N2", curNumber, "M");
                        }

                        automates[1].AddRule("N1", "ε", "qf");
                        automates[1].AddRule("N2", "ε", "qf");
                        automates[1].AddRule("M", "ε", "qf");

                        Console.WriteLine("Enter line to execute first:");
                        automates[0].Execute(Console.ReadLine() + "ε");


                        Console.WriteLine("Enter line to execute second:");
                        automates[1].Execute(Console.ReadLine() + "ε");
                        break;

                    case "2.1":
                        var Gram = new Grammar(new List<Symbol>() { "0", "1" },
                        new List<Symbol>() { "S0", "A", "B" },
                        "S0");
                        Gram.AddRule("S0", new List<Symbol>() { "0" });
                        Gram.AddRule("S0", new List<Symbol>() { "0", "A" });
                        Gram.AddRule("A", new List<Symbol>() { "1", "B" });
                        Gram.AddRule("B", new List<Symbol>() { "0" });
                        Gram.AddRule("B", new List<Symbol>() { "0", "A" });

                        // From Automaton Grammar to State Machine(KA)
                        var KA = Gram.Transform();
                        KA.DebugAuto();
                        break;

                    case "2.2":
                        var GramOP = new GrammarWithOpSymbol(new List<Symbol>() { "a", "e", "f", "d" },
                                                new List<Symbol_Operation>() { new Symbol_Operation("{save}"), new Symbol_Operation("{split}"), new Symbol_Operation("{load}") },
                                               new List<Symbol>() { "S0", "E", "F", "D" },
                                               "S0");
                        GramOP.AddRule("S0", new List<Symbol>() { "a", "E" });
                        GramOP.AddRule("E", new List<Symbol>() { "e", "F", "{load}" });
                        GramOP.AddRule("F", new List<Symbol>() { "f", "D", "{split}" });
                        GramOP.AddRule("D", new List<Symbol>() { "d", "{save}" });

                        var KAOP = GramOP.Transform();
                        KAOP.DebugAuto();
                        Console.WriteLine("Enter line to execute :");
                        KAOP.Execute(Console.ReadLine());
                        Console.ReadLine();
                        break;

                    case "2.3":

                        // grammar 1
                        var grammar2_2__1 = new Grammar(new List<Symbol>() { "a", "l", "t", "i", "p", "s", " " },
                                       new List<Symbol>() { "S0", "A", "B", "C", "D", "E", "F", "G" }, "S0");
                        grammar2_2__1.AddRule("S0", new List<Symbol>() { "a", "A" });
                        grammar2_2__1.AddRule("A", new List<Symbol>() { "l", "B" });
                        grammar2_2__1.AddRule("B", new List<Symbol>() { "l", "C" });
                        grammar2_2__1.AddRule("C", new List<Symbol>() { " ", "D" });
                        grammar2_2__1.AddRule("D", new List<Symbol>() { "t", "E" });
                        grammar2_2__1.AddRule("E", new List<Symbol>() { "i", "F" });
                        grammar2_2__1.AddRule("F", new List<Symbol>() { "p", "G" });
                        grammar2_2__1.AddRule("G", new List<Symbol>() { "s" });

                        // From Automaton Grammar to State Machine(KA)
                        var fsa_1 = grammar2_2__1.Transform();
                        Console.WriteLine("fsa_1:\n");
                        fsa_1.DebugAuto();
                        Console.WriteLine("Enter line to execute(fsa1) :");
                        string str_1 = "all tips";
                        fsa_1.Execute(str_1);

                        // grammar 3
                        var grammar2_2__3 = new Grammar(new List<Symbol>() { "g", "r", "a", "v", "i", "a", "!", "?", "*", " " },
                                       new List<Symbol>() { "S0", "A", "B", "C", "D", "E", "F" }, "S0");
                        grammar2_2__3.AddRule("S0", new List<Symbol>() { "!", "S0" });
                        grammar2_2__3.AddRule("S0", new List<Symbol>() { "*", "S0" });
                        grammar2_2__3.AddRule("S0", new List<Symbol>() { "?", "S0" });
                        grammar2_2__3.AddRule("S0", new List<Symbol>() { "g", "A" });
                        grammar2_2__3.AddRule("A", new List<Symbol>() { "r", "B" });
                        grammar2_2__3.AddRule("B", new List<Symbol>() { "a", "C" });
                        grammar2_2__3.AddRule("B", new List<Symbol>() { "@", "C" });
                        grammar2_2__3.AddRule("C", new List<Symbol>() { "v", "D" });
                        grammar2_2__3.AddRule("D", new List<Symbol>() { "i", "E" });
                        grammar2_2__3.AddRule("D", new List<Symbol>() { "1", "E" });
                        grammar2_2__3.AddRule("D", new List<Symbol>() { "!", "E" });
                        grammar2_2__3.AddRule("E", new List<Symbol>() { "a", "C" });
                        grammar2_2__3.AddRule("E", new List<Symbol>() { "@", "C" });
                        grammar2_2__3.AddRule("F", new List<Symbol>() { "!", "F" });
                        grammar2_2__3.AddRule("F", new List<Symbol>() { "*", "F" });
                        grammar2_2__3.AddRule("F", new List<Symbol>() { "?", "F" });
                        grammar2_2__3.AddRule("F", new List<Symbol>() { " " });

                        // From Automaton Grammar to State Machine(KA)
                        var fsa_3 = grammar2_2__3.Transform();
                        Console.WriteLine("fsa_3:\n");
                        fsa_3.DebugAuto();
                        Console.WriteLine("Enter line to execute(fsa3) :");
                        string str_3 = "gravia ";
                        fsa_3.Execute(str_3);

                        // grammar 9
                        var grammar2_2__9 = new Grammar(new List<Symbol>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " " },
                                       new List<Symbol>() { "S0", "A", "B", "C", "D", "E", "F", "G" }, "S0");
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "0", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "1", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "2", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "3", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "4", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "5", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "6", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "7", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "8", "A" });
                        grammar2_2__9.AddRule("S0", new List<Symbol>() { "9", "A" });

                        grammar2_2__9.AddRule("A", new List<Symbol>() { "0", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "1", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "2", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "3", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "4", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "5", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "6", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "7", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "8", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { "9", "A" });
                        grammar2_2__9.AddRule("A", new List<Symbol>() { " " });

                        // From Automaton Grammar to State Machine(KA)
                        var fsa_9 = grammar2_2__9.Transform();
                        Console.WriteLine("fsa_9:\n");
                        fsa_9.DebugAuto();
                        Console.WriteLine("Enter line to execute(fsa9) :");
                        string str_9 = "124134 ";
                        fsa_9.Execute(str_9);


                        var FSAs = new FSAutomate[] { fsa_1, fsa_3, fsa_9 };
                        //merge
                        var Concatenation123 = Compound_FSAutomate.Merge(FSAs);
                        var Union123 = Compound_FSAutomate.Union(FSAs);

                        Console.WriteLine("\nMerge of three automatas");
                        Concatenation123.DebugAuto();

                        Console.WriteLine("\nUnion of three automatas");
                        Union123.DebugAuto();
                        Console.WriteLine();

                        Console.WriteLine("Enter line to execute(Concatenation123) :");
                        Concatenation123.Execute("ara tips ");
                        Console.WriteLine("Enter line to execute(union123) :");
                        Union123.Execute("eeall tips");

                        break;

                    case "2.4":
                        {
                            Console.WriteLine("Перевод выражения в постфиксную запись:");
                            ThreeStackAutomat tsa = new ThreeStackAutomat(new List<string>() { "(", ")", "+", "-", "*", "/", "A", "B", "C", "D", "E" },
                                                          new List<string>() { "(", ")", "+", "-", "*", "/", "A", "B", "C", "D", "E", "bot" },
                                                          new List<string>() { "q1", "q2", "q3", "q4" },
                                                          "q1", new List<string>() { "q4" }, "bot");
                            foreach (string sym in tsa.sigma)
                                tsa.AddRule("q1", new List<string>() { sym, "", "", "" }, "q1", new List<string>() { "", sym, "" });
                            tsa.AddRule("q1", new List<string>() { "", "", "", "" }, "q2", new List<string>() { "", "", "" });
                            foreach (string sym in tsa.gamma)
                            {
                                if (sym != "bot")
                                    tsa.AddRule("q2", new List<string>() { "", "", sym, "" }, "q2", new List<string>() { sym, "", "" });
                            }
                            tsa.AddRule("q2", new List<string>() { "", "", "bot", "" }, "q3", new List<string>() { "", "bot", "" });
                            foreach (string sym in new List<string>() { "A", "B", "C", "D", "E" })
                                tsa.AddRule("q3", new List<string>() { "", sym, "", "" }, "q3", new List<string>() { "", sym, "" });
                            foreach (string sym1 in new List<string>() { "+", "-" })
                            {
                                foreach (string sym2 in new List<string>() { "+", "-", "*", "/" })
                                    tsa.AddRule("q3", new List<string>() { "", sym1, "", sym2 }, "q3", new List<string>() { sym1, sym2, "" });
                            }
                            foreach (string sym1 in new List<string>() { "*", "/" })
                            {
                                foreach (string sym2 in new List<string>() { "*", "/" })
                                    tsa.AddRule("q3", new List<string>() { "", sym1, "", sym2 }, "q3", new List<string>() { sym1, sym2, "" });
                            }
                            foreach (string sym in new List<string>() { "+", "-", "*", "/" })
                                tsa.AddRule("q3", new List<string>() { "", sym, "", "" }, "q3", new List<string>() { "", "", sym });
                            tsa.AddRule("q3", new List<string>() { "", "(", "", "" }, "q3", new List<string>() { "", "", "(" });
                            foreach (string sym in tsa.gamma)
                            {
                                if (sym != "(")
                                    tsa.AddRule("q3", new List<string>() { "", ")", "", sym }, "q3", new List<string>() { ")", sym, "" });
                            }
                            tsa.AddRule("q3", new List<string>() { "", ")", "", "(" }, "q3", new List<string>() { "", "", "" });
                            foreach (string sym in new List<string>() { "+", "-", "*", "/" })
                                tsa.AddRule("q3", new List<string>() { "", "bot", "", sym }, "q3", new List<string>() { "bot", sym, "" });
                            tsa.AddRule("q3", new List<string>() { "", "bot", "", "bot" }, "q4", new List<string>() { "bot", "", "bot" });
                            foreach (string sym in tsa.gamma)
                            {
                                if (sym != "bot")
                                    tsa.AddRule("q4", new List<string>() { "", "", sym, "" }, "q4", new List<string>() { "", "", sym });
                            }

                            List<Stack<string>> stacks = tsa.Execute(new List<string>() { "A", "+", "B", "-", "C", "+", "D", "*", "E" });
                            Console.WriteLine("В инфиксной: A + B - C + D * E");
                            Console.Write("В постфиксной: ");
                            while (stacks[2].Peek() != "bot")
                                Console.Write(stacks[2].Pop() + " ");
                            Console.WriteLine();

                            Console.WriteLine("Вычисление выражения в постфиксной записи:");
                            List<string> sigma = new List<string>() { "+", "-", "*", "/" };
                            for (int i = -25; i < 26; i++)
                                sigma.Add(i.ToString());
                            List<string> gamma = sigma;
                            gamma.Add("bot");

                            StackMachine sm = new StackMachine(sigma, gamma,
                                                               new List<string>() { "q", "q+", "q-", "q*", "q/" },
                                                               "q", new List<string>() { "q" }, "bot");
                            foreach (string op in new List<string>() { "+", "-", "*", "/" })
                            {
                                for (int i = -25; i < 26; i++)
                                {
                                    for (int j = -25; j < 26; j++)
                                        sm.AddRule("q", new List<string>() { op, i.ToString(), j.ToString() }, "q" + op, new List<string>() { j.ToString(), i.ToString() });
                                }
                            }
                            for (int i = -25; i < 26; i++)
                                sm.AddRule("q", new List<string>() { i.ToString() }, "q", new List<string>() { i.ToString() });

                            for (int i = -25; i < 26; i++)
                            {
                                Pair cur = new Pair("q", new List<string>() { i.ToString() });
                                Pair next = new Pair("q", new List<string>() { i.ToString() });
                                sm.AddFunction(cur, next);
                            }
                            for (int i = -25; i < 26; i++)
                            {
                                for (int j = -25; j < 26; j++)
                                {
                                    int res = (i + j) % 26;
                                    Pair cur = new Pair("q+", new List<string>() { i.ToString(), j.ToString() });
                                    Pair next = new Pair("q", new List<string>() { res.ToString() });
                                    sm.AddFunction(cur, next);
                                }
                            }
                            for (int i = -25; i < 26; i++)
                            {
                                for (int j = -25; j < 26; j++)
                                {
                                    int res = (i - j) % 26;
                                    Pair cur = new Pair("q-", new List<string>() { i.ToString(), j.ToString() });
                                    Pair next = new Pair("q", new List<string>() { res.ToString() });
                                    sm.AddFunction(cur, next);
                                }
                            }
                            for (int i = -25; i < 26; i++)
                            {
                                for (int j = -25; j < 26; j++)
                                {
                                    int res = (i * j) % 26;
                                    Pair cur = new Pair("q*", new List<string>() { i.ToString(), j.ToString() });
                                    Pair next = new Pair("q", new List<string>() { res.ToString() });
                                    sm.AddFunction(cur, next);
                                }
                            }
                            for (int i = -25; i < 26; i++)
                            {
                                for (int j = -25; j < 26; j++)
                                {
                                    if (j == 0)
                                        continue;
                                    int res = (i / j) % 26;
                                    Pair cur = new Pair("q/", new List<string>() { i.ToString(), j.ToString() });
                                    Pair next = new Pair("q", new List<string>() { res.ToString() });
                                    sm.AddFunction(cur, next);
                                }
                            }

                            Stack<string> stack = sm.Execute(new List<string>() { "1", "2", "+", "3", "-", "4", "5", "*", "+" });
                            Console.Write("Результат вычисления 1 2 + 3 - 4 5 * + равен ");
                            Console.WriteLine(stack.Peek());
                            break;
                        }

                    case "3.1":
                        FSAutomate[] ndfsa = new FSAutomate[] {
            new FSAutomate(
                new List<Symbol>() { "S0","A","B","C","D","E","F","G","H","I","J","K","L","N","qf" },
                new List<Symbol>() { "s", "t", "o", "c", "k", "i", "p", "#", "&", " " },
                new List<Symbol>() { "qf" },
                "S0"
            ),
            new FSAutomate(
                new List<Symbol>() { "S0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "N1", "N2", "M", "qf" },
                new List<Symbol>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "." },
                new List<Symbol>() { "qf" },
                "S0"
            )
          };

                        // First automate
                        ndfsa[0].AddRule("S0", "#", "S");
                        ndfsa[0].AddRule("S", "s", "A");
                        ndfsa[0].AddRule("A", "t", "B");
                        ndfsa[0].AddRule("B", "o", "C");
                        ndfsa[0].AddRule("C", "c", "D");
                        ndfsa[0].AddRule("D", "k", "E");
                        ndfsa[0].AddRule("E", "t", "G");
                        ndfsa[0].AddRule("E", " ", "E1");
                        ndfsa[0].AddRule("E1", "t", "G");
                        ndfsa[0].AddRule("E1", " ", "E2");
                        ndfsa[0].AddRule("E2", "t", "G");
                        ndfsa[0].AddRule("E2", " ", "E3");
                        ndfsa[0].AddRule("E3", "t", "G");
                        ndfsa[0].AddRule("G", "i", "H");
                        ndfsa[0].AddRule("H", "p", "I");
                        ndfsa[0].AddRule("I", "s", "K");
                        ndfsa[0].AddRule("K", "&", "qf");

                        //second automate

                        ndfsa[1].AddRule("S0", "1", "A");
                        ndfsa[1].AddRule("A", "9", "B");
                        ndfsa[1].AddRule("B", "2", "C");
                        ndfsa[1].AddRule("C", ".", "D");
                        ndfsa[1].AddRule("D", "1", "E");
                        ndfsa[1].AddRule("E", "6", "F");
                        ndfsa[1].AddRule("F", "8", "G");
                        ndfsa[1].AddRule("G", ".", "H");
                        ndfsa[1].AddRule("H", "1", "I");
                        ndfsa[1].AddRule("I", ".", "J");
                        string[] numbers_s = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        foreach (string curNumber in numbers_s)
                        {
                            ndfsa[1].AddRule("J", curNumber, "N1");
                            ndfsa[1].AddRule("N1", curNumber, "N2");
                            ndfsa[1].AddRule("N2", curNumber, "qf");
                        }

                        ndfsa[1].AddRule("N1", "ε", "qf");
                        ndfsa[1].AddRule("N2", "ε", "qf");

                        var dkaf = new FSAutomate();
                        dkaf.BuildDeltaDKAutomate(ndfsa[0], false);
                        dkaf.DebugAuto();
                        Console.WriteLine("Enter line to execute first:");
                        dkaf.Execute(Console.ReadLine() + "ε");

                        var dkas = new FSAutomate();
                        dkas.BuildDeltaDKAutomate(ndfsa[1], false);
                        dkas.DebugAuto();
                        Console.WriteLine("Enter line to execute second:");
                        dkas.Execute(Console.ReadLine() + "ε");
                        break;

                    case "3.2":
                        var example = new FSAutomate(new List<Symbol>() { "S0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "qf" },
                                                     new List<Symbol>() { "a", "b" },
                                                     new List<Symbol>() { "qf" },
                                                     "S0");
                        example.AddRule("S0", "", "1");
                        example.AddRule("S0", "", "7");
                        example.AddRule("1", "", "2");
                        example.AddRule("1", "", "4");
                        example.AddRule("2", "a", "3");
                        example.AddRule("4", "b", "5");
                        example.AddRule("3", "", "6");
                        example.AddRule("5", "", "6");
                        example.AddRule("6", "", "1");
                        example.AddRule("6", "", "7");
                        example.AddRule("7", "a", "8");
                        example.AddRule("8", "b", "9");
                        example.AddRule("9", "b", "qf");

                        var dkaEX = new FSAutomate();
                        dkaEX.BuildDeltaDKAutomate(example, false); // false по таблице  
                        dkaEX.DebugAuto();                         // true Алгоритм Томсона по очереди 
                        Console.WriteLine("Enter line to execute :");
                        dkaEX.Execute(Console.ReadLine());
                        break;
                    case "3.3":
                        {
                            var ka8 = new FSAutomate(new List<Symbol>() { "S0", "A", "C", "B", "D", "qf" },
                                            new List<Symbol>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "#", "$", "%" },
                                            new List<Symbol>() { "qf" }, "S0");
                            ka8.AddRule("S0", "[0-8]", "A");
                            ka8.AddRule("A", "[#-%]", "B");
                            ka8.AddRule("B", "[02468]", "C");
                            foreach (char i in "0123456789".ToCharArray())
                                ka8.AddRule("C", i.ToString(), "D");
                            foreach (char i in "0123456789".ToCharArray())
                                ka8.AddRule("D", i.ToString(), "qf");

                            ka8.DebugAuto();
                            Console.WriteLine("Enter line to execute 1#845 :");
                            ka8.Execute(Console.ReadLine());
                            break;
                        }
                    case "4.0":
                        // v2.2022 201- Тихонов 
                        var generator = new Tools.CoreGrammarGenerator();
                        var newGrammarGenerator = new Tools.NewGrammarGenerator();
                        string path40 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "log.txt";
                        Console.WriteLine("Выберите: Сгенерировать варианты КС грамматики и сохранить в  файл(1), и загрузить требуемый вариант из файла(2)");
                        int var40 = Convert.ToInt32(Console.ReadLine());
                        if (var40 == 1)
                        {
                            File.WriteAllText(@path40, string.Empty);
                            Console.WriteLine("Введите количество вариантов в таблице одним числом:");
                            int numbers40 = Convert.ToInt32(Console.ReadLine());
                            numbers40++;
                            Console.WriteLine("Введите seed (целое число > 0)");
                            int seed40 = Convert.ToInt32(Console.ReadLine());
                            newGrammarGenerator.LogWrite(seed40, numbers40);
                            Console.WriteLine("Файл с вариантами был добавлен на ваше устройство");
                        }
                        else if (var40 == 2)
                        {
                            Console.WriteLine("Введите ваш вариант");
                            int variant40 = Convert.ToInt32(Console.ReadLine());
                            var vg40 = newGrammarGenerator.GetTask(variant40, path40);
                            string beb_ra = generator.PrintGrammar(vg40, variant40, "V");
                            Console.WriteLine(beb_ra);
                            //загрузка из файла одного варианта в объект Grammar для приведения
                            var NewRegGr = new Grammar(vg40.T, vg40.V, "S");
                            foreach (Production pr40 in vg40.P)
                            {
                                string LHSp = pr40.LHS.ToString();
                                NewRegGr.AddRule(LHSp, pr40.RHS);
                            }
                            Console.WriteLine(
                                "Что вы хотите сделать? \n" +
                                " 1: Устранить бесполезные символы \n" +
                                " 2: Устранить e-правила \n" +
                                " 3: Устранить цепные правила \n" +
                                " 4: Устранить левую рекурсию \n" +
                                " 5: Привести к нормальной форме Холмского \n" +
                                " 6: Привести к нормальной форме Грейбах \n"
                            );
                            int option = Convert.ToInt32(Console.ReadLine());
                            switch (option)
                            {
                                case 1:
                                    {
                                        var g = NewRegGr.unUsefulDelete();
                                        g.DebugPrules();
                                        break;
                                    }
                                case 2:
                                    {
                                        var g = NewRegGr.EpsDelete();
                                        g.DebugPrules();
                                        break;
                                    }
                                case 3:
                                    {
                                        var g = NewRegGr.ChainRuleDelete();
                                        g.DebugPrules();
                                        break;
                                    }

                                case 4:
                                    {
                                        var g = NewRegGr.LeftRecursDelete_new6();
                                        g.DebugPrules();
                                        break;
                                    }
                                case 5:
                                    {
                                        var g1 = NewRegGr.DeleteLongRules();
                                        g1.Debug("T", g1.T);
                                        g1.Debug("V", g1.V);
                                        g1.DebugPrules();

                                        var g2 = g1.unUsefulDelete();
                                        g2.DebugPrules();

                                        var g3 = g2.EpsDelete();
                                        g3.DebugPrules();

                                        var g4 = g3.DeleteS0Rules();
                                        g4.Debug("T", g4.T);
                                        g4.Debug("V", g4.V);
                                        g4.DebugPrules();
                                        Console.Write("Start symbol: ");
                                        Console.WriteLine(g4.S0 + "\n");

                                        var g5 = g4.ChainRuleDelete();
                                        g5.DebugPrules();

                                        var g6 = g5.DeleteTermRules();

                                        Console.WriteLine("--------------------------------------------");
                                        Console.WriteLine("Нормальная форма Холмского");
                                        g6.Debug("T", g6.T);
                                        g6.Debug("V", g6.V);
                                        g6.DebugPrules();
                                        break;
                                    }
                                case 6:
                                    {
                                        var g1 = NewRegGr.DeleteLongRules();

                                        var g2 = g1.unUsefulDelete();

                                        var g3 = g2.EpsDelete();

                                        var g4 = g3.DeleteS0Rules();

                                        var g5 = g4.ChainRuleDelete();

                                        var g6 = g5.DeleteTermRules();

                                        var g7 = g6.LeftRecursDelete_new6();

                                        var g9 = g7.TransformGrForm();
                                        Console.WriteLine("--------------------------------------------");
                                        Console.WriteLine("Нормальная форма Грейбах");
                                        g9.DebugPrules();
                                        break;
                                    }

                            }
                        }
                        break;

                    /* v1
                    var generator = new Assignment.GrammarGenerator();
                    var grammar = generator.GenerateGrammar();
                    Console.ReadKey();
                    break;
                    */
                    case "4.1":
                        var regGr = new Grammar(new List<Symbol>() { "a", "b", "c", "d", "f", "g", "o", "u" },
                                                new List<Symbol>() { "S", "A", "B", "C", "D", "L", "R", "O", "G", "E", "F", "H" },
                                                "S");

                        regGr.AddRule("S", new List<Symbol>() { "a", "A", "b" });
                        regGr.AddRule("A", new List<Symbol>() { "B", "C", "D" });
                        regGr.AddRule("C", new List<Symbol>() { "d", "f", "B" });
                        regGr.AddRule("D", new List<Symbol>() { "L" });
                        regGr.AddRule("L", new List<Symbol>() { "g" });
                        regGr.AddRule("B", new List<Symbol>() { "c", "D", "R" });
                        regGr.AddRule("R", new List<Symbol>() { "" });
                        regGr.AddRule("O", new List<Symbol>() { "c", "G", "v" }); ;
                        regGr.AddRule("E", new List<Symbol>() { "v", "F", "c" });
                        regGr.AddRule("S", new List<Symbol>() { "u", "H" });
                        regGr.AddRule("H", new List<Symbol>() { "H", "o" });
                        regGr.AddRule("H", new List<Symbol>() { "o" });
                        Console.WriteLine("Grammar:");
                        regGr.Debug("T", regGr.T);
                        regGr.Debug("T", regGr.V);
                        regGr.DebugPrules();

                        Grammar G1 = regGr.EpsDelete();
                        G1.DebugPrules();

                        Grammar G2 = G1.ChainRuleDelete();
                        G2.DebugPrules();

                        Grammar G3 = G2.unUsefulDelete();
                        G3.DebugPrules();

                        Grammar G4 = G3.LeftRecursDelete_new6();
                        G4.DebugPrules();
                        // G4 - приведенная грамматика

                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Normal Grammatic:");
                        G4.Debug("T", G4.T);
                        G4.Debug("V", G4.V);
                        G4.DebugPrules();
                        Console.Write("Start symbol: ");
                        Console.WriteLine(G4.S0 + "\n");
                        break;
                    case "6.1":
                        {
                            var g = new Grammar(new List<Symbol>() { "+", "*", "(", ")", "i" },
                                                  new List<Symbol>() { "S", "F", "L", "K" },
                                                  "S");
                            g.AddRule("S", new List<Symbol>() { "S", "+", "F" });
                            g.AddRule("S", new List<Symbol>() { "F" });
                            g.AddRule("F", new List<Symbol>() { "F", "*", "L" });
                            g.AddRule("F", new List<Symbol>() { "L" });
                            g.AddRule("L", new List<Symbol>() { "(", "S", ")" });
                            g.AddRule("L", new List<Symbol>() { "i" });
                            g.AddRule("K", new List<Symbol>() { "i" });

                            Console.WriteLine("Grammar:");
                            g.Debug("T", g.T);
                            g.Debug("T", g.V);
                            g.DebugPrules();

                            var g1 = g.DeleteLongRules();

                            var g2 = g1.unUsefulDelete();

                            var g3 = g2.EpsDelete();

                            var g4 = g3.DeleteS0Rules();

                            var g5 = g4.ChainRuleDelete();

                            var g6 = g5.DeleteTermRules();
                            g6.DebugPrules();

                            var g7 = g6.LeftRecursDelete_new6();
                            g7.DebugPrules();
                            g7.Debug("V", g7.V);

                            var g9 = g7.TransformGrForm();

                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine("Greibach normal form:");
                            g9.Debug("T", g9.T);
                            g9.Debug("V", g9.V);
                            g9.DebugPrules();
                            Console.Write("Start symbol: ");
                            Console.WriteLine(g9.S0 + "\n");
                            break;
                        }
                    case "6.2":
                        {  // Algorithms приведенная грамматика G            
                            var g = new Grammar(new List<Symbol>() { "+", "*", "(", ")", "i" },
                                                 new List<Symbol>() { "S", "F", "L", "K" },
                                                 "S");
                            g.AddRule("S", new List<Symbol>() { "S", "+", "F" });
                            g.AddRule("S", new List<Symbol>() { "F" });
                            g.AddRule("F", new List<Symbol>() { "F", "*", "L" });
                            g.AddRule("F", new List<Symbol>() { "L" });
                            g.AddRule("L", new List<Symbol>() { "(", "S", ")" });
                            g.AddRule("L", new List<Symbol>() { "i" });
                            g.AddRule("K", new List<Symbol>() { "i" });

                            Console.WriteLine("Grammar:");
                            g.Debug("T", g.T);
                            g.Debug("T", g.V);
                            g.DebugPrules();

                            var g1 = g.DeleteLongRules();
                            g1.Debug("T", g1.T);
                            g1.Debug("V", g1.V);
                            g1.DebugPrules();

                            var g2 = g1.unUsefulDelete();
                            g2.DebugPrules();

                            var g3 = g2.EpsDelete();
                            g3.DebugPrules();

                            var g4 = g3.DeleteS0Rules();
                            g4.Debug("T", g4.T);
                            g4.Debug("V", g4.V);
                            g4.DebugPrules();
                            Console.Write("Start symbol: ");
                            Console.WriteLine(g4.S0 + "\n");

                            var g5 = g4.ChainRuleDelete();
                            g5.DebugPrules();

                            var g6 = g5.DeleteTermRules();

                            Console.WriteLine("--------------------------------------------");
                            Console.WriteLine("Chomsky normal form:");
                            g6.Debug("T", g6.T);
                            g6.Debug("V", g6.V);
                            g6.DebugPrules();
                            Console.Write("Start symbol: ");
                            Console.WriteLine(g6.S0 + "\n");
                            break;
                        }
                    case "7.0":
                        {
                            var CFGrammar = new Grammar(new List<Symbol>() { "a", "b", "c", "d", "f", "g", "u", "o" },
                                            new List<Symbol>() { "S", "S'", "A", "A'", "B", "C", "C'", "D", "H", "H'" },
                                            "S");
                            CFGrammar.AddRule("S", new List<Symbol>() { "a", "S'" });
                            CFGrammar.AddRule("S'", new List<Symbol>() { "A", "b" });
                            //CFGrammar.AddRule("U",new List<Symbol>() { "*" });
                            CFGrammar.AddRule("A", new List<Symbol>() { "B", "A'" });
                            CFGrammar.AddRule("A'", new List<Symbol>() { "C", "D" });
                            CFGrammar.AddRule("C", new List<Symbol>() { "d", "C'" });
                            CFGrammar.AddRule("C'", new List<Symbol>() { "f", "B" });
                            CFGrammar.AddRule("D", new List<Symbol>() { "g" });
                            CFGrammar.AddRule("B", new List<Symbol>() { "c", "D" });
                            CFGrammar.AddRule("S", new List<Symbol>() { "u", "H" });
                            CFGrammar.AddRule("H", new List<Symbol>() { "o", "H'" });
                            CFGrammar.AddRule("H", new List<Symbol>() { "o" });
                            CFGrammar.AddRule("H'", new List<Symbol>() { "o", "H'" });
                            CFGrammar.AddRule("H'", new List<Symbol>() { "o" });
                            CFGrammar.DebugPrules();

                            var pda = new PDA(CFGrammar);
                            pda.Debug();
                            Console.Write("Enter chain:\n");
                            Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());

                            //  pda.execute((Console.ReadLine()).ToString());
                            break;
                        }
                    case "7":
                        { // Algorithm Grammar to PDA не детерменированный 
                            var CFGrammar = new Grammar(new List<Symbol>() { "b", "c" },
                                                        new List<Symbol>() { "S", "A", "B", "D" },
                                                        "S");

                            CFGrammar.AddRule("S", new List<Symbol>() { "b" });
                            CFGrammar.AddRule("S", new List<Symbol>() { "c", "A", "B" });
                            CFGrammar.AddRule("S", new List<Symbol>() { "c", "B" });

                            CFGrammar.AddRule("A", new List<Symbol>() { "b", "D" });
                            CFGrammar.AddRule("A", new List<Symbol>() { "b" });
                            CFGrammar.AddRule("A", new List<Symbol>() { "c", "B", "D" });
                            CFGrammar.AddRule("A", new List<Symbol>() { "c", "B" });

                            CFGrammar.AddRule("D", new List<Symbol>() { "b" });
                            CFGrammar.AddRule("D", new List<Symbol>() { "b", "D" });

                            CFGrammar.AddRule("B", new List<Symbol>() { "b" });
                            CFGrammar.AddRule("B", new List<Symbol>() { "c", "B" });

                            Console.Write("Debug KC-Grammar ");
                            CFGrammar.DebugPrules();

                            var pda = new PDA(CFGrammar);
                            pda.Debug();

                            Console.WriteLine("\nEnter the line   :");
                            Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
                            break;
                        }
                    case "7.1":
                        { // !! Algorithm Grammar to PDA {aabb, aaaabbbb}
                          // see 7.2 PDA  
                            var cfgr = new Grammar(new List<Symbol>() { "a", "b" },
                                                   new List<Symbol>() { "S", "A", "B" },
                                                   "S");

                            cfgr.AddRule("S", new List<Symbol>() { "a", "A", "b" }); // S -> aAb
                            cfgr.AddRule("A", new List<Symbol>() { "a", "B", "b" }); // A -> aBb
                            cfgr.AddRule("B", new List<Symbol>() { "a", "b" }); // B -> ab
                            Console.Write("Debug KC-Grammar ");
                            cfgr.DebugPrules();

                            var pda = new PDA(new List<Symbol>() { "q0", "q1", "q2", "qf" },
                                                                   new List<Symbol>() { "a", "b" },
                                                                   new List<Symbol>() { "z0", "a", "b", "S", "A", "B" },
                                                                   "q0",
                                                                   "S",
                                                                   new List<Symbol>() { "qf" });
                            pda.addDeltaRule("q0", "ε", "S", new List<Symbol>() { "q1" }, new List<Symbol>() { "a", "A", "b" }); //δ(q0,ε,S) = (a,A,b)
                            pda.addDeltaRule("q", "ε", "A", new List<Symbol>() { "q" }, new List<Symbol>() { "a", "B", "b" }); //δ(q,ε,A) = (a,B,b)
                            pda.addDeltaRule("q", "ε", "B", new List<Symbol>() { "q" }, new List<Symbol>() { "a", "b" }); //δ(q,ε,B) = (a,b)
                            pda.addDeltaRule("q", "a", "a", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,a,a) = (ε)
                            pda.addDeltaRule("q", "a", "b", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,a,b) = (ε)
                            pda.addDeltaRule("q", "b", "b", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,b,b) = (ε)
                            pda.addDeltaRule("q", "b", "a", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" }); //δ(q,b,a) = (ε)


                            pda.Debug();

                            Console.WriteLine("\nВведите строку, пример :"); // aaabbb
                            Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
                            break;
                        }
                    case "7.2":
                        { // !! Algorithm Grammar to PDA {aabb, aaaabbbb}
                          // see 7.2 PDA
                          //
                          // example expression  i = i  
                            var cfgr1 = new Grammar(new List<Symbol>() { "i", "=" },
                                                    new List<Symbol>() { "S", "F", "L" },
                                                    "S");

                            cfgr1.AddRule("S", new List<Symbol>() { "F", "=", "L" }); //S -> F=L
                            cfgr1.AddRule("F", new List<Symbol>() { "i" });    //F -> i
                            cfgr1.AddRule("L", new List<Symbol>() { "F" });    //L -> F
                            Console.Write("Debug KC-Grammar ");
                            cfgr1.DebugPrules();
                            var pda = new PDA(new List<Symbol>() { "q0", "q1", "q2", "qf" },
                                                                   new List<Symbol>() { "i", "=" },
                                                                   new List<Symbol>() { "z0", "i" },
                                                                   "q0",
                                                                   "z0",
                                                                   new List<Symbol>() { "qf" });
                            pda.addDeltaRule("q0", "i", "z0", new List<Symbol>() { "q1" }, new List<Symbol>() { "i", "z0" });//δ(q0,i,z0) = (i,z0)
                            pda.addDeltaRule("q", "=", "i", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" });  //δ(q,=,i) = (ε)
                            pda.addDeltaRule("q", "i", "i", new List<Symbol>() { "q" }, new List<Symbol>() { "i", "i" }); //δ(q,i,i) = (i,i)
                            pda.addDeltaRule("q", "ε", "i", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" });   //δ(q,ε,i) = (ε)
                            pda.Debug();
                            Console.WriteLine("\nВведите строку, пример :"); // i=i
                            Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
                            break;
                        }
                    case "7.3":
                        { //МП - автоматы  {ab, aaaabbbb}         
                            var pda = new PDA(new List<Symbol>() { "q0", "q1", "q2", "qf" },
                                               new List<Symbol>() { "a", "b" },
                                               new List<Symbol>() { "z0", "a" },
                                               "q0",
                                               "z0",
                                               new List<Symbol>() { "qf" });
                            pda.addDeltaRule("q0", "a", "z0", new List<Symbol>() { "q1" }, new List<Symbol>() { "a", "z0" }); //δ(q0,a,z0) = (a, z0)
                            pda.addDeltaRule("q", "a", "a", new List<Symbol>() { "q" }, new List<Symbol>() { "a", "a" }); //δ(q,a,a) = (a,a)
                            pda.addDeltaRule("q", "b", "a", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,b,a) = (ε)
                            pda.addDeltaRule("q", "ε", "z0", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" });  //δ(q,ε,z0) = (ε)

                            pda.Debug();
                            // Console.ReadKey();
                            //              Console.WriteLine("\nEnter the line :");
                            //              Console.WriteLine(pda.Execute(Console.ReadLine()).ToString());
                            Console.WriteLine("Execute example: ab");
                            Console.WriteLine(pda.Execute("ab"));
                            //pda.Execute("aaabbb");
                            break;
                        }
                    case "7.4":
                        {// NPDA  automata  (v + v )
                            var npda = new PDA(
                                    new List<Symbol>() { "q", "qf" },
                                    new List<Symbol>() { "v", "+", "*", "(", ")" },
                                    new List<Symbol>() { "v", "+", "*", "(", ")", "S", "F", "L" },
                                    "q0",
                                    "S",
                                    new List<Symbol>() { "qf" });

                            // S -> S + F | F
                            // F -> F * L || L
                            // L -> v || (S)
                            npda.addDeltaRule("q", "v", "v", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,v,v) = (ε)
                            npda.addDeltaRule("q", "+", "+", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,+,+) = (ε)
                            npda.addDeltaRule("q", "*", "*", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,*,*) = (ε)
                            npda.addDeltaRule("q", "(", "(", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,(,() = (ε)
                            npda.addDeltaRule("q", ")", ")", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,),) ) = (ε)

                            npda.addDeltaRule("q", "+", "*", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,+,*) = (ε)
                            npda.addDeltaRule("q", "ε", "*", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" }); //δ(q,ε,*) = (ε)
                            npda.addDeltaRule("q", "(", "v", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,(,v) = (ε)
                            npda.addDeltaRule("q", "v", "*", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,v,*) = (ε)
                            npda.addDeltaRule("q", "+", "v", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,+,v) = (ε)
                            npda.addDeltaRule("q", ")", "v", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,),v) = (ε)

                            npda.addDeltaRule("q", "ε", "S", new List<Symbol>() { "q" }, new List<Symbol>() { "F" }); //δ(q,ε,S) = (F)
                            npda.addDeltaRule("q", "ε", "F", new List<Symbol>() { "q" }, new List<Symbol>() { "F", "*", "L" }); //δ(q,ε,F) = (F,*,L)
                            npda.addDeltaRule("q", "ε", "F", new List<Symbol>() { "q" }, new List<Symbol>() { "L" }); //δ(q,ε,F) = (L)
                            npda.addDeltaRule("q", "ε", "L", new List<Symbol>() { "q" }, new List<Symbol>() { "v" }); //δ(q,ε,L) = (v)
                            npda.addDeltaRule("q", "ε", "L", new List<Symbol>() { "q" }, new List<Symbol>() { "(", "S", ")" }); //δ(q,ε,L) = ((,S,))

                            npda.Debug();
                            Console.WriteLine("\nEnter the line :");
                            // Example: v+v
                            //          v*(v+v)
                            Console.WriteLine(npda.Execute(Console.ReadLine()).ToString());

                            break;
                        }
                    case "7.5":
                        { // i @ i
                          // S -> F@L
                          // F -> i
                          // L -> i
                            var pda = new PDA(new List<Symbol>() { "q0", "q1", "q2", "qf" },
                                               new List<Symbol>() { "i", "@" },
                                               new List<Symbol>() { "z0", "i", },
                                               "q0",
                                               "z0",
                                               new List<Symbol>() { "qf" });
                            pda.addDeltaRule("q0", "i", "z0", new List<Symbol>() { "q1" }, new List<Symbol>() { "i", "z0" }); //δ(q0,i,z0) = (i,z0)
                            pda.addDeltaRule("q", "@", "i", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" }); //δ(q,@,i) = (ε)
                            pda.addDeltaRule("q", "i", "i", new List<Symbol>() { "q" }, new List<Symbol>() { "i", "i" }); //δ(q,i,i) = (i, i)
                            pda.addDeltaRule("q", "ε", "i", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" }); //δ(q,ε,i) = (ε)
                            pda.Debug();
                            Console.WriteLine("Example: i@i\n");
                            Console.WriteLine(pda.Execute("i@i"));
                            break;

                        }
                    case "7.6":  //проверка PDA 
                        var pda1 = new PDA(
                                 new List<Symbol>() { "q0", "q", "qf" },
                                 new List<Symbol>() { "m", "h" },
                                 new List<Symbol>() { "m", "h", "z0", "F", "D" },
                                 "q0",
                                 "z0",
                                 new List<Symbol>() { "qf" });
                        pda1.addDeltaRule("q0", "ε", "z0", new List<Symbol>() { "q" }, new List<Symbol>() { "F", "D" });
                        pda1.addDeltaRule("q", "ε", "D", new List<Symbol>() { "q" }, new List<Symbol>() { "m" });
                        pda1.addDeltaRule("q", "ε", "F", new List<Symbol>() { "q" }, new List<Symbol>() { "h" });
                        pda1.addDeltaRule("q", "h", "h", new List<Symbol>() { "q" }, new List<Symbol>() { "ε" });
                        pda1.addDeltaRule("q", "m", "m", new List<Symbol>() { "qf" }, new List<Symbol>() { "ε" });
                        pda1.Debug();

                        Console.WriteLine("Example: hm\n");
                        Console.WriteLine(pda1.Execute(Console.ReadLine()).ToString());
                        break;

                    case "7.10":
                        { /* Конфигурируемый МП-автомат: недетерминированный МПА */

                            // NOTE: Распознаёт любой набор из 0 и 1
                            CPDA<DeltaQSigmaGamma> cpda = new CPDA<DeltaQSigmaGamma>(
                               new List<Symbol>() { "q", "p", "r" },
                               new List<Symbol>() { "0", "1" },
                               new List<Symbol>() { "0", "1", "z" },
                               "q", "z",
                               new List<Symbol>() { "r" }
                            );

                            cpda.AddRule(("q", "0", "ε", new List<Symbol>() { "q" }, new List<Symbol>() { "0" }));
                            cpda.AddRule(("q", "1", "ε", new List<Symbol>() { "q" }, new List<Symbol>() { "1" }));
                            cpda.AddRule(("q", "ε", "ε", new List<Symbol>() { "p" }, new List<Symbol>() { "ε" }));

                            cpda.AddRule(("p", "0", "0", new List<Symbol>() { "p" }, new List<Symbol>() { "ε" }));
                            cpda.AddRule(("p", "1", "1", new List<Symbol>() { "p" }, new List<Symbol>() { "ε" }));
                            cpda.AddRule(("p", "ε", "z", new List<Symbol>() { "r" }, new List<Symbol>() { "ε" }));

                            cpda.SetUp((Queue<(Symbol, int, List<Symbol>)> configs, (Symbol q, int i, List<Symbol> pdl) config, string chain) => {
                                // NOTE: Accept if the whole chain was read AND the current state is accepting (belongs to F)
                                if (config.i == chain.Length - 1 && cpda.F.Contains(config.q))
                                    return true;

                                foreach (DeltaQSigmaGamma rule in cpda.Delta)
                                {
                                    // NOTE: If current state does not match rule's state, skip
                                    if (rule.LHSQ != config.q)
                                        continue;

                                    // NOTE: If current input symbol does not match rule's symbol AND it is not epsilon-move, skip
                                    if (!((config.i < chain.Length - 1 && chain[config.i].ToString().Equals(rule.LHSS.symbol)) ||
                                          rule.LHSS.IsEpsilon()))
                                    {
                                        continue;
                                    }

                                    // NOTE: Skip rules that make stack empty
                                    if (config.pdl.Count < 1)
                                        continue;

                                    int new_i = config.i;
                                    List<Symbol> new_pdl = new List<Symbol>(config.pdl);

                                    // NOTE: Pop symbol from the stack if LHSZ is not epsilon
                                    if (!rule.LHSZ.IsEpsilon())
                                        new_pdl.RemoveAt(new_pdl.Count - 1);

                                    // NOTE: Push symbols on to the stack if RHSZ is not epsilon
                                    if (!rule.RHSZ.First().IsEpsilon())
                                        new_pdl.AddRange(Enumerable.Reverse(rule.RHSZ));

                                    // NOTE: Move reading head one symbol to the right if the input symbol is not epsilon
                                    if (!rule.LHSS.IsEpsilon())
                                        ++new_i;

                                    // NOTE: Put new configurations in the queue for further execution
                                    configs.Enqueue((rule.RHSQ.First(), new_i, new_pdl));
                                }

                                return false;
                            });

                            string[] tests = {
                                  "0",
                                  "1",
                                  "00",
                                  "0101",
                                  "0110",
                                  "10101",
                                  "0012100",
                                  "011001",
                                  "111111",
                                  "0110110",
                               };

                            cpda.Debug();
                            foreach (string test in tests)
                                Console.WriteLine("{0} {1} recognised by custom PDA", test, cpda.Execute(test) ? "was" : "was NOT");
                        }
                        break;

                    case "7.11":
                        { /* Расширенный МП-автомат (детерминированный) */
                            EDPDA epda = new EDPDA(new List<Symbol>() { "q, r" },
                                                   new List<Symbol>() { "w", "v", "+", "*", "(", ")" },
                                                   new List<Symbol>() { "w", "v", "+", "*", "(", ")", "S0", "E", "F", "L", "⊥" },
                                                   "q", "⊥",
                                                   new List<Symbol>() { "r" });

                            epda.addDeltaRule("q", "w", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "w" });
                            epda.addDeltaRule("q", "v", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "v" });
                            epda.addDeltaRule("q", "+", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "+" });
                            epda.addDeltaRule("q", "*", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "*" });
                            epda.addDeltaRule("q", "(", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "(" });
                            epda.addDeltaRule("q", ")", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { ")" });

                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "(", "E", ")", "*", "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "S0" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "E", "+", "F" }, new List<Symbol>() { "q" }, new List<Symbol>() { "E" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "F" }, new List<Symbol>() { "q" }, new List<Symbol>() { "S0" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "w" }, new List<Symbol>() { "q" }, new List<Symbol>() { "E" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "F", "*", "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "F" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "F" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "v" }, new List<Symbol>() { "q" }, new List<Symbol>() { "L" });
                            epda.addDeltaRule("q", "ε", new List<Symbol>() { "⊥", "S0" }, new List<Symbol>() { "r" }, new List<Symbol>() { "ε" });

                            string[] tests = {
                                  "(w+v)*v",
                                  "v+v",
                                  "v+(w+v)*v",
                                  "v",
                                  "w*w"
                               };

                            epda.Debug();
                            foreach (string test in tests)
                                Console.WriteLine("{0} {1} recognised by EPDA\n", test, epda.Execute(test) ? "was" : "was NOT");
                        }
                        break;

                    case "7.12":
                        { /* Конфигурируемый МП-автомат: недетерминированный РМПА */

                            CPDA<DeltaQSigmaGammaEx> cpda = new CPDA<DeltaQSigmaGammaEx>(
                               new List<Symbol>() { "q, r" },
                               new List<Symbol>() { "a", "+", "*", "(", ")" },
                               new List<Symbol>() { "a", "+", "*", "(", ")", "S", "F", "L", "Z" },
                               "q", "Z",
                               new List<Symbol>() { "r" }
                            );

                            cpda.AddRule(("q", "a", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "a" }));
                            cpda.AddRule(("q", "+", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "+" }));
                            cpda.AddRule(("q", "*", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "*" }));
                            cpda.AddRule(("q", "(", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { "(" }));
                            cpda.AddRule(("q", ")", new List<Symbol>() { "ε" }, new List<Symbol>() { "q" }, new List<Symbol>() { ")" }));

                            cpda.AddRule(("q", "ε", new List<Symbol>() { "S", "+", "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "S" }));
                            cpda.AddRule(("q", "ε", new List<Symbol>() { "F" }, new List<Symbol>() { "q" }, new List<Symbol>() { "S" }));
                            cpda.AddRule(("q", "ε", new List<Symbol>() { "F", "*", "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "F" }));
                            cpda.AddRule(("q", "ε", new List<Symbol>() { "L" }, new List<Symbol>() { "q" }, new List<Symbol>() { "F" }));
                            cpda.AddRule(("q", "ε", new List<Symbol>() { "a" }, new List<Symbol>() { "q" }, new List<Symbol>() { "L" }));
                            cpda.AddRule(("q", "ε", new List<Symbol>() { "(", "S", ")" }, new List<Symbol>() { "q" }, new List<Symbol>() { "L" }));
                            cpda.AddRule(("q", "ε", new List<Symbol>() { "Z", "S" }, new List<Symbol>() { "r" }, new List<Symbol>() { "ε" }));

                            cpda.SetUp((Queue<(Symbol, int, List<Symbol>)> configs, (Symbol q, int i, List<Symbol> pdl) config, string chain) => {
                                if (config.i == chain.Length - 1 && cpda.F.Contains(config.q))
                                    return true;

                                foreach (DeltaQSigmaGammaEx rule in cpda.Delta)
                                {
                                    if (rule.LHSQ != config.q)
                                        continue;

                                    if (!((config.i < chain.Length - 1 && chain[config.i].ToString().Equals(rule.LHSS.symbol)) ||
                                          rule.LHSS.IsEpsilon()))
                                    {
                                        continue;
                                    }

                                    int size = rule.LHSZX.Count;
                                    int index = config.pdl.Count - size;

                                    if (index < 0)
                                        continue;

                                    int new_i = config.i;
                                    List<Symbol> new_pdl = new List<Symbol>(config.pdl);

                                    if (!rule.LHSZX.First().IsEpsilon())
                                    {
                                        for (int i = 0; i < size; ++i)
                                        {
                                            if (new_pdl[index + i] != rule.LHSZX[i])
                                                goto skip;
                                        }
                                        new_pdl.RemoveRange(index, size);
                                    }

                                    if (!rule.RHSZ.First().IsEpsilon())
                                        new_pdl.AddRange(rule.RHSZ);

                                    if (!rule.LHSS.IsEpsilon())
                                        ++new_i;

                                    configs.Enqueue((rule.RHSQ.First(), new_i, new_pdl));

                                skip: continue;
                                }

                                return false;
                            });

                            string[] tests = {
                              "(a*a)",
                              "a+(a+a)",
                              "a+a+a",
                              "(a+)",
                              "a+(a*a)+a+a+(a+a)"
               };

                            cpda.Debug();
                            foreach (string test in tests)
                                Console.WriteLine("{0} {1} recognised by custom PDA", test, cpda.Execute(test) ? "was" : "was NOT");
                        }
                        break;

                    case "7.13": // МП-автомат с альтернативами
                        {
                            var altGramm = new GrammarAlt(new List<Symbol>() { "a", "b", "c", "-", "+", "(", ")" },
                                            new List<Symbol>() { "E", "T", "F", "P", "S", "O" },
                                            "E");

                            altGramm.AddRule("E", new List<Symbol>() { "T", "O", "T" });
                            altGramm.AddRule("O", new List<Symbol>() { "+", "|", "-" });
                            altGramm.AddRule("T", new List<Symbol>() { "F", "|", "(", "E", ")" }); // символ "|" - задание альтернатив
                            altGramm.AddRule("F", new List<Symbol>() { "a", "|", "b", "|", "c" });


                            var altPDA = altGramm.Transform();

                            altPDA.Debug();
                            Console.WriteLine("\nEnter the line :");
                            // Example: a+(b-c)
                            Console.WriteLine(altPDA.Execute(Console.ReadLine()).ToString());
                            Console.ReadLine();
                        }
                        break;


                    case "9.1":
                        { // LL Разбор
                            var LL = new Grammar(new List<Symbol>() { "i", "(", ")", "+", "*" },
                                                 new List<Symbol>() { "S", "F", "L" },
                                                 "S");

                            LL.AddRule("S", new List<Symbol>() { "(", "F", "+", "L", ")" });
                            LL.AddRule("F", new List<Symbol>() { "*", "L" });
                            LL.AddRule("F", new List<Symbol>() { "i" });
                            LL.AddRule("L", new List<Symbol>() { "F" });

                            var parser = new LLParser(LL);
                            Console.WriteLine("Пример вводимых строк: (i+i), (i+*i)");
                            Console.WriteLine("Введите строку: ");
                            string stringChain = Console.ReadLine();

                            var chain = new List<Symbol> { };
                            foreach (var x in stringChain)
                                chain.Add(new Symbol(x.ToString()));
                            if (parser.Parse(chain))
                            {
                                Console.WriteLine("Допуск. Цепочка символов = L(G).");
                                Console.WriteLine(parser.OutputConfigure);
                            }
                            else
                            {
                                Console.WriteLine("Не допуск. Цепочка символов не = L(G).");
                            }
                            break;
                        }
                    case "9.2":
                        { // LL Разбор
                            var LL1 = new Grammar(new List<Symbol>() { "i", "&", "^", "(", ")", "" },
                                                  new List<Symbol>() { "S", "S'", "F" },
                                                  "S");

                            LL1.AddRule("S", new List<Symbol>() { "(", "S'" });
                            LL1.AddRule("S'", new List<Symbol>() { "F", "^", "F", ")" });
                            LL1.AddRule("S'", new List<Symbol>() { "S", ")" });
                            LL1.AddRule("F", new List<Symbol>() { "&", "F" });
                            LL1.AddRule("F", new List<Symbol>() { "i" });

                            var parser1 = new LLParser(LL1);
                            Console.WriteLine("Введите строку: ");
                            string stringChain = Console.ReadLine();

                            var chain = new List<Symbol> { };
                            foreach (var x in stringChain)
                                chain.Add(new Symbol(x.ToString()));
                            if (parser1.Parse1(chain))
                            {
                                Console.WriteLine("Допуск. Цепочка символов = L(G).");
                                Console.WriteLine(parser1.OutputConfigure);
                            }
                            else
                            {
                                Console.WriteLine("Не допуск. Цепочка символов не = L(G).");
                            }
                            break;
                        }
                    case "14":
                        var LR0Grammar = new SLRGrammar(new List<Symbol>() { "i", "j", "&", "^", "(", ")" },
                                                        new List<Symbol>() { "S", "F", "L" },
                                                        new List<Production>(),
                                                        "S");

                        LR0Grammar.AddRule("S", new List<Symbol>() { "F", "^", "L" });
                        LR0Grammar.AddRule("S", new List<Symbol>() { "(", "S", ")" });
                        LR0Grammar.AddRule("F", new List<Symbol>() { "&", "L" });
                        LR0Grammar.AddRule("F", new List<Symbol>() { "i" });
                        LR0Grammar.AddRule("L", new List<Symbol>() { "j" });

                        LR0Grammar.Construct();
                        LR0Grammar.Inference();
                        break;
                    case "16.1":
                        {
                            var parser = new MyLRParser();
                            parser.ReadGrammar();
                            parser.Execute();
                            break;
                        }
                    case "16.2":
                        {
                            var parser = new MyLRParser();
                            Console.WriteLine("Пример ввода продукций:");
                            parser.Example();
                            parser.Execute();
                            break;
                        }
                    case "16.3":
                        {
                            var parser = new MyLRParser();
                            parser.ReadGrammar();
                            parser.Execute_LR1();
                            break;
                        }
                    case "16.4":
                        {
                            var parser = new MyLRParser();
                            Console.WriteLine("Пример ввода продукций:");
                            parser.Example_LR1();
                            parser.Execute_LR1();
                            break;
                        }
                    case "17.1":
                        {
                            // Задаем грамматику
                            var spgGrammar = new Grammar(new List<Symbol>() { "a", "d", "f", "b", "g", "c" },
                                new List<Symbol>() { "S", "A", "C", "R", "B", "D", "K" },
                                "S");

                            spgGrammar.AddRule("S", new List<Symbol>() { "a", "A" });
                            spgGrammar.AddRule("A", new List<Symbol>() { "B", "C", "D", "K" });
                            spgGrammar.AddRule("C", new List<Symbol>() { "d", "R" });
                            spgGrammar.AddRule("R", new List<Symbol>() { "f", "B" });
                            spgGrammar.AddRule("B", new List<Symbol>() { "c", "D" });
                            spgGrammar.AddRule("D", new List<Symbol>() { "g" });
                            spgGrammar.AddRule("K", new List<Symbol>() { "b" });
                            // Пример цепочки acgdfcggb
                            var parser = new SPGParser(spgGrammar);
                            parser.Execute();
                            break;
                        }
                    case "17.2":
                        {
                            SPGParser parser = new SPGParser();
                            // Пример цепочки axmmb
                            parser.Example();
                            break;
                        }
                    case "18.1":
                        {
                            // Задаем грамматику
                            var grammar = new Grammar(new List<Symbol>() { "^", "&", "~", "(", ")", "a" },
                               new List<Symbol>() { "S", "T", "E", "F" },
                               "S"
                           );
                            grammar.AddRule("S", new List<Symbol>() { "S", "^", "T" });
                            grammar.AddRule("S", new List<Symbol>() { "T" });
                            grammar.AddRule("T", new List<Symbol>() { "T", "&", "E" });
                            grammar.AddRule("T", new List<Symbol>() { "E" });
                            grammar.AddRule("E", new List<Symbol>() { "~", "E" });
                            grammar.AddRule("E", new List<Symbol>() { "F" });
                            grammar.AddRule("F", new List<Symbol>() { "(", "E", ")" });
                            grammar.AddRule("F", new List<Symbol>() { "a" });

                            // Пример цепочки a, ~(a)
                            var parser = new OPGParser(grammar);
                            parser.Execute();
                            break;
                        }

                    case "18.2":
                        {
                            OPGParser parser = new OPGParser();
                            // Пример цепочки a-a
                            parser.Example();
                            break;
                        }
                    case "I1": // SDT
                        try
                        {
                            var sdt = new mySDTSchemata(new List<Symbol>() { "S", "A" },
                                      new List<Symbol>() { "0", "1" },
                                      new List<Symbol>() { "a", "b" },
                                      "S");

                            sdt.addRule(new Symbol("S"),
                                    new List<Symbol>() { "0", "A", "S" },
                                    new List<Symbol>() { "S", "A", "a" });
                            sdt.addRule(new Symbol("A"),
                                    new List<Symbol>() { "0", "S", "A" },
                                    new List<Symbol>() { "A", "S", "a" });
                            sdt.addRule(new Symbol("S"),
                                    new List<Symbol>() { "1" },
                                    new List<Symbol>() { "b" });
                            sdt.addRule(new Symbol("A"),
                                    new List<Symbol>() { "1" },
                                    new List<Symbol>() { "b" });

                            Console.Write("\nDebug SDTranslator:");
                            sdt.debugSDTS();

                            sdt.Translate(new List<Symbol>() { "0", "0", "1", "0", "1", "1", "1" });

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\nОшибка: {e.Message}");
                        }

                        // Homomorphism
                        try
                        {
                            var h_table = new myHTable(new List<Symbol>() { "0", "1" },
                                                        new List<Symbol>() { "1", "0" });

                            Console.WriteLine("\nDebug Homomorphism:");
                            h_table.debugHTable();

                            Console.WriteLine("\nInput chain:");
                            var r = new List<Symbol>() { "0", "1", "0", "0", "1", "1" };
                            Console.WriteLine(Utility.convert(r));
                            Console.WriteLine("\nTranslation:");
                            Console.WriteLine(h_table.h(r));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\nОшибка: {e.Message}");
                        }

                        try
                        {
                            var sdt = new mySDTSchemata(new List<Symbol>() { "S" },
                                                        new List<Symbol>() { "+", "i" },
                                                        new List<Symbol>() { "+", "i" },
                                                        "S");

                            sdt.addRule(new Symbol("S"),
                                            new List<Symbol>() { "+", "S_1", "S_2" },
                                            new List<Symbol>() { "S_2", "+", "S_1" });

                            sdt.addRule(new Symbol("S"),
                                            new List<Symbol>() { "i" },
                                            new List<Symbol>() { "i" });

                            Console.Write("\nDebug SDTranslator:");
                            sdt.debugSDTS();

                            sdt.Translate(new List<Symbol>() { "+", "+", "+", "i", "i", "i", "i" });

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\nОшибка: {e.Message}");
                        }
                        break;

                    case "I2":
                        var inputGrammar = new Grammar(new List<Symbol>() { "i", "+", "*", "(", ")" },
                                                       new List<Symbol>() { "E", "T", "P" },
                                                       new List<Production>(),
                                                       "E");

                        inputGrammar.AddRule("E", new List<Symbol> { "T", "+", "E" });
                        inputGrammar.AddRule("E", new List<Symbol> { "T" });
                        inputGrammar.AddRule("T", new List<Symbol> { "P", "*", "T" });
                        inputGrammar.AddRule("T", new List<Symbol> { "P" });
                        inputGrammar.AddRule("P", new List<Symbol> { "i" });
                        inputGrammar.AddRule("P", new List<Symbol> { "(", "E", ")" });

                        var converter = new ConverterInTransGrammar(new List<Symbol>() { "i", "+", "*", "(", ")" },
                                                                    new List<Symbol>() { "E", "T", "P" },
                                                                    new List<Production>(),
                                                                    "E");

                        converter.AddRule("E", new List<Symbol> { "T", "+", "E" });
                        converter.AddRule("E", new List<Symbol> { "T" });
                        converter.AddRule("T", new List<Symbol> { "P", "*", "T" });
                        converter.AddRule("T", new List<Symbol> { "P" });
                        converter.AddRule("P", new List<Symbol> { "i" });
                        converter.AddRule("P", new List<Symbol> { "(", "E", ")" });

                        converter.Construct();
                        var tgrm = new TransGrammar();
                        tgrm = converter.ConvertInTransGrammar(inputGrammar, "i+i*i", "iii*+");

                        break;
                    case "I3":
                        { // ATGrammar(V,T,OP,S,P)
                            var atgr = new ATGrammar(new List<Symbol>() { "P", "E", "T", "S" },
                                                     new List<Symbol>() { "*", "+", "(", ")", "c" },
                                                     new List<Symbol_Operation>(), "S");

                            //правила для грамматики
                            atgr.Addrule("S", new List<Symbol>() { "E" });
                            atgr.Addrule("E", new List<Symbol>() { "E", "+", "T" });
                            atgr.Addrule("E", new List<Symbol>() { "T" });
                            atgr.Addrule("T", new List<Symbol>() { "T", "*", "P" });
                            atgr.Addrule("T", new List<Symbol>() { "P" });
                            atgr.Addrule("P", new List<Symbol>() { "c" });
                            atgr.Addrule("P", new List<Symbol>() { "(", "E", ")" });

                            atgr.NewAT(new List<Symbol>() { "p", "q", "r" }, new List<Symbol>() { "*", "+" }, new List<Symbol>() { "c" });

                            atgr.Print();
                            break;
                        }
                    case "I4":
                        { // ATGrammar(V,T,OP,S,P)
                          // S, Er    *, +, cr     {ANS}r
                            var atgr = new ATGrammar(
                              new List<Symbol>() { "S", new Symbol("E", new List<Symbol>() { "r" }) },
                              new List<Symbol>() { "*", "+", new Symbol("c", new List<Symbol>() { "r" }) },
                              new List<Symbol_Operation>() { new Symbol_Operation("{ANS}", new List<Symbol>() { "r" }) },
                              new Symbol("S"));
                            atgr.Addrule(new Symbol("S"), // LHS        LHS -> RHS  
                                                     new List<Symbol>() { // RHS
                                              new Symbol("E", // S -> Ep {ANS}r r -> p
                                              new List<Symbol>() { "p" }), new Symbol_Operation("{ANS}",new List<Symbol>() { "r" }) },
                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {"r" },new List<Symbol> { "p" })
                                                             }
                                                    );

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }), // Ep -> +EpEr p -> q + r
                                    new List<Symbol>() { "+",new Symbol("E",new List<Symbol>() { "p" }),
                                                                         new Symbol("E",new List<Symbol>() { "r" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "p" },new List<Symbol> { "q", "+", "r" })
                                    });

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }),  // Ep -> *EpEr   p -> q * r
                                    new List<Symbol>() { "*", new Symbol("E", new List<Symbol>() { "p" }), new Symbol("E", new List<Symbol>() { "r" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "p" },new List<Symbol> { "q", "+", "r" })
                                    });

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }), // Ep -> Cr   p -> r
                                 new List<Symbol>() { new Symbol("C", new List<Symbol>() { "r" }) },
                                 new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                 "p" },new List<Symbol> { "r" })
                                 });

                            atgr.Print();

                            atgr.transform();   //преобразование в простую форму присваивания

                            Console.WriteLine("\nPress Enter to show result\n");
                            Console.ReadLine();

                            atgr.Print();
                            Console.WriteLine("\nPress Enter to end\n");
                            Console.ReadLine();
                            break;
                        }
                    case "I5":
                        {
                            /* нужна доработка пример  Минеева 201, 2021  i1 , i2=i3 , i4
                             D, Lr, E, F      i_b, =, <,>, n_e    {type}a,c , {push}k, {pull}t
                              ATGrammar(V,T,OP,S,P) 
                            */
                            var atgr = new ATGrammar(
                               new List<Symbol>() { "D", new Symbol("L", new List<Symbol>() { "r" }), "E", "F" },
                               new List<Symbol>() { "=", ",", new Symbol("i", new List<Symbol>() { "b" }), new Symbol("n", new List<Symbol>() { "e" }) },
                               new List<Symbol_Operation>() {
                    new Symbol_Operation("{type}",new List<Symbol>() { "a", "c" }),
                    new Symbol_Operation("{push}",new List<Symbol>() { "k" }),
                    new Symbol_Operation("{pull}",new List<Symbol>() { "t" }),
                    new Symbol_Operation("{stack}",new List<Symbol>() { "u" }) },
                               new Symbol("D"));
                            atgr.Addrule(new Symbol("D"), // LHS
                                 new List<Symbol>() { // RHS
                        new Symbol("i", // D -> i_b {тип}a,c Lr   a <- b c <- r
                        new List<Symbol>() { "b" }),new Symbol("{type}",new List<Symbol>() { "a", "c"}),
                        new Symbol("L",
                        new List<Symbol>() { "r" })  },

                                      new List<AttrFunction>() {
                          new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
                          new AttrFunction(new List<Symbol>() { "c" },new List<Symbol> { "r" })
                                      }
                                 );

                            atgr.Addrule(new Symbol("L",
                            new List<Symbol>() { new Symbol("r") }), // Lr -> n_e    r <- e
                            new List<Symbol>() {
                        new Symbol("n",
                        new List<Symbol>() { "e" }) },

                            new List<AttrFunction>() {
                        new AttrFunction(new List<Symbol>() { "r" },new List<Symbol> { "e" })
                            });

                            atgr.Addrule(new Symbol("L", new List<Symbol>() { "r" }),
                            new List<Symbol>() {
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" })
                        },

                        new List<AttrFunction>() {
             new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
              new AttrFunction(new List<Symbol>() { "r" },new List<Symbol> { "c" })
                        });

                            atgr.Addrule(new Symbol("L", new List<Symbol>() { "r" }),
                            new List<Symbol>() {
                            ",",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{pull}",
                            new List<Symbol>() { "t" }),
                            new Symbol("{pull}",
                            new List<Symbol>() { "p" }),
                            "E"
                        },


                        new List<AttrFunction>() {
             new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
             new AttrFunction(new List<Symbol>() { "c" },new List<Symbol> { "t" }),
             new AttrFunction(new List<Symbol>() { "r" },new List<Symbol> { "p" })
                        });

                            atgr.Addrule(new Symbol("E"),
                                new List<Symbol>() {
                            ",",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{pull}",
                            new List<Symbol>() { "t" }),
                            "E"
                                },

                                new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
                            new AttrFunction(new List<Symbol>() { "c" },new List<Symbol> { "t" })
                                });

                            atgr.Addrule(new Symbol("E"),
                                new List<Symbol>() {
                            "=",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{push}",
                            new List<Symbol>() { "k" }),
                            "F"
                                        },

                            new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "" }),
                            new AttrFunction(new List<Symbol>() { "k" },new List<Symbol> { "c" })
                            });

                            atgr.Addrule(new Symbol("F"),
                                new List<Symbol>() {
                            ",",
                            new Symbol("i",
                            new List<Symbol>() { "b" }),
                            new Symbol("{type}",
                            new List<Symbol>() { "a", "c" }),
                            new Symbol("{push}",
                            new List<Symbol>() { "k" }),
                            "F"
                                },

                                new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "a" },new List<Symbol> { "b" }),
                            new AttrFunction(new List<Symbol>() { "k" },new List<Symbol> { "c" })
                                });

                            atgr.Addrule(new Symbol("F"),
                                new List<Symbol>() {
                            new Symbol("{stack}",
                            new List<Symbol>() { "u" })
                            },


                            new List<AttrFunction>() {
                            new AttrFunction(new List<Symbol>() { "u" },new List<Symbol> { "1" })
                            });

                            atgr.Print();

                            atgr.transform();

                            Console.WriteLine("\nPress Enter to show result\n");
                            Console.ReadLine();

                            atgr.Print();
                            Console.WriteLine("\nPress Enter to end\n");
                            Console.ReadLine();
                            break;
                            //###### Тимофеев 207 2021  ###### 
                        }
                    case "I6":
                        {
                            var atgr = new ATGrammar(
                              new List<Symbol>() { "A", "B", "C", "D", "E", "F", "S" },
                              new List<Symbol>() { "for", "+", "(", ")", "int", "=", "id", ";", "<", "const2", "const1", "const3", "{", "body", "}" },
                              new List<Symbol_Operation>() { new Symbol_Operation  ("D->{ OUT1:= ' for idn1 in range(const1, '}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("E->{OUT2:= 'const2,'}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("F->{OUT1:= 'const3): '}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("B->{ OUT1:= ' body '}", new List<Symbol>() { "r" }),
                      new Symbol_Operation("C->{ if n3 > n4 then Error}", new List<Symbol>() { "n3", "n4" }) },
                                    new Symbol("S"));

                            atgr.Addrule("S0", new List<Symbol>() { "S", "{ OUT1 := OUT1 || OUT2 }" });
                            atgr.Addrule("S", new List<Symbol>() { "A", "B", "C" });
                            atgr.Addrule("A", new List<Symbol>() { "for", "(", "D", "E", "F" });
                            atgr.Addrule("B", new List<Symbol>() { "body", "{OUT1:= ' body'}" });
                            atgr.Addrule("C", new List<Symbol>() { "}" });
                            atgr.Addrule("D", new List<Symbol>() { "int", "id", "=", "const1", ";", new Symbol("{ OUT1:= ' for id_a in range(const1_n, '}", new List<Symbol>() { "n" }) });
                            atgr.Addrule("E", new List<Symbol>() { "id", "<", "const2", ";", new Symbol("{OUT2:= 'const2_p,'}", new List<Symbol>() { "p" }) });
                            atgr.Addrule("F", new List<Symbol>() { "id", "+", "const3", ")", "{", new Symbol("{OUT2:= 'const3_p): '}", new List<Symbol>() { "p" }) });

                            atgr.ATG_C_Py(new List<Symbol>() { "s", "a", "n", "p", "q", "r" }, new List<Symbol>() { "=", "<", "+" });

                            atgr.Print();

                            Console.WriteLine("\nPress Enter to end\n");
                            Console.ReadLine();
                            break;
                        }
                    //######    ######

                    case "I7":
                        {
                            /* Пример "цепочечного" перевода  11 12 13   
                            Грамматика транслирует выражения из инфиксной записи в постфиксную
                            Выражения состоят из i, +, * и скобок
                            i+i*i без чисел 
                            */
                            var chainPostfix = new SDT.Scheme(new List<SDT.Symbol>() { "i", "+", "*", "(", ")" },
                                                                     new List<SDT.Symbol>() { "E", "E'", "T", "T'", "F" },
                                                                     "E");

                            chainPostfix.AddRule("E", new List<SDT.Symbol>() { "T", "E'" });
                            chainPostfix.AddRule("E'", new List<SDT.Symbol>() { "+", "T", SDT.Actions.Print("+"), "E'" });
                            chainPostfix.AddRule("E'", new List<SDT.Symbol>() { SDT.Symbol.Epsilon });
                            chainPostfix.AddRule("T", new List<SDT.Symbol>() { "F", "T'" });
                            chainPostfix.AddRule("T'", new List<SDT.Symbol>() { "*", "F", SDT.Actions.Print("*"), "T'" });
                            chainPostfix.AddRule("T'", new List<SDT.Symbol>() { SDT.Symbol.Epsilon });
                            chainPostfix.AddRule("F", new List<SDT.Symbol>() { "i", SDT.Actions.Print("i") });
                            chainPostfix.AddRule("F", new List<SDT.Symbol>() { "(", "E", ")" });

                            //SDT.LLTranslator chainTranslator = new SDT(chainPostfix);
                            var chainTranslator = new SDT.LLTranslator(chainPostfix);
                            // Console.WriteLine("Введите строку: ");
                            var inp_str = new SDT.SimpleLexer().Parse(Console.ReadLine());
                            if (chainTranslator.Parse(inp_str))
                            {
                                Console.WriteLine("\nУспех. Строка соответствует грамматике.");
                            }
                            else
                            {
                                Console.WriteLine("\nНе успех. Строка не соответствует грамматике.");
                            }
                            break;
                        }
                    case "I8":
                        {
                            /* L-атрибутивная грамматика
                               Грамматика вычисляет результат арифметического выражения
                               Выражения состоят из целых положительных чисел, +, * и скобок
                               1+2*3 без чисел
                            */

                            SDT.Types.Attrs sAttrs = new() { ["value"] = 0 };
                            SDT.Types.Attrs lAttrs = new() { ["inh"] = 0, ["syn"] = 0 };
                            var lAttrSDT = new SDT.Scheme(new List<SDT.Symbol>() { new SDT.Symbol("number", sAttrs), "+", "*", "(", ")" },
                                                                  new List<SDT.Symbol>() { "S", new SDT.Symbol("E", sAttrs), new SDT.Symbol("E'", lAttrs),
                                                                                       new SDT.Symbol("T", sAttrs), new SDT.Symbol("T'", lAttrs), new SDT.Symbol("F", sAttrs) },
                                                                  "S");

                            lAttrSDT.AddRule("S", new List<SDT.Symbol>() { "E", new SDT.Types.Actions((S) => Console.Write(S["E"]["value"].ToString())) });

                            lAttrSDT.AddRule("E", new List<SDT.Symbol>() { "T", new SDT.Types.Actions((S) => S["E'"]["inh"] = S["T"]["value"]), "E'", new SDT.Types.Actions((S) => S["E"]["value"] = S["E'"]["syn"]) });

                            lAttrSDT.AddRule("E'", new List<SDT.Symbol>() { "+", "T", new SDT.Types.Actions((S) => S["E'1"]["inh"] = (int)S["E'"]["inh"] + (int)S["T"]["value"]), "E'", new SDT.Types.Actions((S) => S["E'"]["syn"] = S["E'1"]["syn"]) });

                            lAttrSDT.AddRule("E'", new List<SDT.Symbol>() { SDT.Symbol.Epsilon, new SDT.Types.Actions((S) => S["E'"]["syn"] = S["E'"]["inh"]) });

                            lAttrSDT.AddRule("T", new List<SDT.Symbol>() { "F", new SDT.Types.Actions((S) => S["T'"]["inh"] = S["F"]["value"]), "T'", new SDT.Types.Actions((S) => S["T"]["value"] = S["T'"]["syn"]) });

                            lAttrSDT.AddRule("T'", new List<SDT.Symbol>() { "*", "F", new SDT.Types.Actions((S) => S["T'1"]["inh"] = (int)S["T'"]["inh"] * (int)S["F"]["value"]), "T'", new SDT.Types.Actions((S) => S["T'"]["syn"] = S["T'1"]["syn"]) });

                            lAttrSDT.AddRule("T'", new List<SDT.Symbol>() { SDT.Symbol.Epsilon, new SDT.Types.Actions((S) => S["T'"]["syn"] = S["T'"]["inh"]) });

                            lAttrSDT.AddRule("F", new List<SDT.Symbol>() { "number", new SDT.Types.Actions((S) => S["F"]["value"] = S["number"]["value"]) });

                            lAttrSDT.AddRule("F", new List<SDT.Symbol>() { "(", "E", ")", new SDT.Types.Actions((S) => S["F"]["value"] = S["E"]["value"]) });

                            SDT.LLTranslator lAttrTranslator = new(lAttrSDT);
                            if (lAttrTranslator.Parse(new SDT.ArithmLexer().Parse(Console.ReadLine())))
                            {
                                Console.WriteLine("\nУспех. Строка соответствует грамматике.");
                            }
                            else
                            {
                                Console.WriteLine("\nНе успех. Строка не соответствует грамматике.");
                            }

                            break;
                        }

                    case "I9":
                        {      // Капалин Д.С.
                            /* Дерево разбора  1*3 только для умножения 
                               Грамматика вычисляет арифметические выражения состоящие из произведений целых положительных числе
                               Дерево разбора печатается на экран, конвертируется в .dot файл и выполняется
                            */

                            var atgr = new ATGrammar(
                              new List<Symbol>() { "S", new Symbol("E", new List<Symbol>() { "r" }) },
                              new List<Symbol>() { "*", new Symbol("c", new List<Symbol>() { "r" }) },
                              new List<Symbol_Operation>() { new Symbol_Operation("{ANS}") },   //new List<Symbol>() { "r" }
                              new Symbol("S"));

                            atgr.Addrule(new Symbol("S"), // LHS        LHS -> RHS  
                                                     new List<Symbol>() { // RHS
                                              new Symbol("E", // S -> Ep {ANS}r r -> p
                                              new List<Symbol>() { "p" }), new Symbol_Operation("{ANS}") },  //new List<Symbol>() { "r" }
                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {"r" },new List<Symbol> { "p" })
                                                             }
                                                    );

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }),  // Ep -> *EqEr   p -> q * r
                                    new List<Symbol>() { new Symbol("E", new List<Symbol>() { "q" }), new Symbol("E", new List<Symbol>() { "r" }), "*" },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "p" },new List<Symbol> { "q", "*", "r" })
                                    });

                            atgr.Addrule(new Symbol("E", new List<Symbol>() { "p" }), // Ep -> Cr   p -> r
                                 new List<Symbol>() { new Symbol("c", new List<Symbol>() { "r" }) },  //, new List<Symbol>() { "r" }
                                 new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                 "p" },new List<Symbol> { "r" })
                                 });

                            atgr.Print();
                            Console.WriteLine("Пример вывода цепочки грамматики:");
                            Console.WriteLine("S ⇒   E{ANS} ⇒ E * E {ADD} ⇒  E*E*E{ANS} ⇒  c3 * E*E{ANS}⇒  c3*c4*E{ANS} ⇒   c3*c4*c5{ANS} ");
                            Console.WriteLine("Для построения дерева разбора необходимо будет ввести цепочку вывода в виде последовательности правил");
                            Console.WriteLine("Например: 1 2 2 3 3 3");
                            var a = new ATScheme(atgr, atgr);
                            a.BuildInputTree();
                            a.PrintTree();



                            /* 
                            SDT.Types.Attrs sAttrs2 = new() { ["value"]=0 };
                            SDT.Types.Attrs lAttrs2 = new() { ["inh"]=0,["syn"]=0 };
                            SDT.Scheme treeGrammar = new SDT.Scheme(new List<SDT.Symbol>() { new SDT.Symbol("number",sAttrs2),"*" },
                                                                    new List<SDT.Symbol>() { "S",new SDT.Symbol("T",sAttrs2),new SDT.Symbol("T'",lAttrs2),new SDT.Symbol("F",sAttrs2) },
                                                                    "S");

                            SDT.OperationSymbol op1 = new(new SDT.Types.Actions((S) => Console.Write(S["T"]["value"].ToString())),"print(T.value)");
                            SDT.OperationSymbol op2 = new(new SDT.Types.Actions((S) => S["T'"]["inh"]=S["F"]["value"]),"T'.inh = F.value",new() { "T'.inh" },new() { "F.value" });
                            SDT.OperationSymbol op3 = new(new SDT.Types.Actions((S) => S["T"]["value"]=S["T'"]["syn"]),"T.value = T'.syn",new() { "T.value" },new() { "T'.syn" });
                            SDT.OperationSymbol op4 = new(new SDT.Types.Actions((S) => S["T'1"]["inh"]=(int)S["T'"]["inh"]*(int)S["F"]["value"]),"T'1.inh = T'.inh * F.value",new() { "T'1.inh" },new() { "T'.inh","F.value" });
                            SDT.OperationSymbol op5 = new(new SDT.Types.Actions((S) => S["T'"]["syn"]=S["T'1"]["syn"]),"T'.syn = T'1.syn",new() { "T'.syn" },new() { "T'1.syn" });
                            SDT.OperationSymbol op6 = new(new SDT.Types.Actions((S) => S["T'"]["syn"]=S["T'"]["inh"]),"T'.syn = T'.inh",new() { "T'.syn" },new() { "T'.inh" });
                            SDT.OperationSymbol op7 = new(new SDT.Types.Actions((S) => S["F"]["value"]=S["number"]["value"]),"F.value = number.value",new() { "F.value" },new() { "number.value" });

                            treeGrammar.AddRule("S",new List<SDT.Symbol>() { "T",op1 });
                            treeGrammar.AddRule("T",new List<SDT.Symbol>() { "F",op2,"T'",op3 });
                            treeGrammar.AddRule("T'",new List<SDT.Symbol>() { "*","F",op4,"T'",op5 });
                            treeGrammar.AddRule("T'",new List<SDT.Symbol>() { SDT.Symbol.Epsilon,op6 });
                            treeGrammar.AddRule("F",new List<SDT.Symbol>() { "number",op7 });

                            SDT.ParseTreeTranslator treeTr = new(treeGrammar);
                            SDT.ParseTree root = treeTr.Parse(new SDT.ArithmLexer().Parse(Console.ReadLine()));
                            if (root!=null) {
                              root.Print();
                              root.Execute();
                              // утилиты для прорисовки дерева в файл  
                              root.PrintToFile("../../../../parse_tree.dot",true);
                              root.PrintToFile("../../../../parse_tree2.dot",false);
                            } else {
                              Console.WriteLine("Строка не соответствует грамматике");
                            }
                            */
                            break;
                        }


                    case "I10":
                        { /* Барсов А.В. , Савин А.А. , М8О-307Б-19
                         * «Перевод конечных автоматов и грамматик в код на С# на основе СУ-схемы» */
                            /* В общем виде задаётся правило вида: S →   #→$   .addRule(#, new List<Symbol> { $ });
                               - это общее правило для порождения правил грамматики.
                               Из него достаём нужные элементы и записываем их следующим образом 
                               Реализация для работы с файлами не включена 
                            */

                            string[] ms = "@ # $ %".Split(' ');                             // мета символы для подстановки
                            string grammarPattern = "# → $";                                // левое правило для порождения правил грамматики 
                            string grammarRules = ".addRule(#, new List<Symbol> { $ });";   // правило грамматики для кода(правая)
                            List<string> r = new List<string>()                             // правила для перевода
                {   "Å → Å1 + B",
                    "Å → T",
                    "Å → T * P",
                    "T → T * P",
                    "T → P",
                    "P → ( E )",
                    "P → i"
                };
                            Console.WriteLine("Заданная грамматика: S →    " + grammarPattern + " ,   " + grammarRules);
                            SDTBuilder sdtBuilder = new SDTBuilder(ms, grammarPattern, grammarRules, r);
                            sdtBuilder.BuildTable();

                            break;
                        }
                    case "I11":
                        {
                            var setlFileContent = @"
                          # G(T,V,P,S)

                          # Grammar 1
                          T = {c, d, f, t, g}
                          V = {A, B, C, D, F, G, T}
                          S = A
                          P = {A -> B, B -> CD, C -> c, D -> d, D -> FG, F -> f, G -> g, G -> T, T -> t}
                          C = { a ∈ T* | S => a }

                          # Grammar 2
                          T = {c, d, f, t, g}
                          V = {A, B, C, D, F, G, T}
                          S = A
                          P = {A -> B, B -> CD, C -> c, D -> d}
                          L = { a ∈ T* | S => a }

                          # Final chains intersection
                          M = C ∩ L
                        ";
                            Console.WriteLine("\nEXECUTING SETL:");
                            var setl = new SETL(setlFileContent);
                            foreach (var variable in setl.Variables)
                            {
                                Console.WriteLine($"{variable.Value.Name} = {variable.Value.Data}");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "I12":
                        {
                            // Алгоритм построения дерева разбора цепочки по транслирующей грамматике
                            // Устинов Д.А. М8О-206Б-21 2023
                            var OpG = new GrammarTranslation(
                              new List<Symbol>() { "+", "i", "*", "ε" },
                              new List<Symbol>() { "E", "P", "T", "E`", "T`" },
                              new List<Symbol_Operation> { new Symbol_Operation("+"), new Symbol_Operation("*"), new Symbol_Operation("i") },
                              "E"
                            );
                            OpG.AddRule("E", new List<Symbol>() { new Symbol("T"), new Symbol("E`") });
                            OpG.AddRule("T", new List<Symbol>() { new Symbol("P"), new Symbol("E`") });
                            OpG.AddRule("E`", new List<Symbol>() { new Symbol("+"), new Symbol("P"), new Symbol_Operation("+"), new Symbol("T`") });
                            OpG.AddRule("T`", new List<Symbol>() { new Symbol("ε") });
                            OpG.AddRule("E`", new List<Symbol>() { new Symbol("ε") });
                            OpG.AddRule("T`", new List<Symbol>() { new Symbol("*"), new Symbol("P"), new Symbol_Operation("*"), new Symbol("T`") });
                            OpG.AddRule("P", new List<Symbol>() { new Symbol("i"), new Symbol_Operation("i") });
                            var treeByTG = new TGBuildTableTree(OpG);
                            Console.WriteLine("Введите строку, по которой будет построено дерево разбора:");
                            string stringInput = Console.ReadLine();
                            var input = new List<Symbol> { };
                            foreach (var x in stringInput)
                            {
                                input.Add(new Symbol(x.ToString()));
                            }
                            treeByTG.BuildAndReturnTree(input);
                            break;
                        }
                    case "I18":
                        {
                            var Gram1 = new Grammar(new List<Symbol>() { "0", "1" },
                            new List<Symbol>() { "S", "A" },
                            "S");
                            Gram1.AddRule("S", new List<Symbol>() { "0", "A", "S" });
                            Gram1.AddRule("A", new List<Symbol>() { "0", "S", "A" });
                            Gram1.AddRule("S", new List<Symbol>() { "1" });
                            Gram1.AddRule("A", new List<Symbol>() { "1" });

                            var Gram2 = new Grammar(new List<Symbol>() { "a", "b" },
                            new List<Symbol>() { "S", "A" },
                            "S");
                            Gram2.AddRule("S", new List<Symbol>() { "S", "A", "a" });
                            Gram2.AddRule("A", new List<Symbol>() { "A", "S", "a" });
                            Gram2.AddRule("S", new List<Symbol>() { "b" });
                            Gram2.AddRule("A", new List<Symbol>() { "b" });

                            var Scheme = new Scheme(Gram1, Gram2);
                            Scheme.Print();

                            Scheme.InputRules();
                            Scheme.BuildInputTree();
                            Scheme.PrintTreeStack(Scheme.LeftInfrence1dTreeVertices);
                            Scheme.PrintTree(Scheme.LeftInfrence1dTreeVertices);

                            Scheme.Transform();
                            Console.WriteLine();
                            Scheme.PrintTreeStack(Scheme.RightInfrence1dTreeVertices);
                            Scheme.PrintTree(Scheme.RightInfrence1dTreeVertices);
                            break;
                        }
                    case "I20":
                        {
                            var atgr = new ATGrammar(
                                new List<Symbol>() { "S", "E", new Symbol("R", new List<Symbol>() { "p" }) },
                                new List<Symbol>() { new Symbol("i", new List<Symbol>() { "p" }), "=", "+", "*", "eps" },
                                new List<Symbol_Operation>() { new Symbol_Operation("{=}", new List<Symbol>() { "p" }), new Symbol_Operation("{+}", new List<Symbol>() { "p", "q", "r" }), new Symbol_Operation("{*}", new List<Symbol>() { "p", "q", "r" }) },
                                new Symbol("S"));
                            atgr.Addrule(new Symbol("S"),  // S->i.p1=E{=}.p2 p2<-p1
                                                        new List<Symbol>() {
                                        new Symbol("i",
                                        new List<Symbol>() { "p1" }),"=","E", new Symbol_Operation("{=}",new List<Symbol>() { "p2" }) },
                            new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {"p2" },new List<Symbol> { "p1" })
                                                                }
                                                    );

                            atgr.Addrule(new Symbol("E"), // E -> +i.p1 R.p2  p2<-p1 
                                    new List<Symbol>() { new Symbol("i", new List<Symbol>() { "p1" }), new Symbol("R", new List<Symbol>() { "p2" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                "p2" },new List<Symbol> { "p1"})
                                    });

                            atgr.Addrule(new Symbol("R", new List<Symbol>() { "p1" }),   //R.p1 -> +i.q1 {+}.p2q2r1 R.r2  r1,r2<-COUNTER, p2<-p1, q2-<q1
                                    new List<Symbol>() { "+", new Symbol("i", new List<Symbol>() { "q1" }), new Symbol_Operation("{+}", new List<Symbol>() { "p2", "q2", "r1" }), new Symbol("R", new List<Symbol>() { "r2" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                "p2" },new List<Symbol> { "p1"}), new AttrFunction(new List<Symbol>() {
                                "q2" },new List<Symbol> { "q1"}), new AttrFunction(new List<Symbol>() {
                                "r1" },new List<Symbol> { "COUNTER"}), new AttrFunction(new List<Symbol>() {
                                "r2" },new List<Symbol> { "COUNTER"})}
                                    );

                            atgr.Addrule(new Symbol("R", new List<Symbol>() { "p1" }),  //R.p1 -> *i.q1 {*}.p2q2r1 R.r2  r1,r2<-COUNTER, p2<-p1, q2-<q1
                                    new List<Symbol>() { "*", new Symbol("i", new List<Symbol>() { "q1" }), new Symbol_Operation("{*}", new List<Symbol>() { "p2", "q2", "r1" }), new Symbol("R", new List<Symbol>() { "r2" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                "p2" },new List<Symbol> { "p1"}), new AttrFunction(new List<Symbol>() {
                                "q2" },new List<Symbol> { "q1"}), new AttrFunction(new List<Symbol>() {
                                "r1" },new List<Symbol> { "COUNTER"}), new AttrFunction(new List<Symbol>() {
                                "r2" },new List<Symbol> { "COUNTER"})}
                                    );

                            atgr.Addrule(new Symbol("R", new List<Symbol>() { "p1" }), // R.p1 -> eps
                                    new List<Symbol>() { "eps" },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                            "p1" },new List<Symbol> { "0" })
                                    });

                            atgr.Print();
                            atgr.LLParser(atgr);
                            Console.WriteLine();
                            Console.WriteLine("Введите входную цепочку (например i.7=i.1+i.2*i.3):");

                            break;
                        }

                    case "I21":
                { 
                        //Алгоритм Преобразования аттрибутной грамматики в дерево зависимостей в виде упорядоченного множества
                        //Меркулов М8О-207Б-21 2023

                        //Пример для [1, 4, 2, 4, 3]

                        List<Symbol> myV = new List<Symbol>()
                        {
                            new Symbol("T", new List<Symbol>(){ new Symbol("val") }),
                            new Symbol("F", new List<Symbol>(){ new Symbol("val") }),
                            new Symbol("T'", new List<Symbol>(){ new Symbol("syn"), new Symbol("inh") }),
                            new Symbol("D", new List<Symbol>(){ new Symbol("syn"), new Symbol("inh") })
                        }; //Множество нетерминальных символов
                        List<Symbol> T = new List<Symbol>()
                        {
                            new Symbol("*"),
                            new Symbol("digit", new List<Symbol>(){ new Symbol("lexval")})
                        }; //Множество терминальных символов
                        List<List<Production>> R = new List<List<Production>>(); //Множество правил
                        Symbol S0 = new Symbol("T"); //Начальный символ

                        //Пример для [1, 2, 3]

                        /*List<Symbol> myV = new List<Symbol>()
                        {
                            new Symbol("F", new List<Symbol>(){ new Symbol("val") }),
                            new Symbol("A", new List<Symbol>(){ new Symbol("val") }),
                            new Symbol("B", new List<Symbol>(){ new Symbol("val") }),
                        }; //Множество нетерминальных символов
                        List<Symbol> T = new List<Symbol>() 
                        {
                            new Symbol("+"),
                            new Symbol("digit", new List<Symbol>(){ new Symbol("lexval")})
                        }; //Множество терминальных символов
                        List<List<Production>> R = new List<List<Production>>(); //Множество правил
                        Symbol S0 = new Symbol("F"); //Начальный символ*/

                        AttributeGrammar grammar = new AttributeGrammar(myV, T, R, S0);

                        //Для [1, 4, 2, 4, 3]
                        grammar.AddRule(
                            new Production(new Symbol("T"), new List<Symbol>() { new Symbol("F"), new Symbol("T'") }), // T -> FT'
                            new List<Production>()
                            {
                                new Production(new Symbol("T'.inh"), new List<Symbol>() { new Symbol("F.val") }), // T'.inh <- F.val
                                new Production(new Symbol("T.val"), new List<Symbol>() { new Symbol("T'.syn") }) // T.val <- T'.syn
                            }
                        );
                        grammar.AddRule(
                            new Production(new Symbol("T'"), new List<Symbol>() { new Symbol("*"), new Symbol("F"), new Symbol("D")}), // T' -> *FD
                            new List<Production>()
                            {
                                new Production(new Symbol("D.inh"), new List<Symbol>() { new Symbol("T'.inh"), new Symbol("*"), new Symbol("F.val")}), // D.inh <- T.inh * F.val
                                new Production(new Symbol("T'.syn"), new List<Symbol>() { new Symbol("D.syn")}) // T'.syn <- D.syn 
                            }
                        );
                        grammar.AddRule(
                            new Production(new Symbol("D"), new List<Symbol>(){ new Symbol("ε")}), // D -> ε
                            new List<Production>()
                            {
                                new Production(new Symbol("D.syn"), new List<Symbol>() { new Symbol("D.inh")}) // D.syn <- D.inh
                            }
                        );
                        grammar.AddRule(
                            new Production(new Symbol("F"), new List<Symbol>() { new Symbol("digit")}), // F -> digit
                            new List<Production>()
                            {
                                new Production(new Symbol("F.val"), new List<Symbol>() { new Symbol("digit.lexval")}) // F.val <- digit.lexval
                            }
                        );

                        //Для [1, 2, 3]
                        /*grammar.AddRule(
                            new Production(new Symbol("F"), new List<Symbol>() { new Symbol("A"), new Symbol("+"), new Symbol("B") }), // F -> A+B    
                            new List<Production>()
                            {
                                new Production(new Symbol("F.val"), new List<Symbol>() { new Symbol("A.val"), new Symbol("+"), new Symbol("B.val")}) // F.val <- A.val + B.val
                            }
                        );
                        grammar.AddRule(
                            new Production(new Symbol("A"), new List<Symbol>() { new Symbol("digit") }), // A -> digit
                            new List<Production>()
                            {
                                new Production(new Symbol("A.val"), new List<Symbol>() { new Symbol("digit.lexval")}), // A.val <- digit.lexval
                            }
                        );
                        grammar.AddRule(
                            new Production(new Symbol("B"), new List<Symbol>() { new Symbol("digit") }), // B -> digit
                            new List<Production>()
                            {
                                new Production(new Symbol("B.val"), new List<Symbol>() { new Symbol("digit.lexval")}), // B.val <- digit.lexval
                            }
                        );*/


                        Console.WriteLine(grammar.Print());
                        Console.WriteLine("Введите входную цепочку правил через пробел, например: 1 4 2 4 3");
                        string input = Console.ReadLine();
                        Console.WriteLine();
                        string[] inputs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        List<int> numRules = new List<int>();
                        for (int i = 0; i < inputs.Length; i++)
                        {
                            numRules.Add(Convert.ToInt32(inputs[i]));
                        }
                        _1dTreeVertices myTree = new _1dTreeVertices(grammar, numRules);
                        //Алгоритм построения дерева по слоям в виде упорядоченного множества
                        myTree.BuildTree();
                        //Алгоритм преобразования атрибутной грамматики в дерево зависимостей вычислений в виде упорядоченного множества
                        myTree.BuildTreeWithDependencies();
                        //Вывод атрибутов с индексами
                        Console.WriteLine(myTree.PrintAttributesWithIndexes());
                        //Вывод атрибутных ссылок
                        Console.WriteLine(myTree.PrintAttrLinks());
                        //Топологическая сортировка
                        myTree.TopologicalSort();
                        //Вывод порядка обхода, полученного при помощи топологической сортировки
                        Console.WriteLine(myTree.PrintOrder());
                        //Упорядоченное множество вершин дерева
                        Console.WriteLine(myTree.Print_1dtreevertices());
                        //Преобразование ссылок между атрибутами в ссылки между вершинами дерева
                        Console.WriteLine(myTree.PrintConvertedLinks());
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                        Console.ReadKey();
                        break;
                    }
                    case "I22":
                        {      // Шукайло Е.А.
                            /* Вычисление значений атрибутов по заданной таблице зависимостей
                            */

                            var atgr = new ATGrammar(
                              new List<Symbol>() { new Symbol("S", new List<Symbol>() { "v" }), new Symbol("A", new List<Symbol>() { "v1" }), new Symbol("B", new List<Symbol>() { "v2" }), new Symbol("C", new List<Symbol>() { "v" }) },
                              new List<Symbol>() { "(", ")", "+", "*", new Symbol("i", new List<Symbol>() { "r" }) },
                              new List<Symbol_Operation>() { },   //new List<Symbol>() { "r" }
                              new Symbol("S", new List<Symbol>() { "v" }));


                            atgr.Addrule(new Symbol("S", new List<Symbol>() { "v" }),  // Sv -> Av1*Bv2   v -> v1 * v2
                                    new List<Symbol>() { new Symbol("A", new List<Symbol>() { "v1" }), "*", new Symbol("B", new List<Symbol>() { "v2" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "v" },new List<Symbol> { "v1", "*", "v2" })
                                    });

                            atgr.Addrule(new Symbol("A", new List<Symbol>() { "p" }), // Ap -> (Cr)   p -> r
                                 new List<Symbol>() { "(", new Symbol("C", new List<Symbol>() { "r" }), ")" },
                                 new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                 "p" },new List<Symbol> { "r" })
                                 });

                            atgr.Addrule(new Symbol("C", new List<Symbol>() { "v" }),  // Cv -> Bv3+Bv4   v -> v3 + v4
                                    new List<Symbol>() { new Symbol("B", new List<Symbol>() { "v3" }), "+", new Symbol("B", new List<Symbol>() { "v4" }) },
                                    new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                     "v" },new List<Symbol> { "v3", "+", "v4" })
                                    });

                            atgr.Addrule(new Symbol("B", new List<Symbol>() { "p" }), // Bp -> ir   p -> r
                                 new List<Symbol>() { new Symbol("i", new List<Symbol>() { "r" }) },
                                 new List<AttrFunction>() { new AttrFunction(new List<Symbol>() {
                                 "p" }, new List<Symbol> { "r" })
                                 });



                            atgr.Print();

                            Console.WriteLine("Пример вывода цепочки грамматики:");
                            Console.WriteLine("S ⇒ A * B ⇒  (C) * B ⇒  (B1 + B2) * B ⇒  (i1 + B2) * B ⇒ (i1 + i2) * B ⇒ (i1 + i2) * i3");

                            Console.WriteLine("Для вычисления значений атрибутов необходимо ввести таблицу терминалов/нетерминалов с их атрибутами");
                            Console.WriteLine("Например: S 0 A 0 B 0 C 0 B 0 B 0 i 1 i 3 i 5");

                            //считываем VERTEXS
                            string input = Console.ReadLine();
                            string[] inputs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                            List<Symbol> VERTEXS = new List<Symbol>();
                            for (int i = 0; i < inputs.Length; i = i + 2)
                            {
                                VERTEXS.Add(new Symbol(inputs[i], new List<Symbol>() { inputs[i + 1] }));
                            }


                            //reverse VERTEXS
                            var t = VERTEXS[0];
                            for (int i = 0; i < VERTEXS.Count() / 2 + 1; i++)
                            {
                                t = VERTEXS[VERTEXS.Count() - i - 1];
                                VERTEXS[VERTEXS.Count() - i - 1] = VERTEXS[i];
                                VERTEXS[i] = t;
                            }

                            //print VERTEXS
                            Console.WriteLine("Таблица VERTEXS:\n");
                            for (int i = 0; i < VERTEXS.Count(); i++)
                            {
                                Console.WriteLine("{0} {1} {2}\n", i, VERTEXS[i].symbol, VERTEXS[i].attr[0]);
                            }


                            Console.WriteLine("Для вычисления значений атрибутов необходимо ввести массив зависимостей терминалов/нетерминалов (пары вида индексов откуда-куда)");
                            Console.WriteLine("Например: 2 4 1 3 0 6 4 5 3 5 5 7 7 8 6 8");

                            //считываем LINKS
                            input = Console.ReadLine();
                            inputs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                            Symbol[,] LINKS = new Symbol[VERTEXS.Count() - 1, 2];
                            for (int i = 0; i < VERTEXS.Count() - 1; i++)
                            {
                                LINKS[i, 0] = VERTEXS[Convert.ToInt32(inputs[2 * i])];
                                LINKS[i, 1] = VERTEXS[Convert.ToInt32(inputs[2 * i + 1])];

                            }

                            //Symbol[] VERTEXS = new Symbol[] { new Symbol("i", new List<Symbol>() { "r3" }), new Symbol("i", new List<Symbol>() { "r2" }), new Symbol("i", new List<Symbol>() { "r1" }), new Symbol("B", new List<Symbol>() { "v4" }), new Symbol("B", new List<Symbol>() { "v3" }), new Symbol("C", new List<Symbol>() { "v" }), new Symbol("B", new List<Symbol>() { "v2" }), new Symbol("A", new List<Symbol>() { "v1" }), new Symbol("S", new List<Symbol>() { "v" }) };
                            //Symbol[,] LINKS = new Symbol[,] { { VERTEXS[2], VERTEXS[4] }, { VERTEXS[1], VERTEXS[3] }, { VERTEXS[0], VERTEXS[6] }, { VERTEXS[4], VERTEXS[5] }, { VERTEXS[3], VERTEXS[5] }, { VERTEXS[5], VERTEXS[7] }, { VERTEXS[7], VERTEXS[8] }, { VERTEXS[6], VERTEXS[8] } };

                            _1dTreeVertices calculating = new _1dTreeVertices(atgr, VERTEXS, LINKS);
                            calculating.Calculating();

                            //print VERTEXS
                            Console.WriteLine("Таблица VERTEXS с вычисленными атрибутами:\n");
                            calculating.PrintVertexs();



                            break;
                        }
                    case "I23":
                        {
                            var T = new Turing(new List<Symbol> { "I", "F", "C1", "C0" },
                                new List<Symbol> { "1", "0", "" },
                                new List<Symbol_Operation>() { new Symbol_Operation("{C}"), new Symbol_Operation("{R}") },
                                new List<Symbol> { "F" },
                                "I"
                            );

                            T.AddRule("I", "1", "{C}", "0", "C1");
                            T.AddRule("I", "0", "{C}", "1", "C0");
                            T.AddRule("C1", "0", "{R}", "", "I");
                            T.AddRule("C0", "1", "{R}", "", "I");
                            T.AddRule("I", "", "", "", "F");
                            T.Execute("1010");
                            break;
                        }
                    case "I24":
                        {
                            // Лохматов Н.И. М8О-206Б-21 2023
                            //Взято из примера I20
                            var OpG = new GrammarTranslation(
                              new List<Symbol>() { "def", "__init__", "__del__", "self", ":" },
                              new List<Symbol>() { "S", "H", "B", "E`", "N", "V", "C", "D", "X" },
                              new List<Symbol_Operation> { new Symbol_Operation(";"), new Symbol_Operation("."),
                              new Symbol_Operation("("), new Symbol_Operation(")"), new Symbol_Operation("="), new Symbol_Operation("x"), new Symbol_Operation("class") },
                              "E"
                            );
                            OpG.AddRule("S", new List<Symbol>() { new Symbol("H"), new Symbol("B"), new Symbol("E") });
                            OpG.AddRule("H", new List<Symbol>() { new Symbol_Operation("class"), new Symbol("N") });
                            OpG.AddRule("B", new List<Symbol>() { new Symbol("V"), new Symbol("C"), new Symbol("D") });
                            OpG.AddRule("V", new List<Symbol>() { new Symbol_Operation("x"), new Symbol_Operation("="), new Symbol("X") });
                            OpG.AddRule("C", new List<Symbol>() { new Symbol_Operation("def"), new Symbol_Operation("__init__"),
                            new Symbol_Operation("("), new Symbol_Operation("self"), new Symbol_Operation(")"), new Symbol(":"),
                            new Symbol_Operation("self"), new Symbol_Operation("."), new Symbol("X"), new Symbol_Operation("=") });
                            OpG.AddRule("D", new List<Symbol>() { new Symbol_Operation("def"), new Symbol_Operation("__del__"),
                            new Symbol_Operation("("), new Symbol_Operation("self"), new Symbol_Operation(")"), new Symbol(":"),
                            new Symbol_Operation("self"), new Symbol_Operation("."), new Symbol("X"), new Symbol_Operation("=") });
                            OpG.AddRule("E", new List<Symbol>() { new Symbol("\\n") });

                            Console.WriteLine("Введите класс на C++:");

                            var input = new List<Symbol> { };

                            while (true)
                            {
                                string stringInput = Console.ReadLine();

                                if (stringInput != "};")
                                {
                                    string word = "";

                                    foreach (var x in stringInput)
                                    {
                                        if (x != ' ' && x != '\n')
                                        {
                                            word += x;
                                        } else
                                        {
                                            input.Add(new Symbol(word));
                                            word = "";
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }

                            Console.WriteLine();

                            Console.WriteLine(input[0] + " " + input[1] + OpG.T[4].ToString());
                            Console.WriteLine(input[3] + " " + input[4] + " 0");
                            Console.WriteLine(OpG.T[0] + " " + OpG.T[1] + OpG.OP[2] + OpG.T[3] + OpG.OP[3] + OpG.T[4]);
                            Console.WriteLine(OpG.T[3].ToString() + OpG.OP[1].ToString() + input[7] + " " + input[8] + " " + input[9]);
                            Console.WriteLine(OpG.T[0] + " " + OpG.T[2] + OpG.OP[2] + OpG.T[3] + OpG.OP[3] + OpG.T[4]);
                            Console.WriteLine(OpG.T[3].ToString() + OpG.OP[1].ToString() + input[12] + " " + input[13] + " " + input[14]);
                            
                            Console.WriteLine();

                            break;
                        }

                    case "I25":
                        // Путилин Дмитрий М8О-207Б-21
                        // Перевод транслирующей грамматики методом рекурсивного спуска.
                        // На вход грамматика и исследуемая цепочка
                        {
                            var Postfix = new SDT.Scheme(new List<SDT.Symbol>() { "i", "+", "*", "(", ")" },
                                                                        new List<SDT.Symbol>() { "S", "E", "E'", "T", "T'", "P" },
                                                                        "S");
                            Postfix.AddRule("S", new List<SDT.Symbol>() { "E" });
                            Postfix.AddRule("E", new List<SDT.Symbol>() { "T", "E'" });
                            Postfix.AddRule("E'", new List<SDT.Symbol>() { "+", "T", SDT.Actions.Print("+"), "E'" });
                            Postfix.AddRule("E'", new List<SDT.Symbol>() { SDT.Symbol.Epsilon });
                            Postfix.AddRule("T", new List<SDT.Symbol>() { "P", "T'" });
                            Postfix.AddRule("T'", new List<SDT.Symbol>() { "*", "P", SDT.Actions.Print("*"), "T'" });

                            Postfix.AddRule("T'", new List<SDT.Symbol>() { SDT.Symbol.Epsilon });
                            Postfix.AddRule("P", new List<SDT.Symbol>() { "(", "E", ")" });
                            Postfix.AddRule("P", new List<SDT.Symbol>() { "i", SDT.Actions.Print("i") });


                            //SDT.LLTranslator chainTranslator = new SDT(Postfix);
                            var chainTranslator = new SDT.LLTranslator(Postfix);
                            Console.WriteLine("Введите цепочку для проверки для проверки типа i+i+i: ");
                            String s = "i+i+i";
                            
                            String a = Console.ReadLine();
                            

                            if (a != "\n") {
                                var inp_str = new SDT.SimpleLexer().Parse(s);
                                if (chainTranslator.ParseReq(inp_str))
                                {
                                    Console.WriteLine("Успех. Строка соответствует грамматике.");
                                }
                                else
                                {
                                    Console.WriteLine("\nНе успех. Строка не соответствует грамматике.");
                                }
                            }
                            else {
                                var inp_str = new SDT.SimpleLexer().Parse(a);
                                if (chainTranslator.ParseReq(inp_str))
                                {
                                    Console.WriteLine("Успех. Строка соответствует грамматике.");
                                }
                                else
                                {
                                    Console.WriteLine("\nНе успех. Строка не соответствует грамматике.");
                                }
                            }

                            


                            break;
                        }



                    

                    default:
                        Console.WriteLine("Выход из программы");
                        return;
                } // end switch
            } // end while
        } // end void Main()

    } // end class Program
}
