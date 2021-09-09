using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField] Transform triggerPosition;
    [SerializeField] float triggerDistance = 1f;
    bool triggered;
    bool alreadykilled;

    [Space(10), SerializeField] float gravitiScaleWanted = 1f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        if (!triggered)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(triggerPosition.position, triggerDistance, Player.playerMask);
            if (colliders.Length > 0)
            {
                if (colliders[0].CompareTag("Player"))
                {
                    TriggerTrap();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !alreadykilled)
        {
            Player.instance.Die(false);
            alreadykilled = true;
        } 
    }

    private void OnDrawGizmos()
    {
        if (triggerPosition == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(triggerPosition.position, triggerDistance);

        Gizmos.color = new Color(0, 255, 0, 0.3f);
        Gizmos.DrawLine(transform.position, triggerPosition.position);
    }

    private void TriggerTrap()
    {
        triggered = true;
        rb.gravityScale = gravitiScaleWanted;
    }
}
