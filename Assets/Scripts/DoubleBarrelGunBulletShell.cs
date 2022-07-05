using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelGunBulletShell : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right*10);
        Destroy(this.gameObject, 1f);
    }


}
