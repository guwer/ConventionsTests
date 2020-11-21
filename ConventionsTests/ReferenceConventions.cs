using Common;
using FluentAssertions;
using System.Linq;
using WebApp.Controllers;
using Xunit;

namespace ConventionsTests
{
    public class ReferenceConventions
    {
        [Fact]
        public void AssemblyADoesNotReferenceAssemblyB()
        {
            var refernecedAssemblies = typeof(Class1).Assembly.GetReferencedAssemblies();
            var result = refernecedAssemblies
                .Select(a => a.Name)
                .Contains(typeof(HomeController).Assembly.GetName().Name);

            Assert.NotEmpty(refernecedAssemblies);
            Assert.False(result);
        }

        [Fact]
        public void AssemblyXDoesNotReferenceAssemblyY()
        {
            // With FluentAssertions
            typeof(Class1).Assembly.Should().NotReference(typeof(HomeController).Assembly);
        }
    }
}