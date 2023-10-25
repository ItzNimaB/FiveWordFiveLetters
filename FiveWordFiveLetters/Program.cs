using Algorithms;
using System.Diagnostics;

Stopwatch stopwatch = new();
stopwatch.Start();

int totalCombinations = 0;
int threadsDone = 0;

FiveWords fiveWords = new("alpha");
Dictionary<int, string> dirWords = fiveWords.LoadWords();

void function(double startIndex = 0, double endIndex = 0)
{
    List<List<string>> combinations = new();

    // Console.WriteLine(startIndex + " - " + endIndex);

    // fiveWords.SortRecursive(startIndex, endIndex);

    combinations.AddRange(fiveWords.SortForLoops(startIndex, endIndex));

    // totalCombinations += combinations.Count;
    // Console.WriteLine("{0}/{1} combinations | {2} | [{3} - {4}]", combinations.Count, totalCombinations, stopwatch.Elapsed, startIndex, endIndex);
    threadsDone++;
}

// for (int i = 0; i < 1; i++) function();

int amountOfThreads = 16; Parallel.For(0, amountOfThreads, i => function( (double)i / amountOfThreads, (double)(i + 1) / amountOfThreads));