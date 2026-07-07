using GGMLib.ModuleSystem;
using UnityEngine;

namespace GDH
{
    public class MovementModule : ModuleOwner
    {
        private float _movementMult => MovementModifier.Instance.GetMovementModifier();
    }
}
