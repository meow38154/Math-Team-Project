using UnityEngine;

namespace GDH
{
    [RequireComponent(typeof(NavMovement))]
    public class TriMovementModule : MonoBehaviour
    {
        private enum MovementState
        {
            Patrol,
            Chase,
            Investigate
        }

        [Header("Patrol")]
        [SerializeField] private WayPoints points;

        [SerializeField, Min(0f)]
        private float defaultSpeed = 3f;

        public void AddSpeed(float speed)
        {
            defaultSpeed += speed;
        }

        [Header("Sight")]
        [SerializeField] private Transform trackTarget;

        [Tooltip("적의 눈 위치. 비어 있으면 이 오브젝트의 Transform을 사용합니다.")]
        [SerializeField] private Transform sightOrigin;

        [SerializeField, Min(0f)]
        private float sightDistance = 10f;

        [SerializeField, Range(0f, 360f)]
        private float sightAngle = 90f;

        [Tooltip("Player Transform이 발밑에 있을 경우 몸통을 바라보게 하는 높이")]
        [SerializeField]
        private float targetHeightOffset = 1f;

        [Tooltip("Player와 Wall 레이어를 모두 포함해야 합니다.")]
        [SerializeField]
        private LayerMask sightMask;

        [Header("Investigation")]
        [Tooltip("마지막 발견 위치에 도착했다고 판단할 거리입니다.")]
        [SerializeField, Min(0f)]
        private float investigationArrivalDistance = 0.5f;

        private NavMovement _navMovement;

        private MovementState _movementState = MovementState.Patrol;

        private Vector3 _lastDetectedPosition;
        private bool _hasLastDetectedPosition;

        /*
         * SetDestination 직후에는 NavMovement.IsArrived가
         * 이전 목적지를 기준으로 true일 수 있으므로,
         * 실제 이동이 시작됐는지 확인합니다.
         */
        private bool _hasStartedInvestigationMovement;

        private float MovementMultiplier
        {
            get
            {
                if (MovementModifier.Instance == null)
                {
                    return 1f;
                }

                return MovementModifier.Instance.GetMovementModifier();
            }
        }

        private void Awake()
        {
            _navMovement = GetComponent<NavMovement>();

            if (sightOrigin == null)
            {
                sightOrigin = transform;
            }
        }

        private void Start()
        {
            StartPatrol();
        }

        private void Update()
        {
            bool canSeeTarget = CanSeeTarget();

            UpdateTrackingState(canSeeTarget);
            UpdateDestination();
            UpdateMovementSpeed();
        }

        private void UpdateTrackingState(bool canSeeTarget)
        {
            if (canSeeTarget)
            {
                UpdateLastDetectedPosition();

                if (_movementState != MovementState.Chase)
                {
                    StartChase();
                }

                return;
            }

            if (_movementState == MovementState.Chase)
            {
                StartInvestigation();
            }
        }

        private void UpdateLastDetectedPosition()
        {
            if (trackTarget == null)
            {
                return;
            }

            _lastDetectedPosition = trackTarget.position;
            _hasLastDetectedPosition = true;
        }

        private bool CanSeeTarget()
        {
            if (trackTarget == null)
            {
                return false;
            }

            Vector3 origin = sightOrigin.position;

            Vector3 targetPosition =
                trackTarget.position +
                Vector3.up * targetHeightOffset;

            Vector3 offset = targetPosition - origin;
            float sqrDistance = offset.sqrMagnitude;

            if (sqrDistance <= Mathf.Epsilon)
            {
                return true;
            }

            float sightDistanceSqr =
                sightDistance * sightDistance;

            if (sqrDistance > sightDistanceSqr)
            {
                return false;
            }

            float distance = Mathf.Sqrt(sqrDistance);
            Vector3 direction = offset / distance;

            if (!IsInsideSightAngle(direction))
            {
                return false;
            }

            bool hasHit = Physics.Raycast(
                origin,
                direction,
                out RaycastHit hit,
                distance,
                sightMask,
                QueryTriggerInteraction.Ignore
            );

            if (!hasHit)
            {
                return false;
            }

            return IsTrackTarget(hit.transform);
        }

        private bool IsTrackTarget(Transform hitTransform)
        {
            if (hitTransform == null || trackTarget == null)
            {
                return false;
            }

            if (hitTransform == trackTarget)
            {
                return true;
            }

            /*
             * 플레이어의 자식 Collider에 맞은 경우와
             * trackTarget이 플레이어의 자식인 경우를 모두 처리합니다.
             */
            return hitTransform.IsChildOf(trackTarget) ||
                   trackTarget.IsChildOf(hitTransform);
        }

        private bool IsInsideSightAngle(
            Vector3 targetDirection
        )
        {
            Vector3 flatForward = Vector3.ProjectOnPlane(
                sightOrigin.forward,
                Vector3.up
            );

            Vector3 flatTargetDirection =
                Vector3.ProjectOnPlane(
                    targetDirection,
                    Vector3.up
                );

            // 대상이 적의 바로 위나 아래에 있는 경우
            if (flatTargetDirection.sqrMagnitude <=
                Mathf.Epsilon)
            {
                return true;
            }

            if (flatForward.sqrMagnitude <= Mathf.Epsilon)
            {
                return false;
            }

            flatForward.Normalize();
            flatTargetDirection.Normalize();

            float halfSightAngle = sightAngle * 0.5f;

            float minimumDot = Mathf.Cos(
                halfSightAngle * Mathf.Deg2Rad
            );

            float targetDot = Vector3.Dot(
                flatForward,
                flatTargetDirection
            );

            return targetDot >= minimumDot;
        }

