using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] spikeDeath;
    [SerializeField] AudioSource respawnSound;

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySpikeDeathSound()
    {
        if(spikeDeath.Length > 0)
        spikeDeath[Random.Range(0, 4)].Play();
    }
    public void PlayRespawnSound()
    {
        if (respawnSound) respawnSound.Play();
    }
}
