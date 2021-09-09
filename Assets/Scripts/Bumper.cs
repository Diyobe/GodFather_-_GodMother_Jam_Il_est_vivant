using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [Range(1, 50)]
    [SerializeField] float height;
    [SerializeField] float triggerDuration = 1f;
    bool trigerred;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!trigerred) Bump(collision.transform.GetComponent<Rigidbody2D>());
        }
    }

    private void Bump(Rigidbody2D rb)
    {
        float jumpHeight = transform.position.y + height - transform.position.y;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2 * jumpHeight * -Physics2D.gravity.y * rb.gravityScale));
        StartCoroutine(UpdateTriggerBool());
    }

    IEnumerator UpdateTriggerBool()
    {
        trigerred = true;
        animator.SetBool("triggered", true);
        yield return new WaitForSeconds(triggerDuration);
        animator.SetBool("triggered", false);
        trigerred = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + Vector3.up * height, Vector3.one);
    }
}
