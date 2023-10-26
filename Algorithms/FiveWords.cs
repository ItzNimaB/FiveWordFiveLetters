using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class FiveWords
    {
        private string FileName { get; set; }

        public FiveWords(string FileName) {
            this.FileName = FileName;

            LoadWords();
        }

        Dictionary<int, string> dirWords = new();

        readonly Stopwatch stopwatch = new();

        // private readonly List<List<string>> combinations = new();
        public event EventHandler<string> Combinations;
        public event EventHandler<int> Progress;
        public event EventHandler<int> WordsCount;

        public Dictionary<int, string> LoadWords()
        {
            stopwatch.Start();

            using var reader = new StreamReader($"{FileName}.txt");
            var word = "";

            while ((word = reader.ReadLine()) != null)
            {
                if (word.Length != 5) continue;
                if (word.Distinct().Count() != 5) continue;

                var bit = 0;
                foreach (char letter in word) { bit |= 1 << letter - 'a'; }

                if (dirWords.ContainsKey(bit)) continue;

                dirWords.Add(bit, word);
            }

            dirWords = dirWords.OrderBy(kv => { int key = 0; if (kv.Value.Contains('b') || kv.Value.Contains('c') || kv.Value.Contains('d') || kv.Value.Contains('f')) key = 1; else key = 0; return key; })
            .ThenBy(kv => kv.Value)
            .ToDictionary(kv => kv.Key, kv => kv.Value);

            stopwatch.Stop();

            return dirWords;
        }

        public void SortRecursive(double startIndex = 0, double endIndex = 0)
        {
            stopwatch.Start();

            var bits = dirWords.Keys.ToArray();

            startIndex = Math.Round(startIndex * dirWords.Count);
            if (endIndex > 0) endIndex = Math.Round(endIndex * dirWords.Count);
            else endIndex = dirWords.Count;

            
            FindCombos(0, 0, new int[0]);

            void FindCombos(int startIndex, int usedBits, int[] combination)
            {
                if (combination.Length == 5)
                {
                    Combinations?.Invoke(this, string.Format("{0} {1} {2} {3} {4}",
                        dirWords[combination[0]],
                        dirWords[combination[1]],
                        dirWords[combination[2]],
                        dirWords[combination[3]],
                        dirWords[combination[4]]
                    ));
                    // Console.WriteLine(string.Join(" ", combination));
                    // foreach (var item in combination) Console.Write("{0} ", dirWords[item]); Console.WriteLine();
                    return;
                }

                for (int i = startIndex; i < endIndex; i++)
                {
                    if ((usedBits & bits[i]) == 0)
                    {
                        int[] newCombos = new int[combination.Length + 1];
                        combination.CopyTo(newCombos, 0);
                        newCombos[combination.Length] = bits[i];
                        FindCombos(i + 1, usedBits | bits[i], newCombos);
                    }
                }
            }
        }

        public Task SortForLoops(int amountOfThreads = 1)
        {
            return Task.Run(() =>
            {
                Parallel.For(0, amountOfThreads, thrdNum =>
                {
                var bits = dirWords.Keys.ToArray();

                int wordsCount = dirWords.Count;
                WordsCount?.Invoke(this, wordsCount);

                    int startIndex = (int)Math.Round((double)thrdNum / amountOfThreads * wordsCount);
                    int endIndex = (int)Math.Round((double)(thrdNum + 1) / amountOfThreads * wordsCount);

                    for (int i = startIndex; i < endIndex; i++)
                    {
                        // Progress?.Invoke(this, (int)Math.Round((double)i / (wordsCount - 1) * 100, 2));
                        Progress?.Invoke(this, 1);
                        // Console.WriteLine("{0} | {1}/{2} [{3}%] ({4})", dirWords[bits[i]], i+1, wordsCount, progress, stopwatch.Elapsed);
                        for (int j = i + 1; j < wordsCount; j++)
                        {
                            if ((bits[i] & bits[j]) != 0) continue;
                            var bitIJ = bits[i] | bits[j];
                            for (int k = j + 1; k < wordsCount; k++)
                            {
                                if ((bitIJ & bits[k]) != 0) continue;
                                var bitJK = bitIJ | bits[k];
                                for (int l = k + 1; l < wordsCount; l++)
                                {
                                    if ((bitJK & bits[l]) != 0) continue;
                                    var bitJKL = bitJK | bits[l];
                                    for (int m = l + 1; m < wordsCount; m++)
                                    {
                                        if ((bitJKL & bits[m]) != 0) continue;
                                        // Console.WriteLine("{0} {1} {2} {3} {4}", dirWords[bits[i]], dirWords[bits[j]], dirWords[bits[k]], dirWords[bits[l]], dirWords[bits[m]]);
                                        Combinations?.Invoke(this, string.Format("{0} {1} {2} {3} {4}", dirWords[bits[i]], dirWords[bits[j]], dirWords[bits[k]], dirWords[bits[l]], dirWords[bits[m]]));
                                    }
                                }
                            }
                        }
                    }
                });
            });
                
        }
    }
}
