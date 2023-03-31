using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class audioManager : MonoBehaviour
{
    public AudioClip[] musicCollection;
    public AudioClip[] sfxCollection;
    public AudioSource musica;
    public AudioSource EnemySfx;
    public AudioSource PlayerSfx;
    public AudioSource LevelSfx;
    public AudioSource UISfx;

    public void playEnemyEffect(int SFXIndex)
    {
        EnemySfx.Stop();
        if(!EnemySfx.isPlaying) EnemySfx.PlayOneShot(sfxCollection[SFXIndex]);
    }

    public void playPlayerEffect(int SFXIndex)
    {
        PlayerSfx.Stop();
        if (!PlayerSfx.isPlaying) PlayerSfx.PlayOneShot(sfxCollection[SFXIndex]);
    }

    public void playLevelEffect(int SFXIndex)
    {
        LevelSfx.Stop();
        if (!LevelSfx.isPlaying) LevelSfx.PlayOneShot(sfxCollection[SFXIndex]);
    }


}
