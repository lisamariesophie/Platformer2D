using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public ParticleSystem destroyParticles;
    public GameObject groundDNA;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.contacts[0].normal.y > 0.5f) // check if box is hit from below
        {
           Break();
        }
    }

    private void Break()
    {
        // Spawn Box Particles
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
        // Spawn DNA
        Instantiate(groundDNA, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
