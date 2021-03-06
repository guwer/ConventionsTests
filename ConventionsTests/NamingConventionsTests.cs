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

        [Fact(DisplayName = "Test Methods Have Display Name")]
        public void TestMethods_HaveDisplayName()
        {
            var testMethods = Types.InAssemblies(ConventionsHelper.SolutionAssemblies.OnlyTestsAssemblies())
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Tests")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsTestMethod());

            var noDisplayNameMethods = testMethods
                .Where(m => m.IsDisplayNameMissing() || m.IsDisplayNameValueEmpty());

            testMethods.Should().NotBeEmpty();
            noDisplayNameMethods.Should().BeEmpty();
        }

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
    }
}