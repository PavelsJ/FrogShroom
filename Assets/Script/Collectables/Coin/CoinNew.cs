using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinNew : MonoBehaviour
{
    public int value;  
    private bool coinCollected = false;
    [SerializeField] private AudioSource CoinSound;

    private void Start()
    {
        coinCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Frog"))
        {   
            coinCollected = true;
            CoinCounter.instance.IncreaseCoins(value);
            Destroy(gameObject); 
            if(coinCollected)
            {
                CoinSound.Play();
                StartCoroutine(CooldownTime());
            }
        }
        
    }

    IEnumerator CooldownTime()
    {
        yield return new WaitForSeconds(1); 
        coinCollected = false;
    }
}


