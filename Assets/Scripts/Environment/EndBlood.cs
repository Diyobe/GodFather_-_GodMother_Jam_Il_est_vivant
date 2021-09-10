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
        CollisionEvents = new ParticleCollisionEvent[8];
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

    public void OnParticleCollision(GameObject other)
    {
        int collCount = particleSystem.GetSafeCollisionEventSize();

        if (collCount > CollisionEvents.Length)
            CollisionEvents = new ParticleCollisionEvent[collCount];

        int eventCount = particleSystem.GetCollisionEvents(other, CollisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            Instantiate(bloodSplats, CollisionEvents[i].intersection, Quaternion.identity);
            //TODO: Do your collision stuff here. 
            // You can access the CollisionEvent[i] to obtaion point of intersection, normals that kind of thing
            // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever
        }
    }

    private void OnParticleTrigger()
    {
        //Debug.Log("sah");
        //ParticleSystem ps = particleSystem;

        //// particles
        //List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> particleList = new List<ParticleSystem.Particle>();
        //List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        //// get
        //int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        //// iterate
        //for (int i = 0; i < numEnter; i++)
        //{
        //    ParticleSystem.Particle p = enter[i];
        //    p.startColor = new Color32(255, 0, 0, 255);
        //    enter[i] = p;
        //    Instantiate(bloodSplats, p.position, Quaternion.identity);
        //}
        //for (int i = 0; i < numExit; i++)
        //{
        //    ParticleSystem.Particle p = exit[i];
        //    p.startColor = new Color32(0, 255, 0, 255);
        //    exit[i] = p;
        //}

        //// set
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        //ParticlePhysicsExtensions.GetTriggerParticles(particleSystem,)
    }
}
