using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TMP_Text coinText;
    public int currentCoins = 0;
   
    [SerializeField] private AudioSource startSound;

    void Awake()
    {
        instance = this;
        startSound.Play();
    }

    void Start()
    {
        LoadPlayerCoin();
        coinText.text = "Coins: " + currentCoins.ToString();   
    }

    public void IncreaseCoins(int value)
    {
        currentCoins += value;
        coinText.text = "Coins: " + currentCoins.ToString();
        SavePlayerCoin();
    }

     private void SavePlayerCoin()
    {
        PlayerPrefs.SetInt("PlayerCoin", currentCoins);
        PlayerPrefs.Save();
    }

    private void LoadPlayerCoin()
    {
        currentCoins = PlayerPrefs.GetInt("PlayerCoin", currentCoins);
    }

    

 
}
