using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
   
    public GameObject road;
    private float roadWidth=1000;
    private Vector3 nextPos;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("RoadCreateTrig") && other.gameObject.CompareTag("Player"))
        {
            CreateRoad();   
        }
        else if (gameObject.CompareTag("RoadDestroyTrig") && other.gameObject.CompareTag("Player")) {
            StartCoroutine(nameof(DestroyRoad));
        }
    }

    IEnumerator DestroyRoad() {
        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);
    }

    void CreateRoad() {
        float currentRoadPos = gameObject.GetComponentInParent<Transform>().transform.position.z;

        float pos = currentRoadPos + roadWidth;
        nextPos = new Vector3(0, 0, pos);
        Instantiate(road, nextPos, road.transform.rotation);
    }
    
}
