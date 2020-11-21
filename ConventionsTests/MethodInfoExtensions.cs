using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ConventionsTests
{
    public static class MethodInfoExtensions
    {
        public static bool DisplayNameIsEmpty(this MethodInfo method)
        {
            return method.CustomAttributes
                .SelectMany(a => a.NamedArguments)
                .Where(n => n.MemberName == "DisplayName")
                .Select(n => n.TypedValue.Value).Cast<string>().Single().Length == 0;
        }

        public static bool DisplayNameMissing(this MethodInfo method)
        {
            return !method.CustomAttributes
                .SelectMany(a => a.NamedArguments)
                .Select(n => n.MemberName)
                .Contains("DisplayName");
        }

        public static bool IsTestMethod(this MethodInfo method)
        {
            return method.CustomAttributes
                .Select(a => a.AttributeType)
                .Where(a => typeof(FactAttribute).IsAssignableFrom(a) || typeof(TheoryAttribute).IsAssignableFrom(a))
                .Any();
        }
    }
}