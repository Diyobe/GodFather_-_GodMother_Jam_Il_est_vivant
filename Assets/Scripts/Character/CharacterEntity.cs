using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour {
    [SerializeField] float maxSpeed;
    [SerializeField] float timeBeforeMaxSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float ratioComeBack;

    private Rigidbody2D rb;

    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }


    private void Move() {
        Vector2 currentSpeed = rb.velocity;
        currentSpeed += Mathf.Sign(currentSpeed.x) == Mathf.Sign(direction.x) ? direction * Time.fixedDeltaTime * maxSpeed/timeBeforeMaxSpeed : direction * Time.fixedDeltaTime * maxSpeed / timeBeforeMaxSpeed * ratioComeBack;
        currentSpeed.x = Mathf.Clamp(currentSpeed.x, -maxSpeed, maxSpeed);
        rb.velocity = currentSpeed;
    }

    private void Jump() {

    }
}
