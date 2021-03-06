using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using WebApp.Controllers;
using Xunit;

namespace ConventionsTests
{
    public class StructureConventionsTests
    {
        [Fact(DisplayName ="test")]
        public void TypesInControllersNamespace_InheritFromController()
        {
            var controllers = Types.InAssembly(typeof(HomeController).Assembly)
                .That()
                .AreClasses()
                .And()
                .ResideInNamespaceContaining("Controllers");

            var result = controllers
                .Should()
                .Inherit(typeof(Controller))
                .GetResult();

            Assert.NotEmpty(controllers.GetTypes());
            Assert.True(result.IsSuccessful);
        }

        [Fact(DisplayName = "test")]
        public void TypesInHandlersNamespace_ImplementIHandler()
        {
        }
    }
}