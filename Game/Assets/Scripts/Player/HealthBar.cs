using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;

    public void SetHealth(int health) { //seta a vida do jogador
        slider.value = health;
    }

    public void loseHealth(int health) {//diminui a vida do jogador
        slider.value -= health;
    }

    public void recoverHealth(int health) { //aumenta a vida
        slider.value += health;
    }
}
