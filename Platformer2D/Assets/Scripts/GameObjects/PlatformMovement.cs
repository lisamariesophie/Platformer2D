using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pos1, pos2, startPos;
    public float speed = 1.0f;
    Vector3 nextPos;

    // Start is called before the first frame update
    private void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {   // Set the next position
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        // Move the Platform
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    // Draw Position Gizmos
    private void OnDrawGizmos() {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
