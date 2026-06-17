using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRaySensor : AbstractPlayerRay
    {
        protected override void RayInteraction(Transform target)
        {
            StartCoroutine(DoorOpenClose(target));
        }

        private IEnumerator DoorOpenClose(Transform target)
        {
            Quaternion closedRot = target.localRotation;
            Quaternion openRot = Quaternion.Euler(0f, 90f, 0f);

            target.localRotation = openRot;

            yield return new WaitForSeconds(4f);

            target.localRotation = closedRot;
        }
    }
}