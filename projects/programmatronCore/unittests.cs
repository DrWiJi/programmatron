﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpretatorEnvironment;
using programmatronCore;

namespace testing
{
    public class ProgramTester
    {
        List<string[]> argumentsParsingTestStrings;
        List<InterpretatorParameters> argumentsParsingTestStructs;
        public ProgramTester()
        {
            argumentsParsingTestStrings = new List<string[]>
            {
                new string[]{"test.exe","sdgsdgsdgsdgsdg.sdf","-debug"},
                new string[]{"test.exe","sdgsdggsdgsdgsdggagadsagsdgasd","-debug","sdfssdggdsgd","-ignoreWarnings"},
                new string[]{"test.exe","-ignoreWarnings","sdgsdgsdg","-debug","-saveReports"},//по идее должно сработать, путь по умолчанию, аргумент все равно есть
                new string[]{"test.exe","sdgsdg","sdgggsd","der","-saveReports"}
            };
            argumentsParsingTestStructs = new List<InterpretatorParameters>
            {
                new InterpretatorParameters("sdgsdgsdgsdgsdg.sdf",true,false,false,false),
                new InterpretatorParameters("sdgsdggsdgsdgsdggagadsagsdgasd",true,true,false,false),
                new InterpretatorParameters("program.pgt",true,true,true,false),
                new InterpretatorParameters("sdgsdg",false,false,true,false)
            };
        }

        public void startProgramTests()
        {
            Console.WriteLine("Unit Testing starting...");
            testingArgumentsParsing();
            testingLexemAnalyzer();
            testingInterpretator();
        }

        void testingArgumentsParsing()
        {
            Console.WriteLine("Begin argument parsing test...");
            bool success=true;
            for(int i =0;i<argumentsParsingTestStrings.Count;i++)
            {
                string[] args = argumentsParsingTestStrings[i];
                InterpretatorParameters paramsSample = argumentsParsingTestStructs[i];
#if DEBUG
                Program.fillVirtualMachineParameters(args);
#endif
                foreach(string str in args)
                {
                    Console.Write(str);
                    Console.Write(" ");
                }
                Console.Write("\n");
                Console.WriteLine(Parameters.InterParameters.ToString());
                if(!(Parameters.InterParameters==paramsSample))
                {
                    success = false;
                    Console.WriteLine("TEST FAIL");
                }
                else
                {
                    Console.WriteLine("TEST SUCCESS");
                }
            }
            if(success)
            {
                Console.WriteLine("ALL TESTS ARE SUCCESSFUL");
            }
            else
            {
                Console.WriteLine("SOME TEST ARE FAIL");
            }
        }

        void testingLexemAnalyzer()
        {
            //TO DO реализовать юнит тесты для этого участка
        }

        void testingInterpretator()
        {
            LexemAnalyzer sa = new LexemAnalyzer("универсальная а=40;универсальная б=-60;универсальная в=а+б;универсальная сальная = \"1 строка\";универсальная несальная = \"2 строка\";универсальная склейкаПервая = сальная + несальная;сальная = \"\";универсальная склейкаВторая = сальная + несальная;");
            List<Lexem> list = sa.analize();
            SyntaxTreeGenerator gen = new SyntaxTreeGenerator(list);
            gen.generateTree();
            gen.doCode();
        }
    }
}