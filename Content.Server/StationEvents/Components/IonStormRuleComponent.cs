using Content.Server.StationEvents.Events;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server.StationEvents.Components;

/// <summary>
/// 
/// </summary>
[RegisterComponent, Access(typeof(IonStormRule))]
public sealed partial class IonStormRuleComponent : Component
{
    [DataField("spawnerPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string SpawnerPrototype = "IonPulse";

    /// <summary>
    ///     Timestamp when component was assigned to this entity.
    /// </summary>
    public TimeSpan StartTime;

    /// <summary>
    ///     How long will animation play in seconds.
    ///     Can be overridden by <see cref="TimedDespawnComponent"/>.
    /// </summary>
    public float VisualDuration = 2f;

    /// <summary>
    ///     The range of animation.
    ///     Can be overridden by <see cref="RadiationSourceComponent"/>.
    /// </summary>
    public float VisualRange = 5f;

    // Event specific details
    public float TimeUntilPulse;
    public float MinPulseDelay = 0.3f;
    public float MaxPulseDelay = 0.9f;

    public int PulseCounter;
    public int MinimumPulses = 180;
    public int MaximumPulses = 540;

    public int MinArea = 500;
    public Box2 WorkingBounds;

    public int MaxFail = 1080;
    public int FailCounter;

}
