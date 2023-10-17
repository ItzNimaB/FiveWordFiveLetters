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
            List<string> successCombs = new();
            int successWordsCount = 0;

            foreach (string word in shadowList)
            {
                // Console.WriteLine("{0}", word);
                foreach (string word2 in shadowList)
                {
                    if (word2.Intersect(word).Any()) continue;
                    // Console.WriteLine("{0} {1}", word, word2);
                    foreach (string word3 in shadowList)
                    {
                        if (word3.Intersect(word2).Any() || word3.Intersect(word).Any()) continue;
                        // Console.WriteLine("{0} {1} {2}", word, word2, word3);
                        foreach (string word4 in shadowList)
                        {
                            if (word4.Intersect(word3).Any() || word4.Intersect(word2).Any() || word4.Intersect(word).Any()) continue;
                            // Console.WriteLine("{0} {1} {2} {3}", word, word2, word3, word4);
                            List<string> shadowList5 = new(shadowList);
                            // shadowList5.RemoveAll(r => r.Intersect(word4).Any() || r.Intersect(word3).Any() || r.Intersect(word2).Any() || r.Intersect(word).Any());
                            foreach (string word5 in shadowList5)
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4}", word, word2, word3, word4, word5);
                                if (word5.Intersect(word4).Any() || word5.Intersect(word3).Any() || word5.Intersect(word2).Any() || word5.Intersect(word).Any())
                                {
                                    if (word5.Intersect(word4).Any()) Console.WriteLine(string.Join("", word5.Intersect(word4).ToList() ));
                                    continue;
                                }
                                
                                Console.WriteLine("{0} {1} {2} {3} {4}", word, word2, word3, word4, word5);
                                List<string> combination = new() { word, word2, word3, word4, word5 }; combination.Sort();

                                if (successCombs.Contains(string.Join("", combination))) continue; else successCombs.Add(string.Join("", combination));

                                Console.WriteLine(string.Join(" ", combination));
                                successWordsCount++;
                            }
                        }
                    }
                }

                words = new(shadowList);
            }

            Console.WriteLine("Successfulde kombinationer: " + successWordsCount);

            successCombs.ForEach(Console.WriteLine);

            // successWords.ForEach(Console.WriteLine);

            // Thread thr1 = new Thread(method1);
            // Thread thr2 = new Thread(method2);
            // thr1.Start();
            // thr2.Start();

            return words;
        }
    }
}
