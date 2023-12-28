using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int pointIndex;

    void Start() 
    {
        transform.position = points[pointIndex].transform.position;
    }

    void Update() 
    {
        if(pointIndex <= points.Length -1)
        {
            Vector2 targetPosition = points[pointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position,moveSpeed * Time.deltaTime);

            Vector2 moveDirection = targetPosition - (Vector2)transform.position;
            Flip(moveDirection);

            if(transform.position == points[pointIndex].transform.position)
            {
                pointIndex += 1;
            }

            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }
        }
   
    }

    private void Flip(Vector2 direction)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = (direction.x > 0) ? 2.562f : -2.562f;
        transform.localScale = localScale;
    }
}
