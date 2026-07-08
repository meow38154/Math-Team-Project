using GGMLib.ModuleSystem;
using UnityEngine;
using UnityEngine.AI;

namespace GDH
{
    public class TriMovementModule : MonoBehaviour
    {
        [SerializeField] private WayPoints points;
        [SerializeField] private float defaultSpeed;
        private NavMovement _navMovement;
        private float _movementMult => MovementModifier.Instance.GetMovementModifier();

        private void Awake()
        {
            _navMovement = GetComponent<NavMovement>();
        }
        private void Start()
        {
            int idx = points.GetRandomDestinationIndex();
            WayPoint startPoint = points[idx];
            _navMovement.SetDestination(startPoint.Position);
            Debug.Log("ShouldMove");
        }
        private void Update()
        {
            if(_navMovement.IsArrived)
            {
                int idx = points.GetRandomDestinationIndex();
                WayPoint startPoint = points[idx];
                _navMovement.SetDestination(startPoint.Position);
                Debug.Log("ShouldMove");
            }
            _navMovement.Speed = defaultSpeed * _movementMult;
        }
    }
}
