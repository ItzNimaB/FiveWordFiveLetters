using System.Diagnostics;

List<long> timesElapsed = new();
Stopwatch stopwatch = new();
stopwatch.Start();

Dictionary<int, string> dirWords = new();

int totalCombinations = 0;
int threadsDone = 0;

void loadWords()
{
    int duplicateWords = 0;

    using var reader = new StreamReader("alpha.txt");
    var word = "";

    while ((word = reader.ReadLine()) != null)
    {
        if (word.Length != 5) continue;
        if (word.Distinct().Count() != 5) continue;

        var bit = 0;
        foreach (char letter in word) { bit |= 1 << letter - 'a'; }

        if (dirWords.ContainsKey(bit)) {
            duplicateWords++;
            continue;
        }

        dirWords.Add(bit, word);
    }

    dirWords = dirWords.OrderBy(kv => { int key = 0; if (kv.Value.Contains('b') || kv.Value.Contains('c') || kv.Value.Contains('d') || kv.Value.Contains('f')) key = 1; else key = 0; return key; })
    .ThenBy(kv => kv.Value)
    .ToDictionary(kv => kv.Key, kv => kv.Value);

    Console.WriteLine("Valid - duplicate words: {0} - {1}. Time: {2}", dirWords.Count, duplicateWords, stopwatch.ElapsedMilliseconds);
}

loadWords();
void function(double startIndex = 0, double endIndex = 0)
{
    List<List<string>> combinations = new();
    var bits = dirWords.Keys.ToArray();

    startIndex = Math.Round(startIndex * dirWords.Count);
    if (endIndex > 0) endIndex = Math.Round(endIndex * dirWords.Count);
    else endIndex = dirWords.Count;

    int wordsCount = dirWords.Count;

    // FindCombos(0, 0, new int[0]);

    void FindCombos(int startIndex, int usedBits, int[] combination)
    {
        if (combination.Length == 5)
        {
            combinations.Add(new() {
                dirWords[combination[0]],
                dirWords[combination[1]],
                dirWords[combination[2]],
                dirWords[combination[3]],
                dirWords[combination[4]]
            });
            Console.WriteLine(string.Join(" ", combination));
            return;
        }

        for (int i = startIndex; i < wordsCount; i++)
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

    // Mads' program: 00:04:40:96
     
    for (int i = (int)startIndex; i < endIndex; i++)
    {
        Console.WriteLine("{0} | {1}/{2} [{3}%] ({4})", dirWords[bits[i]], i+1, wordsCount, Math.Round((double)i / (wordsCount - 1) * 100, 2), stopwatch.Elapsed);
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
                        combinations.Add(new() { dirWords[bits[i]], dirWords[bits[j]], dirWords[bits[k]], dirWords[bits[l]], dirWords[bits[m]] });
                    }
                }
            }
        }
    }
    
    // Nima's program: 00:04:32.19

    totalCombinations += combinations.Count;
    Console.WriteLine("{0}/{1} combinations | {2} | [{3} - {4}]", combinations.Count, totalCombinations, stopwatch.Elapsed, startIndex, endIndex);
    threadsDone++;
    timesElapsed.Add(stopwatch.ElapsedMilliseconds);
}

for (int i = 0; i < 1; i++) { function(); } /* Console.WriteLine(string.Join(" ", timesElapsed)); */

// int amountOfThreads = 16; Parallel.For(0, amountOfThreads, i => function( (double)i / amountOfThreads, (double)(i + 1) / amountOfThreads));