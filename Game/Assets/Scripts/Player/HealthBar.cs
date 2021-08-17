using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;

    public GameObject playerObj;
    private Player player;
    private void Start()
    {
        player = playerObj.GetComponent<Player>();
    }

    void Update() {
        checkAlive();
    }

    void checkAlive() {
        if(slider.value <= 0) {
            player.EndGame(false);
        }
    }

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
