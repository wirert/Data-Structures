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

            if (string.IsNullOrEmpty(lookUpWord) || words.Count == 0)
            {
                return;
            }

            FindMatchs(words, lookUpWord, matches, match);

            var result = matches.Select(m => string.Join(" ", m)).Distinct().OrderBy(m => m);

            Console.WriteLine(string.Join(Environment.NewLine, result));            
        }

        private static void FindMatchs(List<string> words, string lookUpWord, List<List<string>> allMathces, List<string> currMatchList)
        {           
            while (lookUpWord.Length > 0)
            {
                string previousMatchedWord = "";
                for (int i = 0; i < words.Count; i++)                
                {
                    if (lookUpWord.StartsWith(words[i]))
                    {
                        if (previousMatchedWord == "")
                        {
                            previousMatchedWord = words[i];
                            currMatchList.Add(words[i]);
                            words.RemoveAt(i);
                            i--;
                        }
                        else if (previousMatchedWord != words[i])
                        {
                            var newMatchList = new List<string>(currMatchList);
                            newMatchList[newMatchList.Count - 1] = words[i];
                            
                            var wordsCopy = new List<string>(words);
                            wordsCopy[i] = previousMatchedWord;
                            string newLookUpWord = lookUpWord.Substring(words[i].Length);

                            FindMatchs(wordsCopy, newLookUpWord, allMathces, newMatchList);                           
                        }
                        else
                        {
                            continue;
                        }                        
                    }
                }                

                if (previousMatchedWord == "")
                {
                    break;
                }

                lookUpWord = lookUpWord.Substring(previousMatchedWord.Length);
            }

            if (lookUpWord.Length == 0)
            {
                allMathces.Add(currMatchList);
            }
        }
    }
}
