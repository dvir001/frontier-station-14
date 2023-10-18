using Content.Shared._NF.Ion.Systems;

namespace Content.Shared._NF.Ion.Components;

/// <summary>
///     Create circle pulse animation of radiation around object.
///     Drawn on client after creation only once per component lifetime.
/// </summary>
[RegisterComponent]
[Access(typeof(IonPulseSystem))]
public sealed partial class IonPulseComponent : Component
{
    /// <summary>
    ///     Timestamp when component was assigned to this entity.
    /// </summary>
    public TimeSpan StartTime;

    /// <summary>
    ///     How long will animation play in seconds.
    ///     Can be overridden by <see cref="Robust.Shared.Spawners.TimedDespawnComponent"/>.
    /// </summary>
    public float VisualDuration = 2f;

    /// <summary>
    ///     The range of animation.
    ///     Can be overridden by <see cref="RadiationSourceComponent"/>.
    /// </summary>
    public float VisualRange = 5f;
}
