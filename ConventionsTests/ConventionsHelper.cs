using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Common;
using WebApp;

namespace ConventionsTests
{
    public class ConventionsHelper
    {
        private static readonly Lazy<Assembly> _webApiAssembly = new Lazy<Assembly>(() => typeof(Startup).Assembly);
        private static readonly Lazy<Assembly> _commonAssembly = new Lazy<Assembly>(() => typeof(Class1).Assembly);
        private static readonly Lazy<string> _rootPath = new Lazy<string>(GetRootPath);
        private static Lazy<List<string>> _sourceFiles = new Lazy<List<string>>(GetSourceFiles);
        private const string _roorNamespace = nameof(ConventionsTests);

        public static string RootPath => _rootPath.Value;
        public static Assembly WebApiAssembly => _webApiAssembly.Value;
        public static Assembly CommonAssembly => _commonAssembly.Value;
        public static List<string> SourceFiles => _sourceFiles.Value;

        public static IEnumerable<Assembly> SolutionAssemblies => GetSolutionAssemblies();

        private static List<string> GetSourceFiles()
        {
            return Directory.EnumerateFiles(RootPath, "*.cs", SearchOption.AllDirectories).Where(p => !p.Contains("obj")).ToList(); ;
        }

        private static IEnumerable<Assembly> GetSolutionAssemblies()
        {
            var assembliesExtensions = new[] { ".dll", ".exe" };
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var files = dir.GetFiles($"{_roorNamespace}*", SearchOption.AllDirectories);

            return files
                .Where(f => !f.FullName.Contains("obj"))
                .Where(f => assembliesExtensions.Contains(f.Extension))
                .Select(f => Assembly.LoadFrom(f.FullName));
        }

        private static string GetRootPath()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\"));
        }
    }
}
