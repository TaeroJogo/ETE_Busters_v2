using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;


    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);//quando tiver com o mouse em cima do botao ele vai tocar o hoverFx
    }
    
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);//quando clicar no botao ele vai tocar o clickFx
    }
}
