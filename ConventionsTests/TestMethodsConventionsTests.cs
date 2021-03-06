using FluentAssertions;
using NetArchTest.Rules;
using System.Linq;
using Xunit;

namespace ConventionsTests
{
    public class TestMethodsConventionsTests
    {
        [Fact(DisplayName = "Test methods have DisplayName")]
        public void TestMethods_HaveDisplayName()
        {
            var noDisplayNameMethods = Types.InAssemblies(ConventionsHelper.SolutionAssemblies.OnlyTestsAssemblies())
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Tests")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsTestMethod() &&
                    (m.IsDisplayNameMissing() || m.IsDisplayNameValueEmpty()));

            noDisplayNameMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "Display name is not method name")]
        public void DisplayName_IsNotMethodName()
        {
            var sameMethodAndDisplayNameMethods = Types.InAssemblies(ConventionsHelper.SolutionAssemblies.OnlyTestsAssemblies())
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Tests")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsTestMethod() && m.DisplayNameEqualsMethodName());

            sameMethodAndDisplayNameMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "Skipped test methods have reason")]
        public void SkippedTestMethods_HaveReason()
        {
            var noSkipReasonMethods = Types.InAssemblies(ConventionsHelper.SolutionAssemblies.OnlyTestsAssemblies())
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Tests")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsSkippedTestMethod() && m.IsSkipReasonEmpty());

            noSkipReasonMethods.Should().BeEmpty();
        }
    }
}