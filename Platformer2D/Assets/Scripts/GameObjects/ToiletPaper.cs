using UnityEngine;

public class ToiletPaper : MonoBehaviour
{
    [SerializeField]
    private float
    speed,
    lifeTime;

    [SerializeField]
    private int damage;
    public ParticleSystem shootParticles;
    private Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("DestroyToiletPaper", lifeTime); // Destroy ToiletPaper after lifetime reached
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                DestroyToiletPaper();
            }
        }
    }

    public void DestroyToiletPaper()
    {
        Instantiate(shootParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
