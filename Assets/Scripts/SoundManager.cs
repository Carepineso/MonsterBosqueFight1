using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audio_arbusto;
    [SerializeField] private AudioClip[] audio_manzana;
    [SerializeField] private AudioClip[] audio_cactus;
    private AudioSource controlaudio;
    // Start is called before the first frame update
    void Awake()
    {
        controlaudio= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void AudioArbusto(int indice)
    {
        controlaudio.PlayOneShot(audio_arbusto[indice]);
    }

    public void AudioManzana(int indice)
    {
        controlaudio.PlayOneShot(audio_manzana[indice]);
    }
    public void AudioCactus(int indice)
    {
        controlaudio.PlayOneShot(audio_cactus[indice]);
    }
}
