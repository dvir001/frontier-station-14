using Robust.Shared.Audio;
using System.Numerics;
using Robust.Shared.Serialization;

namespace Content.Server._NF.Tiles;

/// <summary>
/// Applies flammable and damage while vaulting.
/// </summary>
[RegisterComponent, Access(typeof(UnevenTarainSystem))]
public sealed partial class UnevenTarainComponent : Component
{
    /// <summary>
    /// Path to the sound to be played when a mob slips.
    /// </summary>
    [DataField, AutoNetworkedField]
    [Access(Other = AccessPermissions.ReadWriteExecute)]
    public SoundSpecifier SlipSound = new SoundPathSpecifier("/Audio/Effects/slip.ogg");

    ///<summary>
    /// How high should they hop during the waddle? Higher hop = more energy.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Vector2 HopIntensity = new(0, 0.25f);

    /// <summary>
    /// How far should they rock backward and forward during the waddle?
    /// Each step will alternate between this being a positive and negative rotation. More rock = more scary.
    /// </summary>
    [DataField, AutoNetworkedField]
    public float TumbleIntensity = 20.0f;

    /// <summary>
    /// How long should a complete step take? Less time = more chaos.
    /// </summary>
    [DataField, AutoNetworkedField]
    public float AnimationLength = 0.66f;

    /// <summary>
    /// How much shorter should the animation be when running?
    /// </summary>
    [DataField, AutoNetworkedField]
    public float RunAnimationLengthMultiplier = 0.568f;
}
