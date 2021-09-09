using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontRabbitFoot : MonoBehaviour {
    public Rabbit owner;
    private void OnTriggerEnter2D(Collider2D collision) {
        owner.frontIsStillGrounded++;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        owner.frontIsStillGrounded--;
    }

}
