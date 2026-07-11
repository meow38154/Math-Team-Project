using UnityEngine;

namespace GDH
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField] private WayPoint[] wayPoints;

        public WayPoint this[int index] => wayPoints[index];

        public int GetRandomDestinationIndex() => Random.Range(0, wayPoints.Length);
    }
}
