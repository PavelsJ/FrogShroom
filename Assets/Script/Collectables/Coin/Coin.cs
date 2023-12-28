using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int value;
    private string uniqueKey;

    void Start()
    {
        uniqueKey = "IsObjectDestroyed_" + gameObject.name; 
        LoadSavedCoin(uniqueKey);
    }

    private void LoadSavedCoin(string uniqueKey)
    {
        bool isObjectDestroyed = PlayerPrefs.GetInt(uniqueKey, 0) == 1;

        if (isObjectDestroyed)
        {
            Destroy(gameObject); 
            Debug.Log("Coin Destroyed");
        }
        else
        {
            Debug.Log("Coin Not Destroyed");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Frog"))
        {  
            PlayerPrefs.SetInt(uniqueKey, 1);
            PlayerPrefs.Save();

            LoadSavedCoin(uniqueKey);
            CoinCounter.instance.IncreaseCoins(value);
        }
    }  
}
