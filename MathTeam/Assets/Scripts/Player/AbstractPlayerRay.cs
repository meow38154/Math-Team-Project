using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public abstract class AbstractPlayerRay : MonoBehaviour
    {
        [SerializeField] private LayerMask rayMask;
        [SerializeField] private float rayDistance = 3f;
        
        protected virtual void Update()
        {
            Ray ray = new Ray(transform.position, transform.forward);

            if (!Physics.Raycast(ray, out RaycastHit hit, rayDistance, rayMask)) return;
            Debug.Log(hit.transform.name);

            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                RayInteraction(hit.transform);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
        }

        protected abstract void RayInteraction(Transform target);
    }
}