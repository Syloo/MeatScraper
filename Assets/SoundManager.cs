using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string soundName;
    public AudioClip audioClip;
    [Range(0,1)]
    public float volume = 0.7f;

    [Range(0, .5f)]
    public float randomVolume = 0.2f;

    [Range(.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0, .2f)]
    public float randomPitch = .1f;

  


    private AudioSource source;

    public void SetSource(AudioSource _source)
    {

        source = _source;
        source.clip = audioClip;

    }

    public void Play()
    {

        source.volume = volume *(1 + UnityEngine.Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + UnityEngine.Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
        

    }

}

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;



    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("more than one audio manager");
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject GO = new GameObject("Sound_" + i + "_" + sounds[i].soundName);
            GO.transform.SetParent(this.transform);
            sounds[i].SetSource(GO.AddComponent<AudioSource>());
        }



    }

    public void PlaySound(string _name)
    {

        for(int i = 0; i<sounds.Length; i++)
        {

            if(sounds[i].soundName == _name)
            {

                sounds[i].Play();
                return;

            }

        }

        Debug.LogWarning("AudioManager: Sound not found!");


    }

    internal static void PlaySound()
    {
        throw new NotImplementedException();
    }
}
