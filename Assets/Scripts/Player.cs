using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake() => instance = this;

    Rigidbody2D rb;
    Vector2 lastVelocity;

    [SerializeField] private Vector3 lastCheckpointPos;


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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerEntity = GetComponent<PlayerEntity>();
        lastCheckpointPos = transform.position;

        playerMask = playerMaskSezrializationHelper;
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

    private void SetCheckpoint(Transform checkpoint)
    {
        //Debug.Log("Passed checkpoint");
        if (checkpoint.position != lastCheckpointPos)
        {
            lastCheckpointPos = checkpoint.position;
        }
    }
    private void SetCheckpoint(Vector3 newCheckpointPos)
    {
        //Debug.Log("Force new checkpoint because of distance");
        lastCheckpointPos = newCheckpointPos;
    }

    public void Die(bool immobileDeadBody, bool deadBodyCanBePushed = false)
    {
        SoundManager.Instance.PlaySpikeDeathSound();

        //Debug.Log("Die");
        if (bloodSplats.Length > 0) Instantiate(bloodSplats[Random.Range(0, bloodSplats.Length)], transform.position, Quaternion.identity);
        if (bloodParticle) Instantiate(bloodParticle, transform.position, Quaternion.identity);

        // Tp to last checkpoint
        rb.velocity = Vector3.zero;
        rb.position = lastCheckpointPos;

        SpawnDeadBody(immobileDeadBody, deadBodyCanBePushed);
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
