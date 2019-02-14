using System;
using System.Diagnostics;
using System.Text;
using Wintellect.PowerCollections;

namespace BigList
{
    class BigList
    {
        private static BigList<string> rope;
        private static StringBuilder sb;
        private static Stopwatch timer;
        private static int iterations;
        private static string word;

        static void Main(string[] args)
        {
            iterations = 30000;
            word = "string";
            AppendPerformanceTest();
            InsertInTheMiddlePerformanceTets();
            PrependPerformanceTest();
        }

        private static void PrependPerformanceTest()
        {
            Console.WriteLine("Insert string in the begining test!");
            rope = new BigList<string>();
            sb = new StringBuilder();
            timer = new Stopwatch();

            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                rope.Insert(0, word);
            }
            PrintTime();

            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                sb.Insert(0, word);
            }
            PrintTime();
        }

        private static void InsertInTheMiddlePerformanceTets()
        {
            Console.WriteLine("Insert in the middle test!");
            rope = new BigList<string>();
            sb = new StringBuilder();
            timer = new Stopwatch();

            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                rope.Insert(rope.Count / 2, word);
            }
            PrintTime();

            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                sb.Insert(sb.Length / 2, word);
            }
            PrintTime();
        }

        private static void AppendPerformanceTest()
        {
            Console.WriteLine("Append string test!");
            rope = new BigList<string>();
            sb = new StringBuilder();
            timer = new Stopwatch();

            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                rope.Add(word);
            }
            PrintTime();

            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                sb.Append(word);
            }
            PrintTime();
        }

        private static void PrintTime()
        {
            Console.WriteLine($"Time used {timer.ElapsedMilliseconds} ms.");
            timer.Reset();
        }
    }
}
