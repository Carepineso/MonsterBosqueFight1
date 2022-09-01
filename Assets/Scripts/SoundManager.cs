using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audios;
    private AudioSource control;

    void Awake(){
        control= GetComponent<AudioSource>();
    }

    public void SeleccionAudio(int indice){
        control.PlayOneShot(audios[indice]);
    }
}
