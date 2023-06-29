namespace SharpMeasures;

/// <summary>Determines whether a quantity implementing an operation also implements the mirrored operation.</summary>
public enum OperationMirrorMode
{
    /// <summary>The <see cref="OperationMirrorMode"/> is unknown.</summary>
    Unknown,
    /// <summary>The quantity also implements the mirrored operation.</summary>
    Enabled,
    /// <summary>The quantity does not implement the mirrored operation.</summary>
    Disabled,
    /// <summary>The quantity also implements the mirrored operation if the operation is commutative and the other quantity in the operation is defined in another assembly.</summary>
    /// <remarks>This reduces the likelihood that two separate quantities implement the same operator, causing ambiguous invokations.</remarks>
    Adaptive
}
