using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource backgroundMusic;
    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip music;
    public AudioClip tapBook;
    public AudioClip Achievement;
    public AudioClip MoveShelf;
    public AudioClip OpenBook;
    public AudioClip Drinking;
    public AudioClip Cauldron;
    public AudioClip PotionFusion;

    public void Start()  
    {
        musicSource.clip = background;
        backgroundMusic.clip = music;
        musicSource.Play();
        backgroundMusic.Play();

    }
    public void PlaySound(AudioClip clip)
    {  
        sfxSource.PlayOneShot(clip);
    }
    public void StopSound()
    {
        sfxSource.Stop();
    }


}
