using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public abstract class AbstractPlayerRay : MonoBehaviour
    {
        [Header("Ray")]
        [SerializeField, Min(0f)]
        private float rayDistance = 3f;

        [Tooltip("상호작용할 오브젝트의 레이어")]
        [SerializeField]
        private LayerMask interactionMask;

        [Tooltip("Ray를 가로막을 벽이나 장애물 레이어")]
        [SerializeField]
        private LayerMask obstacleMask;

        protected virtual void Update()
        {
            if (Mouse.current == null ||
                !Mouse.current.leftButton.wasPressedThisFrame)
            {
                return;
            }

            Ray ray = new Ray(
                transform.position,
                transform.forward
            );

            int raycastMask =
                interactionMask.value |
                obstacleMask.value;

            if (!Physics.Raycast(
                    ray,
                    out RaycastHit hit,
                    rayDistance,
                    raycastMask,
                    QueryTriggerInteraction.Ignore))
            {
                return;
            }

            // 가장 먼저 맞은 오브젝트가 상호작용 레이어인지 검사
            if (!IsInLayerMask(
                    hit.transform.gameObject.layer,
                    interactionMask))
            {
                // 벽이나 장애물이 먼저 맞았으므로 상호작용하지 않음
                return;
            }

            RayInteraction(hit.transform);
        }

        private static bool IsInLayerMask(
            int layer,
            LayerMask layerMask)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(
                transform.position,
                transform.forward * rayDistance
            );
        }

        protected abstract void RayInteraction(
            Transform target);
    }
}