using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Slider slider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerF"))
        {
            slider.value = PlayerPrefs.GetInt("Health") + 15;
            if (slider.value > 100)
            {
                PlayerPrefs.SetInt("Health", 100);
            }
            else
            {
                PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 15);
            }

            Destroy(gameObject);
        }
    }
}
