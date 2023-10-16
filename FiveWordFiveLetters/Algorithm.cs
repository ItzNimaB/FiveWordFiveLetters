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

            string exampleWord = "ampyx";

            foreach (string word in shadowList)
            {
                if (word.ToCharArray().Any(c => exampleWord.ToCharArray().Any(c2 => c2 == c)))
                {
                    words.Remove(word);
                }
                
                // if (words.Count == 0) words = new(shadowList);
            }

            exampleWord = "bejig";

            foreach (string word in shadowList)
            {
                if (word.ToCharArray().Any(c => exampleWord.ToCharArray().Any(c2 => c2 == c)))
                {
                    words.Remove(word);
                }

                // if (words.Count == 0) words = new(shadowList);
            }

            exampleWord = "fconv";

            foreach (string word in shadowList)
            {
                if (word.ToCharArray().Any(c => exampleWord.ToCharArray().Any(c2 => c2 == c)))
                {
                    words.Remove(word);
                }

                // if (words.Count == 0) words = new(shadowList);
            }

            exampleWord = "hdqrs";

            foreach (string word in shadowList)
            {
                // if (word.ToCharArray().Any(c => c == 'a' || c == 'm' || c == 'p' || c == 'y' || c == 'x'))
                if (word.ToCharArray().Any(c => exampleWord.ToCharArray().Any(c2 => c2 == c)))
                {
                    words.Remove(word);
                }

                // if (words.Count == 0) words = new(shadowList);
            }

            if (words.Count >= 1) Console.WriteLine("Fundet!");

            // Thread thr1 = new Thread(method1);
            // Thread thr2 = new Thread(method2);
            // thr1.Start();
            // thr2.Start();

            return words;
        }
    }
}
