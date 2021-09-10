using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] spikeDeath;
    [SerializeField] AudioSource[] crushedDeath;
    [SerializeField] AudioSource[] ambientBirds;
    [SerializeField] AudioSource respawnSound;
    [SerializeField] AudioSource surprisedCrowdSound;
    [SerializeField] AudioSource stompCorpseSound;

    float ambientTimer = 4f;

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

    private void Update()
    {
        if (ambientTimer > 0)
            ambientTimer -= Time.deltaTime;
        else
            PlayBirdSounds();
    }

    public void PlaySpikeDeathSound()
    {
        if (spikeDeath.Length > 0)
            spikeDeath[Random.Range(0, spikeDeath.Length)].Play();
    }

    public void PlayCrushedDeathSound()
    {
        if (crushedDeath.Length > 0)
            crushedDeath[Random.Range(0, crushedDeath.Length)].Play();
    }

    public void PlayRespawnSound()
    {
        if (respawnSound) respawnSound.Play();
    }

    public void PlayStompCorpseSound()
    {
        if (stompCorpseSound)
        {
            stompCorpseSound.pitch = Random.Range(0.8f, 1.2f);
            stompCorpseSound.Play();
        }
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

    void PlayBirdSounds()
    {
        if (ambientBirds.Length > 0)
        {
            ambientBirds[Random.Range(0, ambientBirds.Length)].Play();
            ambientTimer = Random.Range(7f, 10f);
        }
    }
}
