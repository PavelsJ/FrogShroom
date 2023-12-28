using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioSource HealthLoseSound;
    [SerializeField] private AudioSource HealthGainSound;
    [SerializeField] private AudioSource DeathSound;
    public LogicScript logic;
    public float currentHealth { get; private set;}
    private bool dead;
    

    private Rigidbody2D rb;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic"). GetComponent<LogicScript>();
        LoadPlayerHealth();

        rb = GetComponent<Rigidbody2D>();

        
    }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        
        if (currentHealth <= 0)
        { 
            DeathSound.Play();
            logic.gameOver();
            GetComponent <MoveBehaviourScript>(). frogIsAlive = false;     
        }
        else
        {     
            HealthLoseSound.Play();
        }

        SavePlayerHealth();

    }

    public void AddHealth(float _value)
    {
        HealthGainSound.Play();
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        SavePlayerHealth();
    }

    private void SavePlayerHealth()
    {
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        PlayerPrefs.Save();
    }

    private void LoadPlayerHealth()
    {
        currentHealth = PlayerPrefs.GetFloat("PlayerHealth", currentHealth);
    }

}
