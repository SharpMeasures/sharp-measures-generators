﻿namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

public static class ExpressionSyntaxFactory
{
    private static ExpressionSyntax Syntax { get; } = SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);

    public static ExpressionSyntax Create() => Syntax;
    public static IReadOnlyList<ExpressionSyntax> CreateCollection() => new[] { Create() };
}
