using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDCard : MonoBehaviour
{

    Text idCardsCounter;

    void Start()
    {
        idCardsCounter = GameObject.Find("Canvas/Text").GetComponent<Text>();
        idCardsCounter.text = PlayerPrefs.GetInt("IDCount").ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerF"))
        {
            idCardsCounter.text = (int.Parse(idCardsCounter.text) + 20).ToString();
            PlayerPrefs.SetInt("IDCount", int.Parse(idCardsCounter.text));
            Destroy(gameObject);
        }
    }
}
