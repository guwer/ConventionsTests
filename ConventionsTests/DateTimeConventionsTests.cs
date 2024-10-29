using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace ConventionsTests
{
    public class DateTimeConventionsTests
    {
        [Fact(DisplayName = "DateTime.Now Is Never Used")]
        public void DateTimeNow_IsNeverUsed()
        {
            var sourceFiles = ConventionsHelper.SourceFiles;

            Assert.NotEmpty(sourceFiles);
    
            var invocations = new List<string>();
    
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
                    .Select(m => $"{Path.GetFileName(file)} [{m.Expression.Parent.ToString()}]")
                    .ToList();
    
                invocations.AddRange(dateTimeNowInvocations);
            }
    
            Assert.Empty(invocations.GroupBy(x => x).Select(x => new { Source = x.Key, Count = x.Count()}));
        }
    }
}
