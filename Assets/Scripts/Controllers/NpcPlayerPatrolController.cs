using Data.UnityObject;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class NpcPlayerPatrolController : MonoBehaviour
    {
        public List<Transform> _waypoints;
        private int _currentWaypointTarget;
        private float _patrolSpeed;
        private Coroutine _prevCoroutine;

        private void Awake()
        {
            _patrolSpeed = 2f;
            GetWaypoints();
        }

        private void Start()
        {
            _prevCoroutine = StartCoroutine(MovingToNextWaypoint());
        }

        private void GetWaypoints() => _waypoints = Resources.Load<CD_WaypointData>("Data/CD_WaypointData").waypoint.Waypoints;

        private IEnumerator MovingToNextWaypoint()
        {
            Transform wp = _waypoints[_currentWaypointTarget];
            transform.LookAt(wp.position);

            while (Vector3.Distance(transform.position, wp.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, _patrolSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = wp.position;
            yield return new WaitForSeconds(1);
            _currentWaypointTarget = (_currentWaypointTarget + 1) % _waypoints.Count;

            StopCoroutine(_prevCoroutine);
            _prevCoroutine = StartCoroutine(MovingToNextWaypoint());
        }


    }
}