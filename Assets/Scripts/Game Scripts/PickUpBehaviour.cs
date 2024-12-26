using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{

    PlayerControl pc;
    BoxCollider collider;
    MeshRenderer meshRenderer;
    AudioSource pickUpAudio;

    public AudioClip powerUpSound;
    public AudioClip ammoPickSound;
    public GameObject rocketBody;
    public GameObject bulletBody;
    



    void Start()
    {
        if (gameObject.CompareTag("PowerUpPickUp"))
        {
            collider = GetComponent<BoxCollider>();
            meshRenderer = this.GetComponentInChildren<MeshRenderer>();
            pickUpAudio = GetComponent<AudioSource>();
        }
        else if (gameObject.CompareTag("AmmoPickUp"))
        {
            collider = GetComponent<BoxCollider>();
            meshRenderer = null;
            pickUpAudio = GetComponent<AudioSource>();
        }
        else
        {
            collider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>();
        }
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("PowerUpPickUp"))
            {
                if (!PlayerControl.isMuted)
                {
                    pickUpAudio.PlayOneShot(powerUpSound);
                }

                if (gameObject.name == "InfinitePowerUp(Clone)")
                {
                    pc.SetInfiniteTimer();
                }
                else if (gameObject.name == "IntangiblePowerUp(Clone)")
                {
                    pc.SetIntangibleTimer();
                }
                else if (gameObject.name == "MultiplierPowerUp(Clone)")
                {
                    pc.SetMultiplierTimer();
                }
            }
            collider.enabled = false;
            if (gameObject.CompareTag("AmmoPickUp"))
            {
                if (!PlayerControl.isMuted)
                {
                    pickUpAudio.PlayOneShot(ammoPickSound);
                }
                if (gameObject.name == "RocketAmmoPickup(Clone)")
                {
                    rocketBody.SetActive(false);
                }
                else
                {
                    bulletBody.SetActive(false);
                }
            }
            else
            {
                meshRenderer.enabled = false;
            }
            StartCoroutine(nameof(DestroyOnDelay));
            
            
        }
        if (gameObject.CompareTag("PowerUpPickUp") && other.gameObject.CompareTag("AmmoPickUp")) {
            Destroy(gameObject);
        }
    }


    IEnumerator DestroyOnDelay()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
    
}
