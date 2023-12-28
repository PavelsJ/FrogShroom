using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public void SetMaxHealth(float maxHealth)
    {
        fillImage.fillAmount = 1f;
    }

    public void SetHealth(float health, float maxHealth)
    {
        float fillAmount = (float)health / maxHealth;
        fillImage.fillAmount = fillAmount;
    }
}
