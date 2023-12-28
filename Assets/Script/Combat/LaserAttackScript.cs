using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackScript : MonoBehaviour
{
    public GameObject laserObject;
    public Transform LaserCheck;
    public SpriteRenderer spriteRenderer;
    public LayerMask enemyLayers;

    public float damagePerSecond = 2f;
    private bool isAttacking = false;
    private float lastDamageTime;
    private Vector2 laserSize; 

    public float moveDistance = 0.5f;
    private Rigidbody2D rb;

    private bool cooldownTime = false; 

    private ManaBar manaBar;
    private Mana manaCost;
    private MoveBehaviourScript moveBehaviour;

    public Animator animator;
    private bool animationPlayed = false;
    [SerializeField] private AudioSource laserAttackSound;
    
    void Start()
    {
        laserObject.SetActive(false);
        lastDamageTime = Time.time;

        if (spriteRenderer != null)
        {
            laserSize = spriteRenderer.bounds.size; 
        }

        rb = GetComponent<Rigidbody2D>();
        manaBar = FindObjectOfType<ManaBar>();
        manaCost = manaBar.mana; 
        moveBehaviour = GetComponent<MoveBehaviourScript>();
    }

    void Update()
    {
        if (moveBehaviour.frogIsAlive)
        {
            if (Input.GetKey(KeyCode.E) && !cooldownTime)
            {
                if (manaCost.GetManaAmount() >= 20)
                {
                    laserObject.SetActive(true);
                    isAttacking = true;

                    if (isAttacking && Time.time - lastDamageTime >= 1.0f)
                    {
                        if(!animationPlayed)
                        {
                            animator.SetTrigger("LaserAttack");
                            laserAttackSound.Play();
                        }

                        DealDamage();
                        ApplyKnockback();
                        manaBar.mana.TrySpendMana(20);
                        moveBehaviour.SetLaserActive(true);
                        animationPlayed = true;
                    }
                }
                else
                {
                    Deactivate();
                }

            }
            else
            {
                Deactivate();
                animationPlayed = false;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                StartCoroutine(CooldownTime());
                moveBehaviour.SetLaserActive(false);
                animationPlayed = false;
                animator.SetTrigger("LaserStop");  
            }

        }
        
    }

    void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(LaserCheck.position, laserSize, 0f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerSecond);
            }

            BossPartHealth bossPartHealth = enemy.GetComponent<BossPartHealth>();
            if (bossPartHealth != null)
            {
                bossPartHealth.TakeDamage(damagePerSecond);
            }
        }

        lastDamageTime = Time.time;
    }

    IEnumerator CooldownTime()
    {
        cooldownTime = true;
        yield return new WaitForSeconds(1); 
        cooldownTime = false;
    }

    void ApplyKnockback()
    {
        Vector2 playerToLaserDirection = LaserCheck.position - transform.position;
        playerToLaserDirection.Normalize();
        rb.AddForce(-playerToLaserDirection * moveDistance, ForceMode2D.Impulse);
    
    }
    
    public void Deactivate()
    {
        laserObject.SetActive(false);
        isAttacking = false;
        moveBehaviour.SetLaserActive(false);
    }

}