using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLoop2Script : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Frog")
        {
            collision.gameObject.transform.parent = transform;
            GameObject.FindGameObjectWithTag("Frog").GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
        }
        else
        {
          
        }
       
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Frog")
        {
            GameObject.FindGameObjectWithTag("Frog").GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            collision.gameObject.transform.parent = null;
        }
        else
        {
            
        }
    }

}
