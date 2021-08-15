using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;

    public void SetHealth(int health) {
        slider.value = health;
    }

    public void loseHealth(int health) {
        slider.value -= health;
    }

    public void recoverHealth(int health) {
        slider.value += health;
    }
}
