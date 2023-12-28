using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartHealth : MonoBehaviour
{
    private SharedHealthManager sharedHealthManager;

    void Start()
    {
        sharedHealthManager = FindObjectOfType<SharedHealthManager>();
    }

    public void TakeDamage(float damage)
    {
        sharedHealthManager.TakeDamage(damage);
        // Example: Play hit animations, trigger special effects, etc.
    }
}