        private void UpdateDestination()
        {
            switch (_movementState)
            {
                case MovementState.Patrol:
                    UpdatePatrolDestination();
                    break;

                case MovementState.Chase:
                    UpdateChaseDestination();
                    break;

                case MovementState.Investigate:
                    UpdateInvestigationDestination();
                    break;
            }
        }

        private void UpdatePatrolDestination()
        {
            if (_navMovement.IsArrived)
            {
                SetRandomPatrolDestination();
            }
        }

        private void UpdateChaseDestination()
        {
            if (trackTarget == null)
            {
                StartInvestigation();
                return;
            }

            _navMovement.SetDestination(
                trackTarget.position
            );
        }

        private void UpdateInvestigationDestination()
        {
            if (!_hasLastDetectedPosition)
            {
                StartPatrol();
                return;
            }

            float sqrDistance =
                GetFlatSqrDistance(
                    transform.position,
                    _lastDetectedPosition
                );

            float arrivalDistanceSqr =
                investigationArrivalDistance *
                investigationArrivalDistance;

            /*
             * 마지막 발견 위치에 이미 충분히 가까우면
             * 바로 순찰로 돌아갑니다.
             */
            if (sqrDistance <= arrivalDistanceSqr)
            {
                StartPatrol();
                return;
            }

            /*
             * 한 번이라도 IsArrived가 false가 되어야
             * 새 목적지로 실제 이동이 시작된 것으로 판단합니다.
             */
            if (!_navMovement.IsArrived)
            {
                _hasStartedInvestigationMovement = true;
                return;
            }

            if (_hasStartedInvestigationMovement)
            {
                StartPatrol();
            }
        }

        private static float GetFlatSqrDistance(
            Vector3 first,
            Vector3 second
        )
        {
            first.y = 0f;
            second.y = 0f;

            return (first - second).sqrMagnitude;
        }

        private void UpdateMovementSpeed()
        {
            float multiplier = Mathf.Max(
                0.1f,
                MovementMultiplier
            );

            _navMovement.Speed =
                defaultSpeed * multiplier;
        }

        private void SetRandomPatrolDestination()
        {
            if (points == null)
            {
                Debug.LogWarning(
                    $"{name}: WayPoints가 설정되지 않았습니다.",
                    this
                );

                return;
            }

            int index =
                points.GetRandomDestinationIndex();

            WayPoint destination = points[index];

            _navMovement.SetDestination(
                destination.Position
            );
        }

        private void StartChase()
        {
            _movementState = MovementState.Chase;
            _hasStartedInvestigationMovement = false;

            Debug.Log(
                "플레이어 발견: 추격 시작",
                this
            );
        }

        private void StartInvestigation()
        {
            if (!_hasLastDetectedPosition)
            {
                StartPatrol();
                return;
            }

            _movementState = MovementState.Investigate;
            _hasStartedInvestigationMovement = false;

            _navMovement.SetDestination(
                _lastDetectedPosition
            );

            Debug.Log(
                $"플레이어 놓침: 마지막 발견 위치 " +
                $"{_lastDetectedPosition}로 이동",
                this
            );
        }

        private void StartPatrol()
        {
            _movementState = MovementState.Patrol;

            _hasLastDetectedPosition = false;
            _hasStartedInvestigationMovement = false;

            SetRandomPatrolDestination();

            Debug.Log(
                "순찰 시작",
                this
            );
        }

        [ContextMenu("Start Chase")]
        private void ForceStartChase()
        {
            if (trackTarget == null)
            {
                Debug.LogWarning(
                    $"{name}: Track Target이 없습니다.",
                    this
                );

                return;
            }

            UpdateLastDetectedPosition();
            StartChase();
        }

        [ContextMenu("Investigate Last Position")]
        private void ForceStartInvestigation()
        {
            if (trackTarget != null)
            {
                UpdateLastDetectedPosition();
            }

            StartInvestigation();
        }

        [ContextMenu("Start Patrol")]
        private void ForceStartPatrol()
        {
            StartPatrol();
        }

        private void OnDrawGizmosSelected()
        {
            Transform originTransform =
                sightOrigin != null
                    ? sightOrigin
                    : transform;

            Vector3 origin = originTransform.position;

            Vector3 flatForward = Vector3.ProjectOnPlane(
                originTransform.forward,
                Vector3.up
            );

            if (flatForward.sqrMagnitude >
                Mathf.Epsilon)
            {
                flatForward.Normalize();
            }

            Vector3 leftDirection =
                Quaternion.AngleAxis(
                    -sightAngle * 0.5f,
                    Vector3.up
                ) * flatForward;

            Vector3 rightDirection =
                Quaternion.AngleAxis(
                    sightAngle * 0.5f,
                    Vector3.up
                ) * flatForward;

            Gizmos.color = Color.yellow;

            Gizmos.DrawRay(
                origin,
                leftDirection * sightDistance
            );

            Gizmos.DrawRay(
                origin,
                rightDirection * sightDistance
            );

            if (trackTarget != null)
            {
                Vector3 targetPosition =
                    trackTarget.position +
                    Vector3.up * targetHeightOffset;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, targetPosition);
            }

            if (_hasLastDetectedPosition)
            {
                Gizmos.color = Color.cyan;

                Gizmos.DrawWireSphere(
                    _lastDetectedPosition,
                    investigationArrivalDistance
                );
            }
        }
    }
}