using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerEntity playerEntity;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        playerEntity.direction.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey("space") || Input.GetKey("z")) {
            playerEntity.tryToJump = true;
        } else {
            playerEntity.tryToJump = false;
        }
    }

}
