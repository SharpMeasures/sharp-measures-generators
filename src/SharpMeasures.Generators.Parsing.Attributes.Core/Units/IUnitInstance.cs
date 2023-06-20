namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed attribute that describes an instance of a unit.</summary>
public interface IUnitInstance
{
    /// <summary>The <see cref="string"/> name of the unit instance.</summary>
    public abstract string? Name { get; }

    /// <summary>The <see cref="string"/> plural form name of the unit instance. If not provided, the unaltered singular form is used.</summary>
    public abstract string? PluralForm { get; }
}
