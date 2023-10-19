using System.Diagnostics;

List<long> timesElapsed = new();
void function(double startIndex = 0, double endIndex = 0)
{
    Stopwatch stopwatch = new();
    stopwatch.Start();

    int duplicateWords = 0;

    List<string> words = new();

    Dictionary<int, string> dirWords = new();
    int[] bits = new int[words.Count];

    using (var reader = new StreamReader("alpha.txt"))
    {
        var word = "";

        while ((word = reader.ReadLine()) != null)
        {
            if (word.Length != 5) continue;
            if (word.Distinct().Count() != 5) continue;

            var bit = 0;
            foreach (char letter in word) { bit |= 1 << letter - 'a'; }
            try
            {
                dirWords.Add(bit, word);
                Array.Resize(ref bits, bits.Length + 1);
                bits[^1] = bit;
                words.Add(word);
            }
            catch { duplicateWords++; }
        }

        Console.WriteLine("Valid - duplicate words: {0} - {1}. Time: {2}", words.Count, duplicateWords, stopwatch.ElapsedMilliseconds);
    }

    List<List<string>> combinations = new();

    startIndex = Math.Round(startIndex * words.Count);
    if (endIndex > 0) endIndex = Math.Round(endIndex * words.Count);
    else endIndex = words.Count;

    int wordsCount = words.Count;

    for (int i = (int)startIndex; i < endIndex; i++)
    {
        Console.WriteLine("{0} | {1}/{2} [{3}%] ({4})", words[i], i+1, wordsCount, Math.Round((double)i / (wordsCount - 1) * 100, 2), stopwatch.Elapsed);
        for (int j = i + 1; j < wordsCount; j++)
        {
            if ((bits[i] & bits[j]) != 0) continue;
            var testComb = bits[i] + bits[j];
            for (int k = j + 1; k < wordsCount; k++)
            {
                if ((testComb & bits[k]) != 0) continue;
                var testComb2 = testComb + bits[k];
                for (int l = k + 1; l < wordsCount; l++)
                {
                    if ((testComb2 & bits[l]) != 0) continue;
                    var testComb3 = testComb2 + bits[l];
                    for (int m = l + 1; m < wordsCount; m++)
                    {
                        if ((testComb3 & bits[m]) != 0) continue;
                        // Console.WriteLine("{0} {1} {2} {3} {4}", words[i], words[j], words[k], words[l], words[m]);
                        combinations.Add(new() { words[i], words[j], words[k], words[l], words[m] });
                    }
                }
            }
        }
    }

    // combinations.ForEach(x => Console.WriteLine(string.Join(" ", x)));
    Console.WriteLine("{0} combinations, {1}", combinations.Count, stopwatch.Elapsed);
    // Console.WriteLine("{0} {1}", startIndex, endIndex);
    timesElapsed.Add(stopwatch.ElapsedMilliseconds);
}

for (int i = 0; i < 1;  i++) { function(); } Console.WriteLine(string.Join(" ", timesElapsed));

List<Thread> threads = new()
{
    new Thread(() => function(0, 0.2)),
    new Thread(() => function(0.2, 0.4)),
    new Thread(() => function(0.4, 0.6)),
    new Thread(() => function(0.6, 0.8)),
    new Thread(() => function(0.8, 1))
};

// threads.ForEach(x => x.Start());