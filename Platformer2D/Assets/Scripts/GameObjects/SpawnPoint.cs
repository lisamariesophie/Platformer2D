using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy Portal SpawnPoint after 3 seconds 
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
