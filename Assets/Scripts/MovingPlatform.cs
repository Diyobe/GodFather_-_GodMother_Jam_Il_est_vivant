using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] points;
    int nextPointIndex = 1;
    [SerializeField] float speed = 5f;
    [SerializeField] float distanceForNextPoint = 0.5f;

    Rigidbody2D rb;
    Rigidbody2D playerRb;
    float startGravityScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = points[0].position;
        nextPointIndex = 1;

        playerRb = Player.instance.GetComponent<Rigidbody2D>();
        startGravityScale = playerRb.gravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("touched");
            playerRb.gravityScale = 0;
            playerRb.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            playerRb.gravityScale = startGravityScale;
            playerRb.transform.SetParent(null);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, points[nextPointIndex].position) <= distanceForNextPoint)
        {
            nextPointIndex = (nextPointIndex + 1) % points.Length;
        }
        rb.velocity = (points[nextPointIndex].position - transform.position).normalized * speed;
    }

    private void OnDrawGizmosSelected()
    {
        if (points.Length == 0) return;
        foreach (Transform point in points)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, distanceForNextPoint);
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawLine(point.position, transform.position);
        }
    }
}
