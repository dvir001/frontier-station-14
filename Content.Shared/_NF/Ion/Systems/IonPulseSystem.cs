using Content.Shared._NF.Ion.Components;
using Robust.Shared.Spawners;
using Robust.Shared.Timing;

namespace Content.Shared._NF.Ion.Systems;

public sealed class IonPulseSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<IonPulseComponent, ComponentStartup>(OnStartup);
    }

    private void OnStartup(EntityUid uid, IonPulseComponent component, ComponentStartup args)
    {
        component.StartTime = _timing.RealTime;

        // try to get despawn time or keep default duration time
        if (TryComp<TimedDespawnComponent>(uid, out var despawn))
        {
            component.VisualDuration = despawn.Lifetime;
        }
        // try to get Ion range or keep default visual range
        if (TryComp<IonSourceComponent>(uid, out var radSource))
        {
            component.VisualRange = radSource.Intensity / radSource.Slope;
        }
    }
}
