using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : MonoBehaviour
{
    private Rigidbody rocketRb;
    void Start()
    {
        Time.timeScale = 0.5F;
        rocketRb = GetComponent<Rigidbody>();
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        transform.Rotate(Vector3.right * 90);
        rocketRb.AddForce(transform.up * 50.0f, ForceMode.Impulse);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < GameObject.FindGameObjectWithTag("Player").transform.position.z - 3)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
