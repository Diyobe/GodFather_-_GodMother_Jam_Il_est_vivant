using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlood : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject bloodParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Death"))
        {
            Debug.LogError("Death");
            Instantiate(bloodSplat, transform.position, Quaternion.identity);
            //Instantiate(bloodParticle, transform.position, Quaternion.identity);
        }
    }
}
