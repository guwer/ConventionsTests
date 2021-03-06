using Common;
using FluentAssertions;
using System.Linq;
using WebApp.Controllers;
using Xunit;

namespace ConventionsTests
{
    public class ReferenceConventionsTests
    {
        [Fact(DisplayName = "Assembly Common Does Not Reference Assembly WebApp")]
        public void AssemblyCommonDoesNotReferenceAssemblyWebApp()
        {
            var refernecedAssemblies = typeof(Class1).Assembly.GetReferencedAssemblies();
            var result = refernecedAssemblies
                .Select(a => a.Name)
                .Contains(typeof(HomeController).Assembly.GetName().Name);

            Assert.NotEmpty(refernecedAssemblies);
            Assert.False(result);
        }

        [Fact(DisplayName = "Assembly Common Does Not Reference Assembly WebApp With Fluent Assertions")]
        public void AssemblyCommonDoesNotReferenceAssemblyWebApp_WithFluentAssertions()
        {
            // With FluentAssertions
            typeof(Class1).Assembly.Should().NotReference(typeof(HomeController).Assembly);
        }

        [Fact(DisplayName = "Assembly WebApp References Assembly Common")]
        public void AssemblyWebAppReferencesAssemblyCommon()
        {
            var refernecedAssemblies = typeof(HomeController).Assembly.GetReferencedAssemblies();
            var result = refernecedAssemblies
                .Select(a => a.Name)
                .Contains(typeof(Class1).Assembly.GetName().Name);

            Assert.NotEmpty(refernecedAssemblies);
            Assert.True(result);
        }
    }
}