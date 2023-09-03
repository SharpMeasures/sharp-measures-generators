namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using System;
using System.Collections.Generic;

internal static class VerifyOneOfSyntax
{
    public static void Verify(OneOf<None, ExpressionSyntax> syntax)
    {
        if (syntax.IsT1 && syntax.AsT1 is null)
        {
            throw new ArgumentException($"The provided {nameof(ExpressionSyntax)} was null.");
        }
    }

    public static void Verify(OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> syntax)
    {
        if (syntax.IsT0 && syntax.AsT0 is null)
        {
            throw new ArgumentException($"The provided {nameof(ExpressionSyntax)} was null.");
        }

        if (syntax.IsT1 && syntax.AsT1 is null)
        {
            throw new ArgumentException($"The provided {nameof(IReadOnlyList<object>)} was null.");
        }
    }
}
