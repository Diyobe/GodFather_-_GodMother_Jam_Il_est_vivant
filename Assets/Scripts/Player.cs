using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 lastVelocity;

    [SerializeField] private Vector3 lastCheckpointPos;


    [SerializeField] private GameObject[] bloodSplats;
    [SerializeField] private GameObject bloodParticle;

    [Tooltip("If the distance between player and last checkpoint bigger than this value --> set new checkpoint on current player position")]
    public float checkpointIntervalMax = 10f;

    [SerializeField] DeadBody deadBodyPrefab;
    private PlayerEntity playerEntity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerEntity = GetComponent<PlayerEntity>();
        lastCheckpointPos = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(lastCheckpointPos, transform.position) >= checkpointIntervalMax)
        {
            SetCheckpoint(transform.position);
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
            case "Death":
                SoundManager.Instance.PlaySpikeDeathSound();
                Die();
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Death")) Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkpointIntervalMax);
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

    private void Die()
    {
        //Debug.Log("Die");
        Instantiate(bloodSplats[Random.Range(0, bloodSplats.Length)], transform.position, Quaternion.identity);
        Instantiate(bloodParticle, transform.position, Quaternion.identity);

        // Tp to last checkpoint
        rb.velocity = Vector3.zero;
        rb.position = lastCheckpointPos;

        // Instantiate dead body
        DeadBody deadBody = Instantiate(deadBodyPrefab, transform.position, transform.rotation);
        deadBody.startVelocity = lastVelocity;
        deadBody.playerEntity = playerEntity;
    }
}
