using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace ConventionsTests
{
    public class DateTimeConventions
    {
        [Fact]
        public void DateTimeNow_IsNeverUsed()
        {
            var sourceFiles = ConventionsHelper.GetSourceFiles();

            foreach (var file in sourceFiles)
            {
                var content = File.ReadAllText(file);

                var tree = CSharpSyntaxTree.ParseText(content);
                var root = tree.GetRoot();
                var memberAccesses = root.DescendantNodes()
                    .OfType<MemberAccessExpressionSyntax>()
                    .ToList();

                var dateTimeNowInvocations = memberAccesses
                    .Where(m => m.Expression.Parent.ToString().Equals("datetime.now", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Assert.Empty(dateTimeNowInvocations);
            }
        }
    }
}