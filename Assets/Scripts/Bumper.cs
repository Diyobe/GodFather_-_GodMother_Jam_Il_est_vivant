using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [Range(5f,50f)]
    [SerializeField] float power = 10f;
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
        rb.velocity = Mathf.Sqrt(2 * power * -Physics2D.gravity.y * rb.gravityScale) * (Vector2)transform.up;
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
        Vector2 playerHalfHeight = 0.9f * Vector2.up;
        Gizmos.color = Color.blue;
        for (float t = 0; t < 10; t+=0.25f)
        {

            Vector2 point = (Vector2)transform.position + (Vector2)transform.up * power * t + 0.5f * Physics2D.gravity * (t * t) + playerHalfHeight;
            Gizmos.DrawWireCube(point, Vector3.one);
        }
    }
}
