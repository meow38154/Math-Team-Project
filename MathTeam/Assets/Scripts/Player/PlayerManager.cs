using GDH;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        [field: SerializeField] public PlayerMovement PlayerMovement {get; private set;}
    }
}