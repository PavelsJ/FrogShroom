using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLoop1Script : MonoBehaviour
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
            transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position,moveSpeed * Time.deltaTime);

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

    
}