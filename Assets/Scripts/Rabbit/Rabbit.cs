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

    [SerializeField] Sprite pictureCute;
    [SerializeField] Sprite normalPicture;
    [SerializeField] GameObject deadBody;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sp;

    Rigidbody2D rb;

    [SerializeField] private GameObject[] bloodSplats;
    [SerializeField] private GameObject bloodParticle;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (isStartingToTheRight) {
            direction = new Vector2(1, 0);
        } else {
            direction = new Vector2(-1, 0);
        }
    }

    void FixedUpdate() {
        isInTheAir();
        Move();
        DoINeedJump();
    }

    void Jump() {
        rb.AddForce(transform.up * jumpForce);
        sp.sprite = pictureCute;
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Death") {
            Dead();
        }
    }

    void Dead() {
        Instantiate(bloodSplats[Random.Range(0, bloodSplats.Length)], transform.position, Quaternion.identity);
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        GameObject corpse = Instantiate(deadBody);
        corpse.transform.position = transform.position;
        corpse.transform.position = new Vector3(corpse.transform.position.x, corpse.transform.position.y - 1.5f, corpse.transform.position.z);
        Destroy(gameObject);
    }

    void isInTheAir() {
        if (underIsStillGrounded == 0 && frontIsStillGrounded == 0) {
            anim.SetBool("Jump", true);
        } else {
            anim.SetBool("Jump", false);
            sp.sprite = normalPicture;
        }
    }
}
