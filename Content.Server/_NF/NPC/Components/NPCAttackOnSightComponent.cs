using Content.Shared.Damage;
using Content.Shared.Mobs.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Server._NF.NPC.Components
{
    [RegisterComponent]
    //[NetworkedComponent]
    [AutoGenerateComponentState]
    public sealed partial class NPCAttackOnSightComponent : Component
    {
    }
}
