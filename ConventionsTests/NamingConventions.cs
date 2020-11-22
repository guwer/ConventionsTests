using FluentAssertions;
using NetArchTest.Rules;
using System.Linq;
using Xunit;

namespace ConventionsTests
{
    public class NamingConventions
    {
        [Fact(DisplayName = "a")]
        public void ClassImplementingIHandler_HaveSuffixHandler()
        {
        }

        [Fact(DisplayName = "a")]
        public void AttributeClass_HaveSuffixAttribute()
        {
        }

        [Fact(DisplayName = "a")]
        public void EventArgsClass_HaveSuffixEventArgs()
        {
        }

        [Fact(DisplayName = "a")]
        public void TestMethods_HaveDisplayName()
        {
            var noDisplayNameMethods = Types.InAssembly(typeof(NamingConventions).Assembly)
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Conventions")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsTestMethod() &&
                    (m.IsDisplayNameMissing() || m.IsDisplayNameValueEmpty()));

            noDisplayNameMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "a")]
        public void SkippedTestMethods_HaveReason()
        {
            // Methods marked with "Skip" with no reason text are not skipped.
            // This test checks if reason is not empty (Skip = "").
            var noSkipReasonMethods = Types.InAssembly(typeof(NamingConventions).Assembly)
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Conventions")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsSkippedTestMethod() &&  m.IsSkipReasonEmpty());

            noSkipReasonMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "a", Skip = "s")]
        public void SkippedMethod()
        { }

        //[Fact(DisplayName = "a", Skip = "")]
        //public void NotSkippedMethod()
        //{ }
    }
}