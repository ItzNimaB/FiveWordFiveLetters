using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordFiveLetters
{
    internal class Algorithm
    {
        public static List<string> Sort(List<string> words)
        {
            List<string> shadowList = new(words);
            List<string> successfulWords = new();

            List<string> exampleWords = new() { "ampyx", "bejig", "fconv", "hdqrs" };

            foreach (string exWord in exampleWords)
            {
                successfulWords.Add(exWord);
                foreach (string word in shadowList)
                {
                    if (word.ToCharArray().Any(c => exWord.ToCharArray().Any(c2 => c2 == c)))
                    {
                        words.Remove(word);
                    }

                    // if (words.Count == 0) Console.WriteLine("No words left");
                }
                // if (words.Count >= 1) Console.WriteLine("Found combi");
            }

            if (words.Count > 0) { successfulWords.Add(words[0]); }
            // successfulWords.ForEach(Console.WriteLine);

            // Thread thr1 = new Thread(method1);
            // Thread thr2 = new Thread(method2);
            // thr1.Start();
            // thr2.Start();

            return successfulWords;
        }
    }
}
