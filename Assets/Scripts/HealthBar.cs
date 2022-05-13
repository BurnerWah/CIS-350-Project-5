using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] BossCovid boss;

    float scaleX_maxHealth = .98f;

    void Start()
    {
        float newScaleX = (scaleX_maxHealth * (1));
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, 1);
    }

    public void UpdateHealth()
    {
        float newScaleX = (scaleX_maxHealth * ((float)boss.BossHealth / (float)boss.BossMaxHealth));
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, 1);
    }
}
