using Content.Shared._NF.Movement.Components;
using Content.Shared.Buckle.Components;
using Content.Shared.Gravity;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Events;
using Content.Shared.Movement.Systems;
using Content.Shared.Standing;
using Content.Shared.Stunnable;
using Robust.Shared.Timing;

namespace Content.Shared._NF.Movement.Systems;

public abstract class SharedWaddleAnimationStrapSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;

    public override void Initialize()
    {
        // Startup
        SubscribeLocalEvent<WaddleAnimationStrapComponent, ComponentStartup>(OnComponentStartup);

        // Start moving possibilities
        SubscribeLocalEvent<WaddleAnimationStrapComponent, MoveInputEvent>(OnMovementInput);
        SubscribeLocalEvent<WaddleAnimationStrapComponent, StoodEvent>(OnStood);

        // Stop moving possibilities
        SubscribeLocalEvent((Entity<WaddleAnimationStrapComponent> ent, ref StunnedEvent _) => StopWaddling(ent));
        SubscribeLocalEvent((Entity<WaddleAnimationStrapComponent> ent, ref DownedEvent _) => StopWaddling(ent));
        SubscribeLocalEvent((Entity<WaddleAnimationStrapComponent> ent, ref BuckleChangeEvent _) => StopWaddling(ent));
        SubscribeLocalEvent<WaddleAnimationStrapComponent, GravityChangedEvent>(OnGravityChanged);
    }

    private void OnGravityChanged(Entity<WaddleAnimationStrapComponent> ent, ref GravityChangedEvent args)
    {
        if (!args.HasGravity && ent.Comp.IsCurrentlyWaddling)
            StopWaddling(ent);
    }

    private void OnComponentStartup(Entity<WaddleAnimationStrapComponent> entity, ref ComponentStartup args)
    {
        if (!TryComp<InputMoverComponent>(entity.Owner, out var moverComponent))
            return;

        // If the waddler is currently moving, make them start waddling
        if ((moverComponent.HeldMoveButtons & MoveButtons.AnyDirection) == MoveButtons.AnyDirection)
        {
            RaiseNetworkEvent(new StartedWaddlingStrapEvent(GetNetEntity(entity.Owner)));
        }
    }

    private void OnMovementInput(Entity<WaddleAnimationStrapComponent> entity, ref MoveInputEvent args)
    {
        // Prediction mitigation. Prediction means that MoveInputEvents are spammed repeatedly, even though you'd assume
        // they're once-only for the user actually doing something. As such do nothing if we're just repeating this FoR.
        if (!_timing.IsFirstTimePredicted)
        {
            return;
        }

        if (!args.HasDirectionalMovement && entity.Comp.IsCurrentlyWaddling)
        {
            StopWaddling(entity);

            return;
        }

        // Only start waddling if we're not currently AND we're actually moving.
        if (entity.Comp.IsCurrentlyWaddling || !args.HasDirectionalMovement)
            return;

        entity.Comp.IsCurrentlyWaddling = true;

        RaiseNetworkEvent(new StartedWaddlingStrapEvent(GetNetEntity(entity.Owner)));
    }

    private void OnStood(Entity<WaddleAnimationStrapComponent> entity, ref StoodEvent args)
    {
        // Prediction mitigation. Prediction means that MoveInputEvents are spammed repeatedly, even though you'd assume
        // they're once-only for the user actually doing something. As such do nothing if we're just repeating this FoR.
        if (!_timing.IsFirstTimePredicted)
        {
            return;
        }

        if (!TryComp<InputMoverComponent>(entity.Owner, out var mover))
        {
            return;
        }

        if ((mover.HeldMoveButtons & MoveButtons.AnyDirection) == MoveButtons.None)
            return;

        if (entity.Comp.IsCurrentlyWaddling)
            return;

        entity.Comp.IsCurrentlyWaddling = true;

        RaiseNetworkEvent(new StartedWaddlingStrapEvent(GetNetEntity(entity.Owner)));
    }

    private void StopWaddling(Entity<WaddleAnimationStrapComponent> entity)
    {
        entity.Comp.IsCurrentlyWaddling = false;

        RaiseNetworkEvent(new StoppedWaddlingStrapEvent(GetNetEntity(entity.Owner)));
    }
}
