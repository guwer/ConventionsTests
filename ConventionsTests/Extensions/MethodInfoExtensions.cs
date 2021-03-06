using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ConventionsTests
{
    public static class MethodInfoExtensions
    {
        public static bool IsSkipReasonEmpty(this MethodInfo method)
        {
            return method.CustomAttributes
                .SelectMany(a => a.NamedArguments)
                .Where(n => n.MemberName == "Skip")
                .Select(n => n.TypedValue.Value)
                .Cast<string>()
                .Single().Length == 0;
        }

        public static bool IsDisplayNameValueEmpty(this MethodInfo method)
        {
            return method.CustomAttributes
                .SelectMany(a => a.NamedArguments)
                .Where(n => n.MemberName == "DisplayName")
                .Select(n => n.TypedValue.Value)
                .Cast<string>()
                .Single().Length == 0;
        }

        public static bool IsDisplayNameMissing(this MethodInfo method)
        {
            return !method.CustomAttributes
                .SelectMany(a => a.NamedArguments)
                .Select(n => n.MemberName)
                .Contains("DisplayName");
        }

        public static bool DisplayNameEqualsMethodName(this MethodInfo method)
        {
            var methodName = method.Name;

            var displayName = method.CustomAttributes
                .SelectMany(a => a.NamedArguments)
                .Single(n => n.MemberName == "DisplayName")
                .TypedValue.Value as string;

            return methodName == displayName;
        }

        public static bool IsTestMethod(this MethodInfo method)
        {
            return GetFactAndTheoryAttributes(method).Any();
        }

        public static bool IsSkippedTestMethod(this MethodInfo method)
        {
            return GetFactAndTheoryAttributes(method)
                .Where(a => a.NamedArguments.Select(n => n.MemberName).Contains("Skip"))
                .Any();
        }

        private static System.Collections.Generic.IEnumerable<CustomAttributeData> GetFactAndTheoryAttributes(MethodInfo method)
        {
            return method.CustomAttributes
                .Where(a => typeof(FactAttribute).IsAssignableFrom(a.AttributeType) || typeof(TheoryAttribute).IsAssignableFrom(a.AttributeType));
        }
    }
}