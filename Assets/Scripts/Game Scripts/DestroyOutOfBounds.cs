using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    void Update()
    {
        if (transform.position.z < GameObject.FindGameObjectWithTag("Player").transform.position.z - 5)
        {
            Destroy(gameObject);
        }
    }
}
