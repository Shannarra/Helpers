using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Helpers
{
    internal static class Scope
    {
        /// <summary>
        /// Returns the index of a } in the current line, -2 if the given <paramref name="braceIndex"/> != { and 
        /// -100 for the other cases
        /// </summary>
        /// <param name="text">The text to search</param>
        /// <param name="braceIndex">The index of the {</param>
        /// <returns></returns>
        static int PerfectIndex(string text, int braceIndex)
        {
            if (text[braceIndex] != '{')
                return -2; // no brace at that index

            Stack stack = new Stack();

            for (int i = braceIndex; i < text.Length; i++)
                if (text[i] == '{')
                    stack.Push((int)text[i]);
                else if (text[i] == '}')
                {
                    stack.Pop();
                    if (stack.Count == 0)
                        return i; // brace closed in the same line
                }
            return -100; // opening brace
        }

        ///<summary>
        /// Returns all lines in the given <paramref name="file"/> as <see cref="StringCollection"/>
        ///</summary>
        ///<param name="file">The file to traverse</param>
        static StringCollection GetLines(string file)
        {
            StringCollection lines = new StringCollection();
            string line;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(file))
                while ((line = reader.ReadLine()) != null)
                    lines.Add(line);
            return lines;
        }

        /// <summary>
        /// Defines all scopes in the given <paramref name="file"/> as tuple of dictionaries
        /// </summary>
        /// <param name="file">The file to traverse</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        static Tuple<Dictionary<int, int>, Dictionary<int, int>> DefineScopes(string file)
        {
            StringCollection lines = GetLines(file);

            int lineNumber = 0;
            Dictionary<int, int> openings = new Dictionary<int, int>();  // [line] => index
            Dictionary<int, int> closings = new Dictionary<int, int>(); // [line] => index

            foreach (string line in lines)
            {
                int index = 0;
                foreach (char ch in line)
                {
                    if (ch == '{')
                        if (PerfectIndex(line, index) == -100) //pure opening brace
                            openings.Add(lineNumber, index);
                        else if (PerfectIndex(line, index) > 0) //Opened and closed at line // asdasd {asasd}asd;
                        { }
                    try
                    {
                        if (ch == '}' && line[line.IndexOf(ch) + 1] == "'".ToCharArray()[0]) // closed in string '}'
                        { }
                    }
                    catch (Exception) // closing brace
                    {
                        closings.Add(lineNumber, index);
                    }

                    index++;
                }

                lineNumber++;
            }

            return new Tuple<Dictionary<int, int>, Dictionary<int, int>>(openings, closings);
        }

        /// <summary>
        /// Prints the given <see cref="Tuple{T1, T2}"/> of <paramref name="scopes"/> (opened and closed braces
        /// </summary>
        /// <param name="scopes">The scopes in the file.</param>
        static void PrintScopes(Tuple<Dictionary<int, int>, Dictionary<int, int>> scopes)
        {
            List<int> known = new List<int>();
            foreach (var opening in scopes.Item1) // for all opened braces
            {
                foreach (var closing in scopes.Item2) // check for matching closing 
                {
                    if (opening.Value == closing.Value) // they are under one another
                    {
                        if (closing.Key < opening.Key) // impossible to do something like this } {
                            continue;
                        if (known.Contains(opening.Key)) // add to the known scopes so no repetitions happen
                            continue;
                        Console.WriteLine($"Perfect match for lines: {opening.Key + 1}:{closing.Key + 1}!");

                        known.Add(opening.Key);
                    }
                }
            }
        }

        public static void Run(string file) 
        { 
            PrintScopes(DefineScopes(file));
        }        
    }
}
