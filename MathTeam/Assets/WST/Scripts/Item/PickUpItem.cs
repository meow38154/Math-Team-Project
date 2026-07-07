using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WST.Scripts.Item
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask hitLayer;
        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, rayDistance, hitLayer))
            {
                if (_hit.collider.TryGetComponent<IPickUpable>(out IPickUpable pickUpable))
                {
                    pickUpable.PickUp();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
        }
    }
}