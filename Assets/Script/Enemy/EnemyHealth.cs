using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float totalHealth = 9;
    public float currentHealth;

    private Rigidbody2D rb;

    public bool knocked;
    public float knockbackForce = 4f;
    public float knockbackDuration = 1f;
    public Transform damageSourcePosition;
    
    void Start()
    {
        currentHealth = totalHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            knocked = true;
            StartCoroutine(Knockback());
        }
    }

    private IEnumerator Knockback()
    {
        // Disable the enemy's movement temporarily during knockback

        Vector2 knockbackDirection = (transform.position - damageSourcePosition.position).normalized;
        rb.velocity = knockbackDirection * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector3.zero;
        knocked = false;
    }

    
   

   
}
