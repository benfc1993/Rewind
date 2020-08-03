using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
       slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float MaxHealth)
    {
        slider.maxValue = MaxHealth;
    }
    public void SetHealth(float Health)
    {
        slider.value = Health;
    }
}
