using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] float rotateSpeed=30.0f;
    [SerializeField] float floatSpeed = 10.0f;
    [SerializeField] float floatRange = 2.0f;
    private AudioSource coinAudio;
    public AudioClip coinSound;
    float middle;
    bool goDown;

    void Start()
    {
        coinAudio = GetComponent<AudioSource>();
        middle = transform.position.y;
        goDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (transform.position.y < middle + floatRange && !goDown)
        {
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        }
        else {
            goDown = true;
        }
        if (transform.position.y > middle - floatRange && goDown){
            transform.Translate(Vector3.down * floatSpeed * Time.deltaTime);
        }
        else {
            goDown = false;   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!PlayerControl.isMuted)
            {
                coinAudio.PlayOneShot(coinSound);
            }
        }
    }
}
