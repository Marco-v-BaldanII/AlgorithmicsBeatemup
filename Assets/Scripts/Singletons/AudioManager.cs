using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] private AudioDictionary musicDictionary;
    [SerializeField] private AudioDictionary sfxDictionary;

    // Single music source, more than 1 songs can't play at the same time
    [SerializeField] private AudioSource musicSource;
    // List of multiple sfx sources so multiple can play at the same time
    [SerializeField] private GameObject sfxSourcesParent;
    private List<AudioSource> sfxSources = new List<AudioSource>();

    // Enforce singleton pattern
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (sfxSourcesParent != null)
        {
            // Add all audio sources under the parent to the list
            sfxSources.AddRange(sfxSourcesParent.GetComponentsInChildren<AudioSource>());
        }
    }

    // public function that can be called from anywhere to change music
    public void PlayMusic(string musicName)
    {
        if (musicDictionary[musicName] != null)
        {
            musicSource.clip = musicDictionary[musicName];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // public function that can be called from anywhere to play sfx
    public void PlaySfx(string sfxName)
    {
        if (sfxDictionary[sfxName] != null)
        {

            AudioSource availableSource = sfxSources.Find(source => !source.isPlaying);
            if (availableSource != null)
            {
                availableSource.clip = sfxDictionary[sfxName];
                availableSource.Play();
            }
            else
            {
                // If no available audio source force play on first one
                availableSource = sfxSources[0];
                availableSource.clip = sfxDictionary[sfxName];
                availableSource.Play();
            }

        }
    }

}