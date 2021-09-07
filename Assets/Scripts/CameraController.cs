using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;

    float startZPosition;

    private void Start()
    {
        startZPosition = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = Vector2.Lerp(transform.position, target.position, smoothSpeed);
        desiredPos.z = startZPosition;
        transform.position = desiredPos;
    }
}
