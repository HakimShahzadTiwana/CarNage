using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject rocket;
    public GameObject enemyGraphics;
    public ParticleSystem smallExplosion;
    public ParticleSystem bigExplosion;
    public AudioClip bigExplosionSound;
    public AudioClip smallExplosionSound;
    public AudioClip rocketSound;
    

    public int health;


    private BoxCollider enemyCollider;
    private AudioSource enemyAudio;

    private float speed = 4.0f;
    private bool added = false;

    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        enemyCollider = GetComponent<BoxCollider>();
        if (gameObject.name == "Tank(Clone)")
        {
            Invoke(nameof(FireRocket), 0.75f);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z < GameObject.Find("Player").transform.position.z-5) {
            Destroy(gameObject);
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            TakeDamage(10);

        }
        else if (collision.gameObject.name == "Rocket(Clone)")
        {
            TakeDamage(50);
          
        }
        else if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
        if (health < 1)
        {
            if (gameObject.name == "Tank(Clone)")
            {
                if (!added) {
                    Achievements.AddTanksDestroyed(1);
                    Challenges.AddDailyTanksDestroyed(1);
                    added = true;
                }
                bigExplosion.Play();
                if (!PlayerControl.isMuted)
                {
                    enemyAudio.PlayOneShot(bigExplosionSound);
                }

            }
            else
            {
                smallExplosion.Play();
                if (!PlayerControl.isMuted)
                {
                    enemyAudio.PlayOneShot(smallExplosionSound);
                }
            }
            enemyCollider.enabled = false;
            enemyGraphics.SetActive(false);
            StartCoroutine(nameof(DestroyOnDelay));
        }
    }


    void TakeDamage(int damage) {
        health -= damage;
    }
    private void FireRocket() {
   
     
        Instantiate(rocket, transform.position - new Vector3(0, 0, 5), rocket.transform.rotation);
        if (!PlayerControl.isMuted)
        {
            enemyAudio.PlayOneShot(rocketSound);
        }
        
    }
    private IEnumerator DestroyOnDelay() 
    {
       
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
