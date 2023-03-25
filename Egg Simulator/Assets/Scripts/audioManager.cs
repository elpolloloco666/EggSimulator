using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class audioManager : MonoBehaviour
{
    public AudioClip[] musicCollection;
    public AudioClip[] sfxCollection;
    public AudioSource musica;
    public AudioSource SFX;
    

    public void playEffect(int SFXIndex)
    {
        SFX.Stop();
        if(!SFX.isPlaying) SFX.PlayOneShot(sfxCollection[SFXIndex]);
    }
}
