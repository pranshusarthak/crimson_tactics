using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    private GameObject player;
    private bool isMoving = false;
    private Queue<Vector3> path;
    private Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

    public void Initialize(GameObject player)
    {
        this.player = player;
        StartCoroutine(AutoMoveCoroutine());
    }

    public void Move()
    {
        if (!isMoving)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 enemyPosition = transform.position;

            path = FindPathToAdjacent(enemyPosition, playerPosition);
            if (path != null)
            {
                StartCoroutine(MoveAlongPath());
            }
        }
    }

    private Queue<Vector3> FindPathToAdjacent(Vector3 start, Vector3 playerPosition)
    {
        foreach (Vector3 dir in directions)
        {
            Vector3 nextPosition = playerPosition + dir;
            if (IsTileWalkable(nextPosition))
            {
                Queue<Vector3> path = new Queue<Vector3>();
                path.Enqueue(nextPosition);
                return path;
            }
        }

        return null;
    }

    private bool IsTileWalkable(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Obstacle"))
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator AutoMoveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (!isMoving)
            {
                Move();
            }
        }
    }

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;

        while (path.Count > 0)
        {
            Vector3 waypoint = path.Dequeue();

            while (Vector3.Distance(transform.position, waypoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint, Time.deltaTime * 2f);
                yield return null;
            }
        }

        isMoving = false;
    }
}
