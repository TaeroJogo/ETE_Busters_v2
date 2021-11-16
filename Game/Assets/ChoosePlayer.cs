using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetString("Player") == "M") {
            Destroy(GameObject.Find("PlayerF").gameObject);
        } else {
            Destroy(GameObject.Find("Player").gameObject);
        }
    }
}
