using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRaySensor : MonoBehaviour
    {
        [SerializeField] private LayerMask rayMask;
        private void Update()
        {
            if (Physics.Raycast(transform.parent.position, transform.parent.forward * 10, out RaycastHit hit, 100, rayMask))
            {
                Debug.Log(hit.transform.name);
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    StartCoroutine(DoorOpenClose(hit.transform));
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.parent.position, transform.parent.forward * 10);
        }

        private IEnumerator DoorOpenClose(Transform target)
        {
            target.transform.rotation = Quaternion.Euler(0, 90, 0);
            yield return new WaitForSeconds(4f);
            target.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}