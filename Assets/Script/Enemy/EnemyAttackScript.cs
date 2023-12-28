using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    // private Animator anim;
    private Transform currentPoint;

    public float speed;
    public float distanceBetween;
    private float distance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        // anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            Flip();
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            Flip();
        }
        

        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < distanceBetween)
        { 
            MoveTowardsPlayer();
        }
        else
        {
            //ReturnToStartPosition();
        }

        
    }

    private void MoveTowardsPlayer()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = player.transform.position;
        // targetPosition.y = currentPosition.y;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, 3f * Time.deltaTime);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

   
}
