namespace SharpMeasures;

/// <summary>Describes the behaviour of a conversion operator.</summary>
public enum ConversionOperatorBehaviour
{
    /// <summary>The <see cref="ConversionOperatorBehaviour"/> is unknown.</summary>
    Unknown,
    /// <summary>The conversion operation is implemented as an explicit operator.</summary>
    Explicit,
    /// <summary>The conversion operation is implemented as an implicit operator.</summary>
    Implicit
}
