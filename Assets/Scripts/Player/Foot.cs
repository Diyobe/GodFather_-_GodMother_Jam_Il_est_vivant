using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] PlayerEntity playerEntity;
    private void OnTriggerEnter2D(Collider2D collision) {
        playerEntity.isGrounded++;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        playerEntity.isGrounded = playerEntity.isGrounded - 1 < 0 ? 0 : playerEntity.isGrounded - 1;
    }
}
