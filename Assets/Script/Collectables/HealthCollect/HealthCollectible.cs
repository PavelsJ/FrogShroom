using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Frog"))
        {
            other.GetComponent<Health>().AddHealth(healthValue);
            Destroy(gameObject); 
        }
    }
}
