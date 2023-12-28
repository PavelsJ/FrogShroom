using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedHealthManager : MonoBehaviour
{
    public float maxHealth = 300f;  
    public float currentHealth;

    public BossHealthBar healthBar;
    public beakAttackScript beakAttack;
    [SerializeField] OpenTheDoorScript doorScript;

    private bool isInSecondPhase = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 150 && !isInSecondPhase)
        {
            StartSecondPhase();
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        healthBar.SetHealth(currentHealth, maxHealth);


        if (currentHealth <= 0)
        {
            Die();
            if (doorScript != null)
            {
                doorScript.OpenDoor();
            }
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }

    void StartSecondPhase()
    {
        isInSecondPhase = true;
        Debug.Log("Boss entered second phase!");

        if (beakAttack != null)
        {
            beakAttack.ActivateSecondPhase();
        }
        else
        {
            Debug.LogError("beakAttack component not found!");
        }
    }
}

