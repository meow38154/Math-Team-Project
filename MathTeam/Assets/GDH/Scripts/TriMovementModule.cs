using GGMLib.ModuleSystem;
using UnityEngine;
using UnityEngine.AI;

namespace GDH
{
    public class TriMovementModule : MonoBehaviour
    {
        [SerializeField] private WayPoints points;
        [SerializeField] private float defaultSpeed;

        [Header("TEMP")]
        [SerializeField] private GameObject trackTarget;
        private NavMovement _navMovement;
        private bool _isChasing;
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
        }
        private void Update()
        {
            if(_isChasing)   // TEMPORARY
            {
                _navMovement.SetDestination(trackTarget.transform.position);
            }
            else if(_navMovement.IsArrived)
            {
                int idx = points.GetRandomDestinationIndex();
                WayPoint startPoint = points[idx];
                _navMovement.SetDestination(startPoint.Position);
            }
            _navMovement.Speed = defaultSpeed * _movementMult;
        }

        [ContextMenu("Chase")]
        private void Chase()    // TEMPORARY
        {
            _isChasing = true;
        }
    }
}
