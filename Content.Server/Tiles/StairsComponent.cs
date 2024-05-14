using Robust.Shared.Audio;

namespace Content.Server.Tiles;

/// <summary>
/// Applies flammable and damage while vaulting.
/// </summary>
[RegisterComponent, Access(typeof(StairsSystem))]
public sealed partial class StairsComponent : Component
{
    /// <summary>
    /// Path to the sound to be played when a mob slips.
    /// </summary>
    [DataField, AutoNetworkedField]
    [Access(Other = AccessPermissions.ReadWriteExecute)]
    public SoundSpecifier SlipSound = new SoundPathSpecifier("/Audio/Effects/slip.ogg");
}
