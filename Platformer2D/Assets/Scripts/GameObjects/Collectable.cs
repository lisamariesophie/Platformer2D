using UnityEngine;
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Score.instance.IncreaseScore();
            GameMaster.instance.PlaySound("Collect");
        }
    }
}
