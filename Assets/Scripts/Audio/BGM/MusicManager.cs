using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip bgm1;
    public AudioClip bgm2;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBGM1();
    }

    void PlayBGM1()
    {
        audioSource.clip = bgm1;
        audioSource.Play();
        Invoke("PlayBGM2", bgm1.length);
    }

    void PlayBGM2()
    {
        audioSource.clip = bgm2;
        audioSource.Play();
        Invoke("PlayBGM1", bgm2.length);
    }

}
