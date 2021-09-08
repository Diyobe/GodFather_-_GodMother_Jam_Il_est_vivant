using UnityEngine;

public class PlayerEntity : MonoBehaviour {
    public float maxSpeed;
    [SerializeField] float maxSpeedInAir;
    [SerializeField] float timeBeforeMaxSpeed; // duration before reach max speed
    [SerializeField] float jumpSpeed;
    [SerializeField] float ratioComeBack; // make the player go to the other direction more fastly (only when grounded)

    /* [HideInInspector]*/
    public int isGrounded;
    int isIntheAir = 0;
    [SerializeField] float friction = 0;
    [SerializeField] float airFriction = 0;
    [SerializeField] float slowSpeed = 0.5f; //if "speed" is below this, "speed" = 0 cause it will make the player shaking when using friction

    [HideInInspector] public bool tryToJump;
    bool rememberJump;
    bool canJump;
    bool mustJump = false;
    [SerializeField] float tryToJumpValue = 0.2f;
    float tryToJumpTimer = -100f;
    [SerializeField] float maxJumpValue = 1f;
    [SerializeField] float minJumpValue = 0.1f;
    float jumpTimer = -100f;
    [SerializeField] float frameBeforeJumpMaxSpeed = 20;
    [SerializeField] float frameBeforeStopJump = 20;
    Vector2 jumpForce = Vector2.zero;

    private Rigidbody2D rb;
    public Animator anim;
    [SerializeField] GameObject playerSprites;

    [HideInInspector] public Vector2 direction;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
        JumpManagement();
    }


    private void Move() {
        Vector2 currentSpeed = rb.velocity;
        if (isIntheAir == 0) {
            //anim.SetBool("isJumpingDown", true);
            if (direction.x == 0 && currentSpeed.x != 0) {
                currentSpeed.x += -Mathf.Sign(currentSpeed.x) * Time.fixedDeltaTime * airFriction;
                if (-slowSpeed < currentSpeed.x && currentSpeed.x < slowSpeed) {
                    currentSpeed.x = 0;
                }
            } else {
                currentSpeed.x += direction.x * Time.fixedDeltaTime * maxSpeedInAir / timeBeforeMaxSpeed;
                currentSpeed.x = Mathf.Clamp(currentSpeed.x, -maxSpeedInAir, maxSpeedInAir);
                if (direction.x != 0) {
                        playerSprites.transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(playerSprites.transform.localScale.z), playerSprites.transform.localScale.y, playerSprites.transform.localScale.z);
                }
            }
            anim.SetBool("walk", false);
        } else {
            if (direction.x == 0 && currentSpeed.x != 0) {
                currentSpeed.x += -Mathf.Sign(currentSpeed.x) * Time.fixedDeltaTime * friction;
                if (-slowSpeed < currentSpeed.x && currentSpeed.x < slowSpeed) {
                    currentSpeed.x = 0;
                }
                anim.SetBool("walk", false);

            } else {
                currentSpeed.x += Mathf.Sign(currentSpeed.x) == Mathf.Sign(direction.x) ? direction.x * Time.fixedDeltaTime * maxSpeed / timeBeforeMaxSpeed : direction.x * Time.fixedDeltaTime * maxSpeed / timeBeforeMaxSpeed * ratioComeBack;
                currentSpeed.x = Mathf.Clamp(currentSpeed.x, -maxSpeed, maxSpeed);
                if (direction.x != 0) {
                    playerSprites.transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(playerSprites.transform.localScale.z), playerSprites.transform.localScale.y, playerSprites.transform.localScale.z);
                    anim.SetBool("walk", true);
                }     
            }
        }
        rb.velocity = currentSpeed;
    }
    public void JumpManagement() {
        RememberJump();
        if (tryToJump || mustJump) {
            if (IsJumpPossible()) {
                Jump(true);
            } else {
                rememberJump = true;
                canJump = false;
                Jump(false);
            }
        } else {
            mustJump = false;
            jumpTimer = -100f;
            canJump = false;
            Jump(false);
        }
        if (isGrounded > 0) {
            anim.SetBool("isJumpingDown", false);
            anim.SetBool("isJumpingUp", false);
            rb.velocity -= jumpForce;
            jumpForce.y = 0;
        }
    }
    private void RememberJump() { // Input Buffer
        if (rememberJump) {
            if (tryToJumpTimer == -100f) {
                tryToJumpTimer = tryToJumpValue;
            }
            tryToJumpTimer -= Time.fixedDeltaTime;
            if (tryToJumpTimer <= 0) {
                tryToJumpTimer = -100f;
                tryToJump = false;
                rememberJump = false;
            } else {
                tryToJump = true;
            }
        }
    }
    public bool IsJumpPossible() {
        if (isGrounded > 0) {
            canJump = true;
            anim.SetBool("isJumpingUp", true);
        }
        if (mustJump || canJump) {
            if (jumpTimer == -100f) {
                jumpTimer = maxJumpValue;
            } else {
                jumpTimer -= Time.fixedDeltaTime;
            }
            if (jumpTimer <= 0) {
                anim.SetBool("isJumpingUp", false);
                anim.SetBool("isJumpingDown", true);
                mustJump = false;
                canJump = false;
                jumpTimer = -100f;
                return false;
            }
            if (maxJumpValue - minJumpValue < jumpTimer) {
                mustJump = true;
            } else {
                mustJump = false;
            }
            return true;
        }
        return false;
    }

    private void Jump(bool add) {
        rb.velocity -= jumpForce;
        if (add) {
            isGrounded = isGrounded - 1 < 0 ? 0 : isGrounded - 1;
            jumpForce += new Vector2(0, jumpSpeed / frameBeforeJumpMaxSpeed);
            if (jumpForce.y > jumpSpeed) {
                jumpForce.y = jumpSpeed;
            }
            rb.velocity += jumpForce;
            rememberJump = false;
            tryToJumpTimer = -100f;
        } else {
            jumpForce -= new Vector2(0, jumpSpeed / frameBeforeStopJump);
            if (jumpForce.y < 0) {
                jumpForce.y = 0;
            }
            rb.velocity += jumpForce;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        isIntheAir++;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isIntheAir--;
    }

}
