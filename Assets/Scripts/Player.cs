using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private Vector3 lastCheckpointPos;

    [Tooltip("If the distance between player and last checkpoint bigger than this value --> set new checkpoint on current player position")]
    public float checkpointIntervalMax = 10f;

    [SerializeField] GameObject deadBodyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Vector3.Distance(lastCheckpointPos, transform.position) >= checkpointIntervalMax)
        {
            SetCheckpoint(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Checkpoint":
                SetCheckpoint(collision.transform);
                break;
            case "Death":
                Die();
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
        Debug.Log("Force new checkpoint because of distance");
        lastCheckpointPos = newCheckpointPos;
    }

    private void Die()
    {
        Debug.Log("Die");

        // Tp to last checkpoint
        rb.velocity = Vector3.zero;
        rb.position = lastCheckpointPos;

        // Instantiate dead body
        Instantiate(deadBodyPrefab, transform);
    }
}
