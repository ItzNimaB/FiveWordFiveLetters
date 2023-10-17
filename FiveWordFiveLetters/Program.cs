using System;
using System.Diagnostics;
using System.Threading;
using FiveWordFiveLetters;

var stopwatch = new Stopwatch();

stopwatch.Start();

List<string> words = File.ReadAllLines("alpha.txt").ToList();
List<string> validWords = new();

int moreThanFiveWords = 0;
int notUniqueLetters = 0;

foreach (string word in words)
{
    if (word.Length != 5)
    {
        moreThanFiveWords++;
        continue;
    }
    else validWords.Add(word);

    var isValidWord = true;
    foreach (char letter in word)
    {
        if (word.ToCharArray().Count(c => c == letter) > 1) {
            isValidWord = false;
            break;
        }
    }

    if (!isValidWord)
    {
        validWords.Remove(word);
        notUniqueLetters++;
    }
}

// validWords.ForEach(Console.WriteLine);

Algorithm.Sort(validWords); //.ForEach(Console.WriteLine);

Console.Write("Words with more than 5 words: ");
Console.WriteLine(moreThanFiveWords);
Console.Write("Words with recurring letters: ");
Console.WriteLine(notUniqueLetters);
Console.Write("Amount of unique words: ");
Console.WriteLine(validWords.Count);

stopwatch.Stop();
Console.WriteLine("ms: " + stopwatch.ElapsedMilliseconds);

Console.Read();