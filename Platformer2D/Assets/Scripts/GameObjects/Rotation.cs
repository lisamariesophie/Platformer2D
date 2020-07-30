using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotationZ;
    public float rotationSpeed = 100f;
    public bool clockWise = true;

    // Update is called once per frame
    void Update()
    {
        if (!clockWise)
        {
            rotationZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotationZ += -Time.deltaTime * rotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
