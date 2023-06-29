namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class TestData
{
    public static ITestData<TResult> Create<TResult>(AttributeData attributeData, AttributeSyntax attributeSyntax, TResult expectedResult) => new Implementation<TResult>(attributeData, attributeSyntax, expectedResult);

    private sealed class Implementation<TResult> : ITestData<TResult>
    {
        public AttributeData AttributeData { get; }
        public AttributeSyntax AttributeSyntax { get; }
        public TResult ExpectedResult { get; }

        public Implementation(AttributeData attributeData, AttributeSyntax attributeSyntax, TResult expectedResult)
        {
            AttributeData = attributeData;
            AttributeSyntax = attributeSyntax;
            ExpectedResult = expectedResult;
        }
    }
}
