/* Robert Krawczyk, Gerard Lamoureux
 * Mission Operation Bigfish
 * Text and damage gradient
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text healthText, scoreText;
    SpriteRenderer damageGradient;

    float timeDamageTaken;
    readonly float damageGradientDuration = .5f, damageGradientAlphaScalar = .5f;
    bool damageGradientOn = false;

    void Start()
    {
        damageGradient = GameObject.Find("damage gradient").GetComponent<SpriteRenderer>();
        damageGradient.gameObject.SetActive(false);
        if(healthText != null)
            healthText.text = $"Health: {GameManager.Instance.humanHealth}";
        if(scoreText != null)
            scoreText.text = $"Score: {GameManager.Instance.score}";
    }

    // UI when killing a covid cell (called by Missile)
    public void ScoreUI()
    {
        if(scoreText != null)
            scoreText.text = $"Score: {GameManager.Instance.score}";
    }

    // UI when taking damage (called by other DestroyOffscreen and Missile)
    public void DamageUI()
    {
        if(healthText != null)
            healthText.text = $"Health: {GameManager.Instance.humanHealth}";
        damageGradient.gameObject.SetActive(true);
        timeDamageTaken = Time.time;
        damageGradientOn = true;
    }

    
    private void FixedUpdate()
    {
        // Make gradient fade away
        if (damageGradientOn)
        {
            float newOpacity = 1 - ((Time.time - timeDamageTaken) / damageGradientDuration);
            if (newOpacity <= 0)
            {
                damageGradientOn = false;
                damageGradient.gameObject.SetActive(false);
                print("Turned off damage gradient");
            }
            else
            {
                print($"New opacity {newOpacity}");
                SetDamageGradientOpacity(Mathf.Clamp01(newOpacity));
            }
            
        }
        
    }

    // helpful
    void SetDamageGradientOpacity(float alpha)
    {
        damageGradient.color = new Color(damageGradient.color.r, damageGradient.color.g, damageGradient.color.b, alpha);
    }
}
