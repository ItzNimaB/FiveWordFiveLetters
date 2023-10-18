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
        public static Dictionary<string, int> Sort(Dictionary<string, int> words)
        {
            Dictionary<string, int> shadowList = new(words);
            List<List<int>> successCombs = new();
            int successWordsCount = 0;

            foreach (var (word1, bit1) in shadowList)
            {
                foreach (var (word2, bit2) in shadowList)
                {
                    if (word2.Intersect(word1).Any()) continue;
                    foreach (var (word3, bit3) in shadowList)
                    {
                        if (word3.Intersect(word1 + word2).Any()) continue;
                        foreach (var (word4, bit4) in shadowList)
                        {
                            if (word4.Intersect(word1 + word2 + word3).Any()) continue;
                            foreach(var (word5, bit5) in shadowList)
                            {
                                // Console.WriteLine("{0} {1} {2} {3} {4}", word, word2, word3, word4, word5);
                                word5.ToCharArray();
                                if (word5.Intersect(word1 + word2 + word3 + word4).Any())
                                {
                                    // if (word5.Intersect(word4).Any()) Console.WriteLine(string.Join("", word5.Intersect(word4).ToList() ));
                                    continue;
                                }
                                
                                // Console.WriteLine("{0} {1} {2} {3} {4}", word1, word2, word3, word4, word5);
                                List<int> combination = new() { bit1, bit2, bit3, bit4, bit5 }; combination.Sort();

                                if (successCombs.Contains(combination)) continue; else successCombs.Add(combination);
                                 
                                Console.WriteLine(string.Join(" ", combination));
                                successWordsCount++;
                            }
                        }
                    }
                }

                words = new(shadowList);
            }

            Console.WriteLine("Successfulde kombinationer: " + successWordsCount);

           // successCombs.ForEach(Console.WriteLine);

            // successWords.ForEach(Console.WriteLine);

            // Thread thr1 = new Thread(method1);
            // Thread thr2 = new Thread(method2);
            // thr1.Start();
            // thr2.Start();

            return words;
        }
    }
}
