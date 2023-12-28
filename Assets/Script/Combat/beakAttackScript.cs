using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beakAttackScript : MonoBehaviour
{
    public Transform player;
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float attackHeight = -1f;
    [SerializeField] public float groundLevel = -6f;
    [SerializeField] public float beakDownSpeed = 5f;
    [SerializeField] public float beakUpSpeed = 2f;
    [SerializeField] public float center = 34f;

    private bool isMoving = false;
    private bool isInSecondPhase = false;

    [SerializeField] private Animator animator;
    [SerializeField] public CameraShakeScript camera;

    private Vector2 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        if (isMoving && player != null)
        {
            MoveTowardsPlayer();
        }

        if (isInSecondPhase)
        {
            AdditionalAttack();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector2 moveDirection = (player.position.x > transform.position.x) ? Vector2.right : Vector2.left;
        Flip(moveDirection);
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (!isInSecondPhase) // Check if not in the second phase
            {
                StopMovingBeak();

                yield return new WaitForSeconds(1f);

                yield return StartCoroutine(MoveBeakDown());

                yield return new WaitForSeconds(1f);

                yield return StartCoroutine(MoveBeakUp());

                StartMovingBeak();

                lastPlayerPosition = player.position;
            }
            else
            {
                // If in the second phase, you may want to do something different or nothing at all
            }
        }
    }

    void AdditionalAttack()
    {
        SharedHealthManager sharedHealthManager = GetComponentInParent<SharedHealthManager>();

        if (sharedHealthManager != null && sharedHealthManager.currentHealth <= 150)
        {
            Vector3 targetPosition = new Vector3(center, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator AdditionalAttackRoutine()
    {
        while (isInSecondPhase)
        {
            yield return new WaitForSeconds(5f);
            
            yield return StartCoroutine(MoveBeakDown());

            animator.SetBool("BossKluvAttack", true);
            StartCoroutine(camera.Shake(0.15f, 0.2f));
            
            yield return new WaitForSeconds(1f); 

            yield return StartCoroutine(MoveBeakUp());
        }
    }

    IEnumerator MoveBeakDown()
    {
        while (transform.position.y > groundLevel)
        {
            float newY = Mathf.MoveTowards(transform.position.y, groundLevel, beakDownSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    IEnumerator MoveBeakUp()
    {
        while (transform.position.y < attackHeight)
        {
            float newY = Mathf.MoveTowards(transform.position.y, attackHeight, beakUpSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    public void ActivateSecondPhase()
    {
        isInSecondPhase = true;
        StopMovingBeak();
        StartCoroutine(AdditionalAttackRoutine());
    }

    void StopMovingBeak()
    {
        isMoving = false;
    }

    public void StartMovingBeak()
    {
        isMoving = true;
    }

    public void StartRoutine()
    {
        StartCoroutine(AttackRoutine());
    }
    
    

    private void Flip(Vector2 direction)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = (direction.x > 0) ? -1.5858f : 1.5858f;
        transform.localScale = localScale;
    }
    
    
}
