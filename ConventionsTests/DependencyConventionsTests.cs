using System.Linq;
using Common;
using NetArchTest.Rules;
using WebApp.Controllers;
using Xunit;

namespace ConventionsTests
{
    public class DependencyConventionsTests
    {
        [Fact(DisplayName = "s")]
        public void TypesInANamespace_HaveNoDependenciesOnTypesInBNamespace()
        {
            var webAppNamespaceTypes = Types.InAssembly(typeof(HomeController).Assembly)
                .That()
                .ResideInNamespaceStartingWith(nameof(WebApp))
                .GetTypes()
                .Select(t => t.Namespace);

            var commonNamespaceTypes = Types.InAssembly(typeof(Class1).Assembly)
                .That()
                .ResideInNamespaceStartingWith(nameof(Common));

            Assert.NotEmpty(webAppNamespaceTypes);
            Assert.NotEmpty(commonNamespaceTypes.GetTypes());
            var result = commonNamespaceTypes.Should()
                .NotHaveDependencyOnAny(webAppNamespaceTypes.ToArray())
                .GetResult()
                .IsSuccessful;
            Assert.True(result);
        }
    }
}
