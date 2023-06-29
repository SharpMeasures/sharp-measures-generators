namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit using instances of other units, according to a definition provided by a <see cref="UnitDerivationAttribute"/>.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string? PluralForm { get; }

    /// <summary>The ID of the intended derivation signature, as defined by a <see cref="UnitDerivationAttribute"/> - or <see langword="null"/> if there is exactly one derivation available.</summary>
    public string? DerivationID { get; }

    /// <summary>The names of the unit instances of other units from which this unit instance is derived. The order must match that of the derivation signature.</summary>
    public string[] UnitInstances { get; }

    /// <inheritdoc cref="DerivedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/></param>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public DerivedUnitInstanceAttribute(string name, string? pluralForm, string? derivationID, string[] unitInstances)
    {
        Name = name;
        PluralForm = pluralForm;
        DerivationID = derivationID;
        UnitInstances = unitInstances;
    }

    /// <summary><inheritdoc cref="DerivedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public DerivedUnitInstanceAttribute(string name, string? pluralForm, string[] unitInstances)
    {
        Name = name;
        PluralForm = pluralForm;
        UnitInstances = unitInstances;
    }

    /// <summary><inheritdoc cref="DerivedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstances"><inheritdoc cref="UnitInstances" path="/summary"/></param>
    public DerivedUnitInstanceAttribute(string name, string[] unitInstances)
    {
        Name = name;
        UnitInstances = unitInstances;
    }
}
