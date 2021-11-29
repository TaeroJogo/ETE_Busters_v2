using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public void PlayTime()
    {
        Invoke("Play", 1);
    }

    public void QuitTime()
    {
        Invoke("Exit", 1);
    }

    public void Play()
    {
        GameObject.Find("Canvas").transform.Find("Title").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Quit Button").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Play Button").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("FPlayer").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("MPlayer").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("PlayerTitle").gameObject.SetActive(true);
    }

    public void MStart()
    {
        PlayerPrefs.SetString("Player", "M");
        SceneManager.LoadScene("Corredor 2");
    }

    public void FStart()
    {
        PlayerPrefs.SetString("Player", "F");
        SceneManager.LoadScene("Corredor 2");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
