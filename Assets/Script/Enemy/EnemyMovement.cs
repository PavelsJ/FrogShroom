using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 60f;
    public float nextWaypointDistance = 1f;
    public float followRadius = 10f; 
    public Transform initialPosition; 

    public float specialAttackCooldown = 5f;
    public float specialAttackDistance = 10f;
    public float specialAttackSpeed = 10f; 
    public Animator animator;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool isFollowing = false;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        StartCoroutine(SpecialAttackRoutine());
    }

    private IEnumerator SpecialAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(specialAttackCooldown);

            if (isFollowing)
            {
                rb.velocity = Vector2.zero;

                animator.SetTrigger("Charge");

                yield return new WaitForSeconds(2f); 


                animator.SetTrigger("Dash");
                Vector2 specialAttackTarget = (Vector2)target.position;
                Vector2 direction = (specialAttackTarget - (Vector2)transform.position).normalized;
                rb.velocity = direction * specialAttackSpeed;

                float startTime = Time.time;
                while (Time.time - startTime < specialAttackDistance / specialAttackSpeed)
                {
                    yield return null;
                }
                rb.velocity = Vector2.zero;


                MoveTo(target.position);
            }
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone() && isFollowing)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void MoveTo(Vector3 position)
    {
        Vector2 direction = ((Vector2)position - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        seeker.StartPath(rb.position, position, OnPathComplete);
    }

    private void Update()
    {
        float distanceToTarget = Vector2.Distance(rb.position, target.position);

        if (distanceToTarget <= followRadius)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
            // Return to initial position
            if (Vector2.Distance(rb.position, initialPosition.position) > nextWaypointDistance)
            {
                MoveTo(initialPosition.position);
            }
            return;
        }

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        Flip(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void Flip(Vector2 direction)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = (direction.x > 0) ? 2f : -2f;
        transform.localScale = localScale;
    }
}

