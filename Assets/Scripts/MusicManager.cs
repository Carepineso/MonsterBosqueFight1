using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicaFondo;
    public AudioClip[] pistas;
    private int trackIndex = 0;

    void Start()
    {
        if(musicaFondo != null && pistas.Length > 0)
        {
            PlayMusic(trackIndex);
        }
    }

    public void CambiarMusica()
    {
        if(musicaFondo != null && pistas.Length > 0)
        {
            trackIndex = (trackIndex + 1) % pistas.Length;
            PlayMusic(trackIndex);
        }
    }

    private void PlayMusic(int indexTrack)
    {
        musicaFondo.Stop();
        musicaFondo.clip = pistas[indexTrack];
        musicaFondo.Play();
    }
}
