using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private AudioSource meleeAttackSound;
    private MoveBehaviourScript moveBehaviour;
    
    public Animator animator;
    public Transform attackCheck;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDamage = 3;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
    }
    
    void Update()
    {
        if (moveBehaviour.frogIsAlive)
        {
            if(Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    StartCoroutine(PerformAttack());
                }

            }
        }
    }

    IEnumerator PerformAttack()
    {
        
        nextAttackTime = Time.time + 1f / attackRate;
        Attack();
        yield return new WaitForSeconds(1); // Adjust this delay as needed
        
    }

    void Attack()
    {
        meleeAttackSound.Play();
        animator.SetTrigger("MeleeAttack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Attack: " + enemy.name);

            // Check if the hit object has the EnemyHealth script
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }

            // Check if the hit object has the BossPartHealth script
            BossPartHealth bossPartHealth = enemy.GetComponent<BossPartHealth>();
            if (bossPartHealth != null)
            {
                bossPartHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackCheck == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
    }
}
