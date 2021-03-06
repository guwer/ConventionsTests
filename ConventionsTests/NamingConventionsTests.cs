using FluentAssertions;
using NetArchTest.Rules;
using System.IO;
using System.Linq;
using Xunit;

namespace ConventionsTests
{
    public class NamingConventionsTests
    {
        [Fact(DisplayName = "test")]
        public void ClassImplementingIHandler_HaveSuffixHandler()
        {
        }

        [Fact(DisplayName = "test")]
        public void AttributeClass_HaveSuffixAttribute()
        {
        }

        [Fact(DisplayName = "test")]
        public void EventArgsClass_HaveSuffixEventArgs()
        {
        }

        [Fact(DisplayName = "test")]
        public void TestMethods_HaveDisplayName()
        {
            var noDisplayNameMethods = Types.InAssembly(typeof(NamingConventionsTests).Assembly)
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

        [Fact(DisplayName = "test")]
        public void SkippedTestMethods_HaveReason()
        {
            // Methods marked with "Skip" with no reason text are not skipped.
            // This test checks if reason is not empty (Skip = "").
            var noSkipReasonMethods = Types.InAssembly(typeof(NamingConventionsTests).Assembly)
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Conventions")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsSkippedTestMethod() &&  m.IsSkipReasonEmpty());

            noSkipReasonMethods.Should().BeEmpty();
        }

        [Fact(DisplayName = "test", Skip = "reason")]
        public void SkippedMethod()
        { }

        [Theory(DisplayName = "File names have correct acronym casing")]
        [InlineData("dto", "Dto")]
        [InlineData("qa", "QA")]
        public void FileNames_HaveCorrectAcronymCasing(string element, string expectedElement)
        {
            var namesWithElement = ConventionsHelper
                .SourceFiles
                .Select(f => Path.GetFileName(f))
                .Where(f => f.ToLower().Contains(element));

            var correctNames = namesWithElement.Where(f => f.Contains(expectedElement));

            correctNames.Should().BeEquivalentTo(namesWithElement);
        }

        //[Fact(DisplayName = "a", Skip = "")]
        //public void NotSkippedMethod()
        //{ }
    }
}