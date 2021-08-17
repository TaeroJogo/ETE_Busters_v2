using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDCardsCounterController : MonoBehaviour
{

    Text idCardsCounter;
    void Start()
    {
        idCardsCounter = GameObject.Find("Canvas/Text").GetComponent<Text>();//pega o objeto de texto
        idCardsCounter.text = "100";//comeca com 100
    }

    public void UpdateIDCardsCounter(string amount)//atualiza o texto de quantidade de carteirinhas
    {
        idCardsCounter.text = amount;
    }
}
