using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimation playerAnim;
    private LoadLevel loadLevel;


    [Header("General")]
    public Transform spawnPoint;
    public Transform feet;
    public Transform front;
    public LayerMask whatIsGround;
    public float checkRadius = 0.3f;

    [Header("Movement")]
    public float speed = 6f;

    [Header("Jump")]
    public float jumpForce = 15f;
    public float jumpHeight = 0.5f;
    public float airForce = 40f;
    public int extraJumps = 1;


    [Header("WallJump")]
    public Vector2 wallJumpDirection;
    public float wallDistance = 0.5f;
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 25f;


    [Header("Knockback")]
    public float knockbackDuration;

    private bool
    isFacingRight = true,
    isGrounded,
    isTouchingWall,
    wallSliding,
    jumping,
    knockback;

    private int
    jumpsLeft,
    facingDirection = 1;

    private float
    moveInput,
    knockbackStartTime;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        wallJumpDirection.Normalize(); // -> Set Vector to 1
        loadLevel = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LoadLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
        CheckDirection();
        CheckIfGrounded();
        CheckIfWallSliding();
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, whatIsGround); // detect ground
        isTouchingWall = Physics2D.Raycast(front.position, transform.right, wallDistance, whatIsGround); // detect wall
        Move();
    }

    private void DetectInput()
    {
        moveInput = Input.GetAxis("Horizontal"); // returns 1 (right) or -1 (left)

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (jumpsLeft > 0 && !isTouchingWall))
            {
                Jump();
            }
            if (wallSliding && moveInput != 0 && moveInput != facingDirection)
            {
                WallJump();
            }
        }

        // Jump Height
        if (!Input.GetButton("Jump") && jumping) // Jump Button released and jumping
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeight); // reduce y-velocity
            jumping = false;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);

        //Player moving in air
        if (!isGrounded && !wallSliding && moveInput != 0)
        {
            Vector2 addForce = new Vector2(airForce * moveInput, 0); // add Force when in Air and move Input != 0
            rb.AddForce(addForce);
        }

        //Player is wallsliding
        if (wallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(0, -wallSlideSpeed);
                playerAnim.WallSlide(true);
            }
        }
    }

    private void CheckDirection()
    {
        if (isFacingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveInput > 0)
        {
            Flip();
        }
        if (moveInput != 0)
        {
            playerAnim.Run(true);
        }
        else
        {
            playerAnim.Run(false);
        }
    }

    private void CheckIfGrounded()
    {
        if (isGrounded)
        {
            jumpsLeft = extraJumps;
            playerAnim.Jump(false);
            playerAnim.WallSlide(false);
        }
        else
        {
            playerAnim.Jump(true);
        }
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
            playerAnim.WallSlide(false);
        }
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        GameMaster.instance.PlaySound("Jump");
        playerAnim.JumpTakeOff();
        jumpsLeft--;
        jumping = true;
    }

    private void WallJump()
    {
        GameMaster.instance.PlaySound("Jump");
        playerAnim.JumpTakeOff();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        Vector2 force = new Vector2(wallJumpForce * wallJumpDirection.x * moveInput, wallJumpForce * wallJumpDirection.y);
        rb.AddForce(force, ForceMode2D.Impulse);
        wallSliding = false;
        jumping = true;
    }

    public void KnockBack()
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(0.0f, 10); // up
    }

    private void Flip()
    {
        if (!wallSliding)
        {
            transform.Rotate(0f, 180f, 0f);
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MGround"))
        {
            // Stick Player to Moving Platform
            this.transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MGround"))
        {
            // Release Player from Moving Platform
            this.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            loadLevel.LoadNextLevel();
        }
    }
}
