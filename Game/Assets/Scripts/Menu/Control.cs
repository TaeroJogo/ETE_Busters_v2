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
        SceneManager.LoadScene("Corredor 1");//carrega a cena do corredor
    }

    public void Exit()
    {
        Application.Quit();//fecha o jogo
    }
}
