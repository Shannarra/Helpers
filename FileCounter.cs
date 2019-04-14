﻿using System;
using System.IO;
using System.Linq;

namespace CSFiles
{
    /// <summary>
    /// This program counts all files with certain extension.
    /// </summary>
    static class FileCounter
    {
        #region Extensions


        delegate uint ForeachDelegate(string item);

        private static void ForEach(this System.Collections.Generic.IEnumerable<string> coll, ForeachDelegate del)
        {
            foreach (var item in coll)
                del(item);
        }

        private static void ForEach(this System.Collections.IEnumerable coll, ForeachDelegate del)
        {
            if (coll != null)
                foreach (var item in coll)
                    del(item as string);
        }

        #endregion

        /// <summary>
        /// Gives you all files with certain extension in the directories argument <paramref name="dirs"/>
        /// </summary>
        /// <param name="dirs">Directories in a collection</param>
        /// <param name="extension">The file extension.</param>
        /// <returns><see cref="System.Collections.IEnumerable"/></returns>
        private static System.Collections.IEnumerable GetFilesIn(this System.Collections.Generic.IEnumerable<string> dirs, string extension)
        {
            if (dirs != null)
                foreach (var path in dirs)
                {
                    var files = from file in Directory.GetFiles(path)
                                where Path.GetExtension(file).EndsWith(extension)
                                select file as string;
                    return files;
                }

            return null;
        }

        /// <summary>
        /// Increments the counter
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static uint Increment(string e) => count++;

        /// <summary>
        /// The counter.
        /// </summary>
        private static uint count = 1;

        /// <summary>
        /// The times the <see cref="Count(string)"/> method was evoked.
        /// </summary>
        private static uint evoked = 1;

        static void Main(string[] args)
        {
            Console.WriteLine(Count(@"c:\") + Count(@"d:\"));
            Console.WriteLine($"Evoked: {evoked} times");
        }

        /// <summary>
        /// Counts all files with certain extension in the given <paramref name="path"/>
        /// </summary>
        /// <param name="path">The path to the files.</param>
        /// <returns><see cref="uint"/></returns>
        private static uint Count(string path)
        {
            evoked++;
            try
            {
                var dirs = from dir in Directory.GetDirectories(path)
                           where Directory.Exists(dir)
                           select dir;
                dirs.ForEach(Count);

                var csFiles = dirs.GetFilesIn(".cs");

                csFiles.ForEach(Increment);

                return count;

            }
            catch (UnauthorizedAccessException) { return count; }
        }
    }
}