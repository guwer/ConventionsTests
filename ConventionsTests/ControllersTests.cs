using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using WebApp.Controllers;
using Xunit;

namespace ConventionsTests
{
    public class ControllersTests
    {
        [Fact]
        public void Test()
        {
            var types = Types.InAssembly(typeof(HomeController).Assembly)
                .That()
                .ResideInNamespaceContaining("Controllers");

            var result = types
                .Should()
                .Inherit(typeof(Controller))
                .GetResult();

            Assert.NotEmpty(types.GetTypes());
            Assert.True(result.IsSuccessful);
        }
    }
}