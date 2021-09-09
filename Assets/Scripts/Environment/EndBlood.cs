using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlood : MonoBehaviour
{
    [SerializeField] GameObject bloodSplats;
    ParticleSystem particleSystem;
    private ParticleCollisionEvent[] CollisionEvents;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        
    }

    //public void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("sah");
    //    ParticleSystem ps = particleSystem;

    //    // particles
    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    //    // get
    //    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    //    // iterate
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        ParticleSystem.Particle p = enter[i];
    //        //p.startColor = new Color32(255, 0, 0, 255);
    //        Instantiate(bloodSplats, p.position, Quaternion.identity);
    //        enter[i] = p;
    //    }
    //}

    private void OnParticleCollision(GameObject other)
    {
        
    }

    private void OnParticleTrigger()
    {
        Debug.Log("sah");
        ParticleSystem ps = particleSystem;

            //Instantiate(bloodSplats, .position, Quaternion.identity);
    }
}
