using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingBoulder : MonoBehaviour
{
    [SerializeField] Vector2 yRotationMinMax;
    [SerializeField] float timePerSwing = 0.5f;
    //[SerializeField] Rigidbody2D deathBoulder;
    //[SerializeField] float deathBoulderTorqueForce;

    [SerializeField] bool startFromRight;
    //bool lastGoingLeft;
    bool goingLeft;

    //bool readyToGo;
    //bool going;

    private void Start()
    {
        goingLeft = startFromRight;
        //lastGoingLeft = goingLeft;

        //if (startFromRight) deathBoulderTorqueForce = -deathBoulderTorqueForce;
    }

    //private void Update()
    //{
    //    if (!readyToGo)
    //    {
    //        if(deathBoulder.transform.childCount >= 2)
    //        {
    //            readyToGo = true;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        //if (!going)
        //{
            bool temp = (int)(Time.time / timePerSwing) % 2 == 0;
            goingLeft = temp ^ !startFromRight;

            //// It means it just changed
            //if(goingLeft == lastGoingLeft)
            //{
            //    lastGoingLeft = !goingLeft;

            //    if (readyToGo && goingLeft == !startFromRight) StartRockNRoll();
            //}

            Vector3 newEulerAngles = transform.eulerAngles;
            float t = Mathf.SmoothStep(0, 1, (Time.time%timePerSwing) / timePerSwing);
            newEulerAngles.z = Mathf.Lerp(goingLeft ? yRotationMinMax.x : yRotationMinMax.y, goingLeft ? yRotationMinMax.y : yRotationMinMax.x, t);
            //newEulerAngles.z = Mathf.Lerp(left ? yRotationMinMax.x, left ? yRotationMinMax.y : yRotationMinMax.x, t);

            transform.eulerAngles = newEulerAngles;        
        //}
        //else
        //{
        //    deathBoulder.AddTorque(deathBoulderTorqueForce);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        for (int i = 0; i < 2;i++)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(yRotationMinMax[i] * Mathf.Deg2Rad), Mathf.Sin(yRotationMinMax[i] * Mathf.Deg2Rad), 0) * 10f);
        }
    }

    //private void StartRockNRoll()
    //{
    //    going = true;
    //    deathBoulder.transform.SetParent(null);
    //    deathBoulder.gravityScale = 1f;
    //    deathBoulder.constraints = RigidbodyConstraints2D.None;
    //}
}
