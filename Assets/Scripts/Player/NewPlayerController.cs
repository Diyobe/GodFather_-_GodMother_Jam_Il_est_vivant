using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float lowJumpMultiplier;
    [SerializeField] float fallMultiplier;

    [SerializeField] float jumpHeight;
    bool isGrounded;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckDistance;

    float jumpRemember;
    float jumpRememberTime;
    float groundRemember;
    float groundRememberTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        jumpRemember -= Time.deltaTime;
        groundRemember -= Time.deltaTime;

        if (isGrounded)
        {
            groundRemember = groundRememberTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRemember = jumpRememberTime;
        }

        if(jumpRemember <= 0 && groundRemember <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            // Giving a bit of velocity on y axis to amplify gravity when falling
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * rb.gravityScale * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // Give a bit of velocity on the y axis to start amplifying the gravity when the player starts to jump and stop pressing the jump button
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * rb.gravityScale * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckDistance, groundLayerMask);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                return true;
            }
        }
        return false;
    }
}
