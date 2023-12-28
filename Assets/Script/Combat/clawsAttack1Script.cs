using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clawsAttack1Script : MonoBehaviour
{
    private Vector2 initialPosition;
    [SerializeField] public float liftHeight = 3f; 
    [SerializeField] public float liftSpeed = 2f;  
    [SerializeField] public float movementSpeed = 1f; 
    private GameObject player;

    public Animator animatorClawaKoka;

    void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindWithTag("Frog"); 
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            Flip(directionToPlayer);

            while (transform.position.y < initialPosition.y + liftHeight)
            {
                animatorClawaKoka.SetTrigger("ClawUp");
                transform.Translate(Vector2.up * liftSpeed * Time.deltaTime);
                MoveTowardsPlayer(directionToPlayer);
                yield return null;
            }

            yield return new WaitForSeconds(0f);

            while (transform.position.y > initialPosition.y)
            {
                animatorClawaKoka.SetTrigger("ClawDown");
                transform.Translate(Vector2.down * liftSpeed * Time.deltaTime);
                MoveTowardsPlayer(directionToPlayer);
                yield return null;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    void MoveTowardsPlayer(Vector2 direction)
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    private void Flip(Vector2 direction)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = (direction.x > 0) ? -2.163599f : 2.163599f;
        transform.localScale = localScale;
    }

    public void StartMovingClaws1()
    {
       StartCoroutine(MoveObject());
    }
}