using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour {
    public Rabbit owner;
    private void OnTriggerEnter2D(Collider2D collision) {
        owner.direction = new Vector2(-owner.direction.x, owner.direction.y);
    }

}
