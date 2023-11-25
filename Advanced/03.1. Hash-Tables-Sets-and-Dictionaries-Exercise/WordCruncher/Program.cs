using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCruncher
{
    class Program
    {
        static void Main()
        {
            var words = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();            
            string lookUpWord = Console.ReadLine();
            var matches = new List<List<string>>();
            var match = new List<string>();
            FindMatchs(words, lookUpWord, matches, match);

            var result = matches.Select(m => string.Join(" ", m)).OrderBy(m => m);

            Console.WriteLine(string.Join(Environment.NewLine, result));            
        }

        private static void FindMatchs(List<string> words, string lookUpWord, List<List<string>> allMathces, List<string> currMatch)
        {           
            while (lookUpWord.Length > 0)
            {
                string matchedWord = "";
                for (int i = 0; i < words.Count; i++)                
                {
                    if (lookUpWord.StartsWith(words[i]))
                    {
                        if (matchedWord == "")
                        {
                            matchedWord = words[i];
                            currMatch.Add(words[i]);
                            words.RemoveAt(i);
                            i--;
                        }
                        else if (matchedWord != words[i])
                        {
                            var copy = new List<string>(currMatch);
                            copy[copy.Count - 1] = words[i];
                            
                            var wordsCopy = new List<string>(words);
                            wordsCopy[i] = matchedWord;
                            string copyLookUp = lookUpWord.Substring(words[i].Length);

                            FindMatchs(wordsCopy, copyLookUp, allMathces, copy);                           
                        }
                        else
                        {
                            continue;
                        }
                        
                    }
                }                

                if (matchedWord == "")
                {
                    break;
                }

                lookUpWord = lookUpWord.Substring(matchedWord.Length);
            }

            if (lookUpWord.Length == 0)
            {
                allMathces.Add(currMatch);
            }
        }
    }
}
