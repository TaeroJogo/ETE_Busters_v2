using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{

    public GameObject videoPlayer;
    public GameObject videoPlayerF;

    void Start()
    {
        if (PlayerPrefs.GetString("Player") == "M")
        {
            Destroy(videoPlayerF);
        }
        else
        {
            Destroy(videoPlayer);
        }
        Invoke("EndGame", 19);
    }

    void EndGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
