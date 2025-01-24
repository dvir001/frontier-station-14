using Content.Shared.DoAfter;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._NF.NFSprayPainter.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class NFSprayPainterComponent : Component
{
    [DataField]
    public SoundSpecifier SpraySound = new SoundPathSpecifier("/Audio/Effects/spray2.ogg");

    [DataField]
    public TimeSpan PipeSprayTime = TimeSpan.FromSeconds(1);

    [DataField, AutoNetworkedField]
    public string? PickedColor;

    [DataField]
    public Dictionary<string, Color> ColorPalette = new();

    [DataField, AutoNetworkedField]
    public Dictionary<string, int> Indexes = new();

    [DataField]
    public Dictionary<string, DoAfterId> DoAfters = new();
}
