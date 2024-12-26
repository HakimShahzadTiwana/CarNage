using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMovement : MonoBehaviour
{
    private float speed = 50.0f;
    private GameObject firePoint;
    private Rigidbody ammoRb;
    void Start()
    {
        ammoRb = GetComponent<Rigidbody>();
        firePoint = GameObject.Find("FirePoint");
    }


    void Update()
    {
        ammoRb.AddForce(firePoint.transform.forward * speed, ForceMode.Impulse);
        if (transform.position.z > GameObject.Find("Player").transform.position.z + 100) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        Destroy(gameObject);
    }
}
