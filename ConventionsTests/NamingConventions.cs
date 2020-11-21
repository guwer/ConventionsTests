using FluentAssertions;
using NetArchTest.Rules;
using System.Linq;
using Xunit;

namespace ConventionsTests
{
    public class NamingConventions
    {
        [Fact]
        public void ClassImplementingIHandler_HaveSuffixHandler()
        {
        }

        [Fact]
        public void AttributeClass_HaveSuffixAttribute()
        {
        }

        [Fact]
        public void EventArgsClass_HaveSuffixEventArgs()
        {
        }

        [Fact(DisplayName = "")]
        public void TestMethod()
        {
            var methods = Types.InAssembly(typeof(NamingConventions).Assembly)
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Conventions")
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsTestMethod() &&
                    (m.DisplayNameMissing() || m.DisplayNameIsEmpty()));

            methods.Should().BeEmpty();
        }
    }
}