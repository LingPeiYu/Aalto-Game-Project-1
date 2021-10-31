using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicater : MonoBehaviour
{
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color hurtColor;
    [SerializeField]
    private Color recoverColor;
    [SerializeField]
    private float colorChangeSpeed;

    private Health health;

    private Health PlayerHealth
    {
        get
        {
            if(health==null) health = GameManager._instance.Player.GetComponent<Health>();
            return health;
        }
    }

    private Image healthBar;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        float healthRatio = PlayerHealth.CurrentHealthPoint / PlayerHealth.MaxHealthPoint;
        if (healthBar.fillAmount - healthRatio > 0.001)
        {
            healthBar.color = hurtColor;
        }
        else if (healthBar.fillAmount - healthRatio < -0.001)
        {
            healthBar.color = recoverColor;
        }

        healthBar.fillAmount = healthRatio;

        if (healthBar.fillAmount > 0.2)
            healthBar.color = Color.Lerp(healthBar.color, normalColor, colorChangeSpeed * Time.deltaTime);
        else
            healthBar.color = Color.Lerp(healthBar.color, hurtColor, colorChangeSpeed * Time.deltaTime);
    }
}
