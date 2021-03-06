using FluentAssertions;
using NetArchTest.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ConventionsTests
{
    public class TestMethodsConventionsTests
    {
        [Fact(DisplayName = "Test methods have DisplayName")]
        public void TestMethods_HaveDisplayName()
        {
            var testMethods = GetTestMethods();

            var noDisplayNameMethods = testMethods
                .Where(m => m.IsDisplayNameMissing() || m.IsDisplayNameValueEmpty());

            testMethods.Should().NotBeEmpty();
            noDisplayNameMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "Display name is not method name")]
        public void DisplayName_IsNotMethodName()
        {
            var testMethods = GetTestMethods();

            var sameMethodAndDisplayNameMethods = testMethods
                .Where(m => m.DisplayNameEqualsMethodName());

            testMethods.Should().NotBeEmpty();
            sameMethodAndDisplayNameMethods.Should().BeEmpty();
        }

        // Methods marked with "Skip" with no reason text are not skipped by xunit.
        // This test checks if reason is not empty (Skip = "").
        [Fact(DisplayName = "Skipped test methods have reason")]
        public void SkippedTestMethods_HaveReason()
        {
            var testMethods = GetTestMethods();

            var noSkipReasonMethods = testMethods
                .Where(m => m.IsSkippedTestMethod() && m.IsSkipReasonEmpty());

            testMethods.Should().NotBeEmpty();
            noSkipReasonMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "test", Skip = "reason")]
        public void SkippedMethod()
        { }

        // This would cause to fail above test - skip does not have a reason text
        //[Fact(DisplayName = "a", Skip = "")]
        //public void NotSkippedMethod()
        //{ }

        private static IEnumerable<MethodInfo> GetTestMethods()
        {
            return Types.InAssemblies(ConventionsHelper.SolutionAssemblies.OnlyTestsAssemblies())
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Tests")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsTestMethod());
        }
    }
}