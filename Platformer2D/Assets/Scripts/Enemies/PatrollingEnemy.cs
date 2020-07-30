using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : EnemyController
{
    [Header("DETECTION")]
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("DISTANCES")]
    public float groundDistance;
    public float wallDistance;

    // private
    private Rigidbody2D rb;

    private bool
    groundDetected,
    wallDetected;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingDirection = 1;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        CheckDirection();
        CheckHealth();
    }

    private void CheckDirection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            Flip();
            ChangeDirection();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallDistance, wallCheck.position.y));
    }
}
