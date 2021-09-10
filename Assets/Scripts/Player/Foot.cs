using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour {
    [SerializeField] PlayerEntity playerEntity;
    private void OnCollisionEnter2D(Collision2D collision) {
        playerEntity.isGrounded++;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        playerEntity.isGrounded--;
        if (playerEntity.isGrounded < 0) {
            playerEntity.isGrounded = 0;
        }
    }
}
