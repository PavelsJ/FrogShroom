using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageCooldown = 1f;
    private bool canApplyDamage = true;
    public CameraShakeScript camera;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (canApplyDamage)
        {
            if (collision.tag == "Frog")
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                StartCoroutine(camera.Shake(0.15f, 0.2f));
            }
            else if (collision.tag == "Cuty")
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
{
    canApplyDamage = false;
    yield return new WaitForSeconds(damageCooldown);
    canApplyDamage = true;
}

    

    



    

}
