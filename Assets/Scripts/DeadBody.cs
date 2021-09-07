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

    [HideInInspector] public PlayerEntity playerEntity; // just used to get the max horizontal speed

    private void Start()
    {
        StartCoroutine(DisableFallAfterTime());

        Collider2D colliderToIgnore = Physics2D.OverlapCircle(rb.position, overlapRange, ~ignoreLayerMask);
        if(colliderToIgnore != null)
        {
            Physics2D.IgnoreCollision(colliderToIgnore, bodyCollider, true);
            //Debug.Log($"{colliderToIgnore.name} collider ignored");

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

        float timer = 0;
        while(timer < timeBeforeFallStop)
        {
            rb.velocity = Vector2.Lerp(startVelocity, Vector2.zero, Mathf.SmoothStep(0, timeBeforeFallStop, timer));
            timer += Time.deltaTime;
            yield return null;
        }
        rb.gravityScale = 0;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }
}
