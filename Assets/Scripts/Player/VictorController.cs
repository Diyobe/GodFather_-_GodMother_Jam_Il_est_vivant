using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorController : MonoBehaviour
{
    [SerializeField] PlayerEntity playerEntity;
    public Animator anim;
    public Rigidbody2D rigid;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerEntity.direction.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") > 0 )
        {
            anim.SetBool("walk", true);
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetBool("walk", false);
        }


        if (Input.GetKey("space") || Input.GetKey("z")) {
            playerEntity.tryToJump = true;
        } else {
            playerEntity.tryToJump = false;
        }

        if (rigid.velocity.y > 0)
        {
            //jump up
        } else if (rigid.velocity.y < 0)
        {
            //jump down
        }

    }

}
