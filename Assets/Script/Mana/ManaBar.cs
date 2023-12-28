using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image currentManaBar;
    public Mana mana;
    public LaserAttackScript laserAttackScript;

    private void Awake()
    {
        currentManaBar = transform.Find("ManaBarCurrent").GetComponent<Image>();
        
        mana = new Mana();
    }

    private void Update()
    {
        mana.Update();
        currentManaBar.fillAmount = mana.GetManaNormalized();
    }

}


public class Mana 
{
    public const int manaMax = 100;

    public float manaAmount;
    private float manaRegenAmount;

    public Mana()
    {
        manaAmount = 0;
        manaRegenAmount = 10f;
    }

    public void Update()
    {
        manaAmount += manaRegenAmount * Time.deltaTime;
        manaAmount = Mathf.Clamp(manaAmount, 0f, manaMax);
    }

    public bool TrySpendMana(int amount) 
    {
        if(manaAmount >= amount)
        {
            manaAmount -= amount;
            return true;
        }
        return false;

    }

    public float GetManaNormalized()
    {
        return manaAmount / manaMax;
    }

    public float GetManaAmount()
    {
        return manaAmount;
    }
}

