using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource stepSource;

    [Header("Die Sounds")]
    public AudioClip[] playerAudioDie;

    [Header("Damage Sounds")]
    public AudioClip[] playerAudioDamage;

    [Header("Step Sounds")]
    public AudioClip[] playerAudioStep;

    [Header("Shoot Sounds")]
    public AudioClip[] playerAudioShoot;

    // Start is called before the first frame update
    void Start()
    {
       stepSource = GetComponentInChildren<AudioSource>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage()
    {
        int randomClipID = Random.Range(0, playerAudioDamage.Length);
        audioSource.PlayOneShot(playerAudioDamage[randomClipID]);       
    }

    public void Die()
    {
        int randomClipID = Random.Range(0, playerAudioDie.Length);
        audioSource.PlayOneShot(playerAudioDie[randomClipID]);
    }

}
