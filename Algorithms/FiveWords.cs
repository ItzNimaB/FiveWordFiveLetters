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

        public event EventHandler<string> Combinations;
        public event EventHandler<int> Progress;
        public event EventHandler<int> WordsCount;

        public Dictionary<int, string> LoadWords()
        {
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

            return dirWords;
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
                        Progress?.Invoke(this, 1);
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
