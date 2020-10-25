using Common;
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
            var assemblies = typeof(Class1).Assembly.GetReferencedAssemblies();

            var result = assemblies.Select(a => a.Name).Contains(typeof(HomeController).Assembly.GetName().Name);

            Assert.NotEmpty(assemblies);
            Assert.False(result);
        }
    }
}