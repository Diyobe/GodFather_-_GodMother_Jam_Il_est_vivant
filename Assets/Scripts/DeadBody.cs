using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public Collider2D bodyCollider;
    public Rigidbody2D rb;
    [HideInInspector] public Vector2 startVelocity;

    [SerializeField] LayerMask ignoreLayerMask;
    [SerializeField] float overlapRange = 0.5f;

    [Space(10)]
    [SerializeField] Vector2 timeBeforeFallStopMinMax;

    [Space(10)]
    [SerializeField] GameObject bloodParticles;
    [SerializeField] GameObject[] bloodSplats;

    [HideInInspector] public bool stayImmobile;
    [HideInInspector] public bool canBePushed;
    [HideInInspector] public PlayerEntity playerEntity; // just used to get the max horizontal speed

    private void Start()
    {
        if(stayImmobile) StartCoroutine(DisableFallAfterTime());
        Debug.Log($"immboile = {stayImmobile}");

        Collider2D[] collidersFound = Physics2D.OverlapCircleAll(rb.position, overlapRange, ~ignoreLayerMask);
        if(collidersFound.Length > 0)
        {
            float shortestDistance = Vector3.Distance(collidersFound[0].ClosestPoint(rb.position), rb.position);
            int closestObjectIndex = 0;
            for(int i = 1; i < collidersFound.Length; i++)
            {
                Collider2D collider = collidersFound[i];
                if (collider.isTrigger) continue;

                float distance = Vector3.Distance(collider.ClosestPoint(rb.position), rb.position);
                if(distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestObjectIndex = i;
                }
            }

            Collider2D colliderToIgnore = collidersFound[closestObjectIndex];

            Physics2D.IgnoreCollision(colliderToIgnore, bodyCollider, true);
            Debug.Log($"{colliderToIgnore.name} collider ignored");

            // Set this collider as parent
            transform.SetParent(colliderToIgnore.transform);
        }

        rb.velocity = startVelocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(rb.position, overlapRange);
    }

    IEnumerator DisableFallAfterTime()
    {
        float t = Mathf.InverseLerp(playerEntity.maxSpeed, 0, startVelocity.x);
        float timeBeforeFallStop = Mathf.Lerp(timeBeforeFallStopMinMax.x, timeBeforeFallStopMinMax.y, t);
        Debug.Log($"startVelo = {startVelocity.x} // t = {t} // timeBeforeFallStop = {timeBeforeFallStop}");

        rb.gravityScale = 0;

        float timer = 0;
        while(timer < timeBeforeFallStop)
        {
            rb.velocity = Vector2.Lerp(startVelocity, Vector2.zero, Mathf.SmoothStep(0, timeBeforeFallStop, timer));
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SkewerIfPossible(Vector2 force)
    {
        if (canBePushed) StartCoroutine(Skewer(force));
    }
    IEnumerator Skewer(Vector2 force)
    {
        float duration = 0.5f;
        float timer = 0;

        Vector2 startPos = transform.position;
        Vector2 aimedPos = (Vector2)transform.position + force * duration;

        Instantiate(bloodSplats[Random.Range(0, bloodSplats.Length)], transform.position, transform.rotation);
        Instantiate(bloodParticles, transform.position, transform.rotation);

        while (timer < duration)
        {
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            transform.position = Vector3.Lerp(startPos, aimedPos, t);
            timer += Time.deltaTime;
            yield return null;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Collide with player");
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        }
    }
}
