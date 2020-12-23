using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace ConventionsTests
{
    public class DateTimeConventionsTests
    {
        [Fact(DisplayName = "s")]
        public void DateTimeNow_IsNeverUsed()
        {
            var sourceFiles = ConventionsHelper.SourceFiles;

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
