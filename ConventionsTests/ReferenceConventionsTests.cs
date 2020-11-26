﻿using System.Linq;
using Common;
using FluentAssertions;
using WebApp.Controllers;
using Xunit;

namespace ConventionsTests
{
    public class ReferenceConventionsTests
    {
        [Fact(DisplayName = "s")]
        public void AssemblyADoesNotReferenceAssemblyB()
        {
            var refernecedAssemblies = typeof(Class1).Assembly.GetReferencedAssemblies();
            var result = refernecedAssemblies
                .Select(a => a.Name)
                .Contains(typeof(HomeController).Assembly.GetName().Name);

            Assert.NotEmpty(refernecedAssemblies);
            Assert.False(result);
        }

        [Fact(DisplayName = "a")]
        public void AssemblyXDoesNotReferenceAssemblyY()
        {
            // With FluentAssertions
            typeof(Class1).Assembly.Should().NotReference(typeof(HomeController).Assembly);
        }
    }
}