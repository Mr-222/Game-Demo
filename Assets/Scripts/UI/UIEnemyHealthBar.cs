using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBar : MonoBehaviour
{
    Slider slider;
    float hidden_wait = 0;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        hidden_wait = 3f;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void Update()
    {
        hidden_wait = hidden_wait - Time.deltaTime;
        if(slider != null)
        {
            if (hidden_wait <= 0)
            {
                hidden_wait = 0;
                slider.gameObject.SetActive(false);
            }
            else
            {
                if (!slider.gameObject.activeInHierarchy)
                {
                    slider.gameObject.SetActive(true);
                }
            }

            if (slider.value < 0)
            {
                Destroy(slider.gameObject);
            }
        }
        
    }


}
