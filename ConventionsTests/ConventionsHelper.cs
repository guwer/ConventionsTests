using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConventionsTests
{
    public class ConventionsHelper
    {
        public static IEnumerable<string> GetSourceFiles()
        {
            var root = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\"));
            var sourceFiles = Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories).Where(p => !p.Contains("obj"));

            return sourceFiles;
        }
    }
}