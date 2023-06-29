namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal interface ITestData<out TResult>
{
    public abstract AttributeData AttributeData { get; }
    public abstract AttributeSyntax AttributeSyntax { get; }

    public abstract TResult ExpectedResult { get; }
}
