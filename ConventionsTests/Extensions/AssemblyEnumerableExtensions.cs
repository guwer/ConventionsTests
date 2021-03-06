using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConventionsTests
{
    public static class AssemblyEnumerableExtensions
    {
        public static IEnumerable<Assembly> WithoutTestsAssemblies(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(a => !a.FullName.Contains("Test"));
        }

        public static IEnumerable<Assembly> OnlyTestsAssemblies(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(a => a.FullName.Contains("Test"));
        }
    }
}