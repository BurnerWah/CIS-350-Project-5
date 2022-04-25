using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] BossCovid boss;

    float scaleX_maxHealth = .975f;

    void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        float newScaleX = scaleX_maxHealth * boss.BossHealth / boss.BossMaxHealth;
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, 1);
    }
}
