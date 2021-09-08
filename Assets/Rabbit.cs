using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
    public float speed;
    public float jumpForce;

    public bool isStartingToTheRight;

     public int underIsStillGrounded;
     public int frontIsStillGrounded;

    public Vector2 direction;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (isStartingToTheRight) {
            direction = new Vector2(1, 0);
        } else {
            direction = new Vector2(-1, 0);
        }
    }

    void FixedUpdate() {
        Move();
        DoINeedJump();
    }

    void Jump() {
        rb.AddForce(transform.up * jumpForce);
    }

    void DoINeedJump() {
        if (underIsStillGrounded != 0 && frontIsStillGrounded == 0) {
            Jump();
        }
    }

    private void Move() {
        if (direction.x != 0) {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        rb.velocity = new Vector2(speed * direction.x, rb.velocity.y);
    }
}
