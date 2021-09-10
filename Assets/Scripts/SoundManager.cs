using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] spikeDeath;
    [SerializeField] AudioSource respawnSound;
    [SerializeField] AudioSource surprisedCrowdSound;

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
        if (spikeDeath.Length > 0)
            spikeDeath[Random.Range(0, spikeDeath.Length)].Play();
    }
    public void PlayRespawnSound()
    {
        if (respawnSound) respawnSound.Play();
    }
    public void PlaySurprisedCrowdSound()
    {
        if (surprisedCrowdSound)
        {
            surprisedCrowdSound.pitch = Random.Range(0.8f, 1.2f);
            StartCoroutine(DelayCrowdSound());
        }
    }

    IEnumerator DelayCrowdSound()
    {
        yield return new WaitForSeconds(0.3f);
        EnvironmentManager.Instance.PlayAllEnvironmentDeathAnim();
        surprisedCrowdSound.Play();
    }
}
