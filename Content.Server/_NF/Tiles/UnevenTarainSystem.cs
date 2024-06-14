using Content.Server.Atmos.Components;
using Content.Server.Atmos.EntitySystems;
using Content.Server.Stunnable;
using Content.Shared.Buckle.Components;
using Content.Shared.Interaction.Components;
using Content.Shared.StatusEffect;
using Content.Shared.StepTrigger.Systems;
using Content.Shared.Buckle;
using Content.Shared.Slippery;
using Content.Shared.Stunnable;
using Robust.Shared.Physics.Components;
using Content.Shared.Administration.Logs;
using Content.Shared.Database;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Physics.Systems;
using Content.Shared.Clothing;
using Content.Shared.Clothing.Components;
using Content.Shared.Movement.Components;
using Content.Shared.Inventory.Events;

namespace Content.Server._NF.Tiles;

public sealed class UnevenTarainSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<UnevenTarainComponent, StepTriggeredOffEvent>(OnUnevenTarainStepTriggered);
        SubscribeLocalEvent<UnevenTarainComponent, StepTriggerAttemptEvent>(OnUnevenTarainStepTriggerAttempt);
    }

    [Dependency] private readonly ISharedAdminLogManager _adminLogger = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedStunSystem _stun = default!;
    [Dependency] private readonly StatusEffectsSystem _statusEffects = default!;
    [Dependency] private readonly SharedPhysicsSystem _physics = default!;
    [Dependency] private readonly SharedBuckleSystem _buckle = default!;
    [Dependency] private readonly SlipperySystem _slip = default!;

    private void OnUnevenTarainStepTriggerAttempt(EntityUid uid, UnevenTarainComponent component, ref StepTriggerAttemptEvent args)
    {
        //if (HasComp<ClumsyComponent>(args.Tripper))
        //    args.Continue = true;

        if (!TryComp<StrapComponent>(args.Tripper, out var strap))
            return;

        if (strap.BuckledEntities == null)
            return;

        args.Continue = true;
    }

    private void OnUnevenTarainStepTriggered(EntityUid uid, UnevenTarainComponent component, ref StepTriggeredOffEvent args)
    {
        var otherUid = args.Tripper;

        if (TryComp<StrapComponent>(otherUid, out var strap))
        {
            if (strap.BuckledEntities == null)
                return;

            foreach (var buckleUid in strap.BuckledEntities)
            {
                _buckle.TryUnbuckle(buckleUid, buckleUid, true);

                if (HasComp<KnockedDownComponent>(buckleUid))
                    return;

                var attemptEv = new SlipAttemptEvent();
                RaiseLocalEvent(buckleUid, attemptEv);
                if (attemptEv.Cancelled)
                    return;

                var attemptCausingEv = new SlipCausingAttemptEvent();
                RaiseLocalEvent(uid, ref attemptCausingEv);
                if (attemptCausingEv.Cancelled)
                    return;

                var ev = new SlipEvent(buckleUid);
                RaiseLocalEvent(uid, ref ev);

                if (TryComp(buckleUid, out PhysicsComponent? physics) && !HasComp<SlidingComponent>(buckleUid))
                {
                    _physics.SetLinearVelocity(buckleUid, physics.LinearVelocity * 5f, body: physics);
                }

                var playSound = !_statusEffects.HasStatusEffect(buckleUid, "KnockedDown");

                _stun.TryParalyze(buckleUid, TimeSpan.FromSeconds(3f), true);

                // Preventing from playing the slip sound when you are already knocked down.
                if (playSound)
                {
                    _audio.PlayPredicted(component.SlipSound, buckleUid, buckleUid);
                }

                _adminLogger.Add(LogType.Slip, LogImpact.Low,
                    $"{ToPrettyString(buckleUid):mob} slipped on collision with {ToPrettyString(uid):entity}");
            }
        }
    }
}
