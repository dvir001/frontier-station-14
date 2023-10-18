namespace Content.Shared._NF.Ion.Components;

/// <summary>
///     Irradiate all objects in range.
/// </summary>
[RegisterComponent]
public sealed partial class IonSourceComponent : Component
{
    /// <summary>
    ///     Radiation intensity in center of the source in rads per second.
    ///     From there radiation rays will travel over distance and loose intensity
    ///     when hit radiation blocker.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    [DataField("intensity")]
    public float Intensity = 1;

    /// <summary>
    ///     Defines how fast radiation rays will loose intensity
    ///     over distance. The bigger the value, the shorter range
    ///     of radiation source will be.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    [DataField("slope")]
    public float Slope = 0.5f;
}
