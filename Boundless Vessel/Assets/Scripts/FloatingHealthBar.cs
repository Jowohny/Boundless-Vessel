using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentvalue, float maxValue)
    {
        slider.value = currentvalue / maxValue;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
