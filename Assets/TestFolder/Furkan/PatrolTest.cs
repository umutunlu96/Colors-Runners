using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class PatrolTest : MonoBehaviour
{
    public List<Transform> _waypoints;
    private int _currentWaypointTarget;
    private float _patrolSpeed;
    private Coroutine _prevCoroutine;

    private void Awake()
    {
        _patrolSpeed = 2f;
        _waypoints.AddRange(GameObject.FindGameObjectsWithTag("WayPoint").Select(x => x.transform));
    }

    private void Start()
    {
        _prevCoroutine = StartCoroutine(MovingToNextWaypoint());
    }

    private IEnumerator MovingToNextWaypoint()
    {
        Transform wp = _waypoints[_currentWaypointTarget];

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
