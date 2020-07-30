using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float WaitAfterLastShot;

    [Header("Game Objects")]
    public GameObject toiletPaperPrefab;
    public Transform shotPoint;

    // Update is called once per frame
    private void Update()
    {
        CheckShoot();
    }

    private void CheckShoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Get & Convert Mouse Position
        float rotationZ = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg; // calculate z-Rotation
        transform.rotation = Quaternion.Euler(0, 0, rotationZ); // Set Weapon Rotation to mousePosition

        if (WaitAfterLastShot <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }
        else
        {
            WaitAfterLastShot -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameMaster.instance.PlaySound("Shot");
        Instantiate(toiletPaperPrefab, shotPoint.position, transform.rotation);
        WaitAfterLastShot = 0.5f;
    }
}


