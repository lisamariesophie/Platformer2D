using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("GENERAL")]
    [SerializeField]
    protected float speed;
    public LayerMask whatIsGround;
    [Header("HEALTH")]
    [SerializeField]
    protected int maxHealth = 100;
    [Header("UI")]
    public HealthBar healthBar;
    [Header("OTHER")]
    public ParticleSystem deathEffect;
    public GameObject DNAPrefab;
    protected int facingDirection;

    // private
    protected int currentHealth;
    protected bool facingLeft = true;

    protected void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetCurrentHealth(currentHealth);
    }

    protected virtual void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Instantiate(DNAPrefab, transform.position, Quaternion.identity);
    }

    protected virtual void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    protected virtual void ChangeDirection() {
        facingDirection *= -1;
    }
}
