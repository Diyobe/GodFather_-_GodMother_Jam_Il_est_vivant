using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderRabbitFoot : MonoBehaviour {
    public Rabbit owner;
    private void OnTriggerEnter2D(Collider2D collision) {
        owner.underIsStillGrounded++;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        owner.underIsStillGrounded--;
    }
}
