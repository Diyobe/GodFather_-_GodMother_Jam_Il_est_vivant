using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake() => instance = this;

    Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    Vector2 lastVelocity;

    [SerializeField] private Checkpoint lastCheckpointPos;


    [SerializeField] private GameObject[] bloodSplats;
    [SerializeField] private GameObject bloodParticle;

    //[Tooltip("If the distance between player and last checkpoint bigger than this value --> set new checkpoint on current player position")]
    //public float checkpointIntervalMax = 10f;

    [SerializeField] DeadBody deadBodyPrefab;
    private PlayerEntity playerEntity;

    [SerializeField] float deadBodyPushForce = 1f;
    private Transform lastDeadBodyPushed;

    [Space(10), SerializeField, InspectorName("Player mask")] LayerMask playerMaskSezrializationHelper;
    public static LayerMask playerMask;

    CameraZoom camZoom;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerEntity = GetComponent<PlayerEntity>();

        playerMask = playerMaskSezrializationHelper;

        camZoom = Camera.main.GetComponent<CameraZoom>();
    }

    private void Update()
    {
        if (PlayerController.JumpInput)
        {
            lastDeadBodyPushed = null;
        }
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Checkpoint":
                SetCheckpoint(collision.transform);
                break;
            case "Boulder":
                Die(true);
                break;
            case "Spikes":
                Die(true, true);
                break;
            case "Death":
                Die(false);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Dead body":
                if (collision.transform != lastDeadBodyPushed)
                {
                    DeadBody deadBody = collision.transform.parent.GetComponent<DeadBody>();
                    deadBody.SkewerIfPossible((Vector2)(collision.transform.position - transform.position).normalized * deadBodyPushForce);
                    lastDeadBodyPushed = collision.transform;
                }
                break;
            case "Boulder":
                Die(true);
                break;
            case "Spikes":
                Die(true, true);
                break;
            case "Death":
                Die(false);
                break;
        }
    }

    private void SetCheckpoint(Transform checkpointTransform)
    {
        //Debug.Log("Passed checkpoint");
        Checkpoint checkpoint;
        if (checkpointTransform.TryGetComponent<Checkpoint>(out checkpoint))
        {
            if (checkpoint != lastCheckpointPos)
            {
                lastCheckpointPos = checkpoint;
            }
        }
        else Debug.LogError("Checkpoint component not found");
    }

    public void Die(bool immobileDeadBody, bool deadBodyCanBePushed = false)
    {
        if(SoundManager.Instance != null)
        SoundManager.Instance.PlaySpikeDeathSound();

        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySurprisedCrowdSound();
        }

        //Debug.Log("Die");
        if (bloodSplats.Length > 0) nstantiate(bloodSplats[Random.Range(0, bloodSplats.Length)], transform.position, Quaternion.identity);
        if (bloodParticle) Instantiate(bloodParticle, transform.position, Quaternion.identity);

        camZoom.StartRespawn(lastCheckpointPos.respawnPoint.gameObject);

        SpawnDeadBody(immobileDeadBody, deadBodyCanBePushed);

        // Use last checkpoint
        if (lastCheckpointPos != null)
        lastCheckpointPos.UseCheckpoint(rb);
    }
    private void SpawnDeadBody(bool immobile, bool canBePushed)
    {
        // Instantiate dead body
        DeadBody deadBody = Instantiate(deadBodyPrefab, transform.position, transform.rotation);
        deadBody.stayImmobile = immobile;
        deadBody.canBePushed = canBePushed;
        deadBody.startVelocity = lastVelocity;
        deadBody.playerEntity = playerEntity;
    }
}
