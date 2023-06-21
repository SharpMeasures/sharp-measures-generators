namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <inheritdoc/>
internal abstract class AArgumentRecorder : SharpAttributeParser.AArgumentRecorder
{
    /// <summary>The <see cref="Location"/> of the name of the attribute.</summary>
    public Location AttributeNameLocation { get; private set; } = Location.None;

    /// <summary>The <see cref="Location"/> of the entire attribute.</summary>
    public Location AttributeLocation { get; private set; } = Location.None;

    /// <summary>Records some <see cref="Location"/> related to the attribute, such as the <see cref="Location"/> of the name of the attribute.</summary>
    /// <param name="syntax">The <see cref="AttributeSyntax"/>, syntactically describing how the attribute was used.</param>
    public void RecordAttributeLocations(AttributeSyntax syntax)
    {
        AttributeNameLocation = syntax.Name.GetLocation();
        AttributeLocation = syntax.GetLocation();
    }
}
