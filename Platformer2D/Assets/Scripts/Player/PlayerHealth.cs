using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth;
    private int currentHealth;
    private PlayerController player;

    [Header("Particles")]
    public ParticleSystem deathParticles;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Audio")]
    public string deathSoundName;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GetComponent<PlayerController>();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= (int)amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetCurrentHealth(currentHealth);
    }

    private void TakeDamage(int amount)
    {
        DecreaseHealth(amount);
        player.KnockBack();
    }

    public void Die()
    {
        GameMaster.instance.PlaySound(deathSoundName);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        GameMaster.instance.KillPlayer(player);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn") || other.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
