using UnityEngine;

public class Boss : EnemyController
{
    [SerializeField]
    private float
    attackSpeed,
    attackPlayerSpeed,
    checkRadius;

    private bool
    topDetected,
    bottomDetected,
    wallDetected,
    directionUp = true,
    followingPlayer = false;

    [SerializeField]
    private Vector2
    idleDirection,
    attackDirection;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform
    checkUp,
    checkDown,
    checkSide;

    private Rigidbody2D rb;
    private Vector2 playerPos;
    private Animator bossAnim;

    [SerializeField]
    private float
    waitDamage,
    damage, 
    distance;

    private float lastDamageTime = 0f;

    public GameObject PortalPrefab;
    public Transform spawnPortal;



    // Start is called before the first frame update
    void Start()
    {
        idleDirection.Normalize(); // fix for direction effecting speed
        attackDirection.Normalize(); // fix for direction effecting speed
        rb = GetComponent<Rigidbody2D>();
        bossAnim = GetComponent<Animator>();
        facingLeft = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        FlipToPlayer();
        CheckHealth();
        CheckSides();
    }

    private void CheckSides()
    {
        topDetected = Physics2D.Raycast(checkUp.position, Vector2.up, distance, whatIsGround);
        bottomDetected = Physics2D.Raycast(checkDown.position, Vector2.down, distance, whatIsGround);
        wallDetected = Physics2D.Raycast(checkSide.position, Vector2.left, distance, whatIsGround);
    }

    public void Idle()
    {
        if (topDetected && directionUp)
        {
            ChangeDirection();
        }
        else if (bottomDetected && !directionUp)
        {
            ChangeDirection();
        }
        if (wallDetected)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        rb.velocity = speed * idleDirection;
    }

    public void Attack()
    {
        if (topDetected && directionUp)
        {
            ChangeDirection();
        }
        else if (bottomDetected && !directionUp)
        {
            ChangeDirection();
        }
        if (wallDetected && facingLeft)
        {
            Flip();
        }
        else if (!facingLeft && wallDetected)
        {
            Flip();
        }
        rb.velocity = attackSpeed * attackDirection;
    }

    public void AttackPlayer()
    {
        if (!followingPlayer)
        {
            playerPos = player.transform.position - transform.position;
            playerPos.Normalize();
            followingPlayer = true;
        }
        if (followingPlayer)
        {
            rb.velocity = playerPos * attackPlayerSpeed;
        }
        if (wallDetected || bottomDetected)
        {
            rb.velocity = Vector2.zero;
            followingPlayer = false;
            bossAnim.SetTrigger("attackDown");
        }
    }

    private void randomAttack() // called from idle animation
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                bossAnim.SetTrigger("attack");
                break;
            case 1:
                bossAnim.SetTrigger("attackPlayer");
                break;
        }
    }

    protected override void ChangeDirection()
    {
        directionUp = !directionUp;
        idleDirection.y *= -1;
        attackDirection.y *= -1;
    }

    protected override void Flip()
    {
        idleDirection.x *= -1;
        attackDirection.x *= -1;
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    private void FlipToPlayer()
    {
        float playerDir = player.transform.position.x - transform.position.x;
        if (playerDir > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDir < 0 && !facingLeft)
        {
            Flip();
        }
    }

    protected override void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameObject portal = Instantiate(PortalPrefab, spawnPortal.position, Quaternion.identity);
        Animator portalAnim = portal.GetComponent<Animator>();
        portalAnim.SetTrigger("open");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + waitDamage) // wait for some Time before next Player Damage
            {
                lastDamageTime = Time.time;
                other.gameObject.SendMessage("TakeDamage", damage);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(checkUp.position, new Vector2(checkUp.position.x, checkUp.position.y + distance));
        Gizmos.DrawLine(checkSide.position, new Vector2(checkSide.position.x - distance, checkSide.position.y));
        Gizmos.DrawLine(checkDown.position, new Vector2(checkDown.position.x, checkDown.position.y - distance));
    }
}
