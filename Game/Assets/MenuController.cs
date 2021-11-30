using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject menu;

    private bool menuOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOpen = !menuOpen;
            menu.SetActive(menuOpen);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            menuOpen = !menuOpen;
            menu.SetActive(menuOpen);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
