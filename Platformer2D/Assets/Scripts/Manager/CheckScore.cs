using UnityEngine;

public class CheckScore : MonoBehaviour
{
    public GameObject player;
    private PlayerHealth health;
    public GameObject portalPrefab;
    public Transform nextLevelPortal;

    private void Start()
    {
        health = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Check();
        }
    }

    private void Check()
    {
        // if Score high enough open Next Level Portal, else Die
        if (Score.instance.GetScore() >= Score.instance.GetNeedScore())
        {
            GameObject portal = Instantiate(portalPrefab, nextLevelPortal.position, Quaternion.identity);
            Animator portalAnim = portal.GetComponent<Animator>();
            portalAnim.SetTrigger("open");
        }
        else
        {
            health.Die();
        }
    }
}
