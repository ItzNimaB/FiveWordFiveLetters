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

            foreach (string word in shadowList)
            {
                for (int i = 0; i < 5;  i++)
                {
                    Console.WriteLine(word);
                    string exWord = "backy";
                    if (word.ToCharArray().Any(c => c == word[0] || c == word[1] || c == word[2] || c == word[3] || c == word[4])) words.Remove(word);

                    string exWord2 = "fjord";
                    if (word.ToCharArray().Any(c => c == exWord2[0] || c == exWord2[1] || c == exWord2[2] || c == exWord2[3] || c == exWord2[4])) words.Remove(word);

                    string exWord3 = "muntz";
                    if (word.ToCharArray().Any(c => c == exWord3[0] || c == exWord3[1] || c == exWord3[2] || c == exWord3[3] || c == exWord3[4])) words.Remove(word);
                }

            }
            return words;
        }
    }
}
