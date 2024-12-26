using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   
    public static string INTANGIBLE = "Intangible" ;
    public static string INFINITEAMMO = "Infinite Ammo";
    public static string MULTIPLIER = "2x Multiplier";
    public static string PLAYERCOINS = "TotalCoins";
    public static string PLAYERHIGHSCORE = "HighScore";
    public static string INFINITE_LEVEL ="lvlInfnite";
    public static string INTANGIBLE_LEVEL ="lvlIntangible";
    public static string MULTIPLIER_LEVEL ="lvlMultiplier";
    public static string IS_MUTED = "IS_MUTED";
    public static string MUSIC_TIME = "MUSIC_TIME";

    public static bool isMuted;

    public GameObject bullet;
    public GameObject rocket;
    public GameObject playerBody;

    public ParticleSystem muzzleFlash;
    public ParticleSystem smoke;
    public ParticleSystem explosion;
    public ParticleSystem fire;

    public AudioClip bulletSound;
    public AudioClip missileSound;
    public AudioClip impactSound;
    public AudioClip rocketImpactSound;
    public AudioClip explosionSound;

    public Material[] playerColor;

    private float speed = 20f;
    private float torque = 1f;
    private float TorqueRange = 0.26f;
    private float stabilizeTorque=40f;
    private float stabilizeTorqueRange = 0.01f;
    private float xRange = 5f;
    private float horizontalInput;
    private float startingPos;
    private float currPos;

    private int bulletCap=15;
    private int RocketCap=3;
    private int bulletCount=5;
    private int rocketCount = 1;
    private int coinCount = 0;
    private int health=100;
    private int score;

    private bool isInfiniteAmmo = false;
    private bool isIntangible= false;
    private bool isMultiplier = false;
    private bool stopped;
    private int infiniteDuration = 5;
    private int multiplierDuration = 5;
    private int intangibleDuration = 5;
    

   

    private Rigidbody playerRb;
    private BoxCollider playerCollider;
    private GameObject firePoint;
    private AudioSource playerAudio;
    private AudioSource backgroundMusic;
    private Light powerUpIndicator;
    private UIManager UIM;

   

    void Start()
    {
        
        playerBody.GetComponent<MeshRenderer>().material = playerColor[PlayerPrefs.GetInt(CustomizationUIManager.PLAYER_COLOR, 0)];
        playerAudio = GetComponent<AudioSource>();
        powerUpIndicator = GetComponent<Light>();
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
        firePoint = GameObject.Find("FirePoint");
        startingPos = transform.position.z;
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        UIM.UpdateBullets(bulletCount);
        UIM.UpdateRockets(rocketCount);
        infiniteDuration += (PlayerPrefs.GetInt(INFINITE_LEVEL, 0) * 2);
        intangibleDuration += (PlayerPrefs.GetInt(INFINITE_LEVEL, 0) * 2);
        multiplierDuration += (PlayerPrefs.GetInt(INFINITE_LEVEL, 0) * 2);
        Challenges.ClearSingleGameCoins();
        Challenges.ClearSingleGameScore();  
        backgroundMusic = Camera.main.GetComponent<AudioSource>();
        backgroundMusic.time = PlayerPrefs.GetFloat(MUSIC_TIME, 0f);
        GetMuteStatus();
        PlayerMute();
    }

   
    void Update()
    {
        /*********** UNCOMMENT FOLLOWING LINES FOR PLAYING ON PC AND REMOVE CORRESPONDING UI BUTTONS *************/
        /* horizontalInput = Input.GetAxis("Horizontal");


          if (Input.GetKeyDown(KeyCode.Space)) {
              FireBullet();

          }

          if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
             FireRocket();
          }
        */
        HandleScore();
        if (horizontalInput > -0.5 && horizontalInput < 0.5)
        {
            StabilizePlayer();
        }

        if (health < 1 && !UIM.isGameOver)
        {
            PlayerDeath();
        }


    }

    private void FixedUpdate()
    {
        if (!UIM.isGameOver)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, speed);
            TurnPlayer();
        }
        else
        {
            playerRb.velocity = new Vector3(0, 0, 0);
        }
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ammo"))
        {
            if (!isMuted)
            {
                playerAudio.PlayOneShot(rocketImpactSound);
            }
            TakeDamage(25);

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isMuted)
            {
                playerAudio.PlayOneShot(impactSound);
            }
            TakeDamage(50);
        }
        else if (collision.gameObject.CompareTag("Wall")) {
            if (!isMuted)
            {
                playerAudio.PlayOneShot(impactSound);
            }
            TakeDamage(10);
        }

        if (health <= 50)
        {
            smoke.Play();
            smoke.GetComponentInChildren<Light>().enabled = true;
        }
       
       


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AmmoPickUp"))
        {
            if (other.gameObject.name == "BulletAmmoPickup(Clone)")
            {
                AddBullets();
            }
            else if (other.gameObject.name == "RocketAmmoPickup(Clone)")
            {
                AddRockets();
            }

        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            CollectCoins();

        }
        else if (other.gameObject.CompareTag("Repair")) {
            RepairDamage(20);
        }
    }

    void TakeDamage(int damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
        }
        UIM.UpdateHealth(health);

    }
    void RepairDamage(int repair) {
        if (health <= 100-repair)
        {
            health += repair;
        }
        else {
            health = 100;
        }
  
        UIM.UpdateHealth(health);
    }
    void AddBullets() {
        if (bulletCount < bulletCap)
        {
            bulletCount += 5;
            if (bulletCount > bulletCap) { bulletCount = bulletCap; }
            UIM.UpdateBullets(bulletCount);
        }
    }
    void AddRockets() {
        if (rocketCount < RocketCap)
        {
            rocketCount++;
            UIM.UpdateRockets(rocketCount);
        }
    }
    void CollectCoins() {
        if (isMultiplier)
        {
            coinCount += 2;
            UIM.UpdateCoins(coinCount);
        }
        else
        {
            coinCount++;
            UIM.UpdateCoins(coinCount);
        }
    }
    void StabilizePlayer()
    {

        if (transform.rotation.y < -stabilizeTorqueRange)
        {
            transform.Rotate(transform.up * stabilizeTorque * Time.deltaTime);
        }
        else if (transform.rotation.y > stabilizeTorqueRange)
        {
            transform.Rotate(transform.up * -1 * stabilizeTorque * Time.deltaTime);
        }
    }
    void TurnPlayer() {
        if (!(playerRb.position.x < -xRange && horizontalInput < 0) && !(playerRb.position.x > xRange && horizontalInput > 0))
        {
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
            if (transform.rotation.y > -TorqueRange && transform.rotation.y < TorqueRange)
                transform.Rotate(transform.up * torque * horizontalInput);

            if (transform.position.x > -xRange && transform.position.x < xRange)
            {
                stopped = false;
            }
        }

        if (transform.position.x < -xRange && !stopped)
        {
            playerRb.velocity = new Vector3(0, 0, playerRb.velocity.z);
            stopped = true;
        }
        else if (transform.position.x > xRange && !stopped)
        {
            playerRb.velocity = new Vector3(0, 0, playerRb.velocity.z);
            stopped = true;
        }
    }

    void SaveTotalCoins() {
        if (PlayerPrefs.HasKey(PLAYERCOINS))
        {
            int currentCoins = PlayerPrefs.GetInt(PLAYERCOINS);
            PlayerPrefs.SetInt(PLAYERCOINS, currentCoins + coinCount);
        }
        else
        {

            PlayerPrefs.SetInt(PLAYERCOINS, coinCount);

        }
    }
    void SaveHighScore() {
        if (PlayerPrefs.HasKey(PLAYERHIGHSCORE))
        {
            if (score > PlayerPrefs.GetInt(PLAYERHIGHSCORE))
            {
                PlayerPrefs.SetInt(PLAYERHIGHSCORE, score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(PLAYERHIGHSCORE, score);
        }
    }

    
    void PlayerDeath()
    {  
        explosion.Play();
        fire.Play();
        if (!isMuted)
        {
            playerAudio.PlayOneShot(explosionSound);
        }
        SaveTotalCoins(); 
        SaveHighScore();
        Achievements.AddCoinsCollected(coinCount);
        Achievements.AddPointsScored(score);
        Challenges.AddDailyCoins(coinCount);
        Challenges.AddDailyScore(score);
        Challenges.SaveSingleGameCoins(coinCount);
        Challenges.SaveSingleGameScore(score);
        UIM.DisplayGameOver(score, coinCount);
        
        //Destroy(gameObject);

    }
    void HandleScore()
    {
        currPos = transform.position.z;
        if (isMultiplier)
        {
            //  score = (int)(currPos - startingPos) * 2;
            score = (int)(currPos - startingPos);
        }
        else
        {
            score = (int)(currPos - startingPos);
        }

        UIM.UpdateScore(score);

    }
    public void FireRocket()
    {
        if (rocketCount > 0)
        {
            Instantiate(rocket, new Vector3(transform.position.x, rocket.transform.position.y, transform.position.z + 5), rocket.transform.rotation);
            muzzleFlash.Play();
            if (!isMuted)
            {
                playerAudio.PlayOneShot(missileSound);
            }
            rocketCount--;
            Achievements.AddRocketsLaunched(1);
            UIM.UpdateRockets(rocketCount);
        }
    }
    public void FireBullet()
    {
        if (bulletCount > 0 || isInfiniteAmmo)
        {
            Instantiate(bullet, new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z), bullet.transform.rotation);
            muzzleFlash.Play();
            if (!isMuted)
            {
                playerAudio.PlayOneShot(bulletSound);
            }
            if (!isInfiniteAmmo)
            {
                bulletCount--;
                Achievements.AddBulletsFired(1);
                UIM.UpdateBullets(bulletCount);


            }
        }
    }

    public void UpdateHorizontalInput(int inp) {
        horizontalInput = inp;
    }
    public void SetInfiniteTimer() {
        isInfiniteAmmo = true;
        Achievements.AddPowerGained(1);
        StartCoroutine(nameof(InfiniteTimer));
    }
    public void SetIntangibleTimer()
    {
        isIntangible = true;
        Achievements.AddPowerGained(1);
        StartCoroutine(nameof(IntangibileTimer));
    }
    public void SetMultiplierTimer()
    {
        isMultiplier = true;
        Achievements.AddPowerGained(1);
        StartCoroutine(nameof(MultiplierTimer));
    }
    IEnumerator InfiniteTimer()
    {
        Debug.Log("Infinite Picked");
        UIM.UpdatePowerUp(INFINITEAMMO, Color.red);
        powerUpIndicator.color = Color.red;
        yield return new WaitForSeconds(infiniteDuration);
        isInfiniteAmmo = false;
        powerUpIndicator.color = Color.black;
        UIM.UpdatePowerUp("", Color.white);
        Debug.Log("Infinite Dropped");

    }
    IEnumerator IntangibileTimer()
    {
        Debug.Log("Intangible Picked");
        UIM.UpdatePowerUp(INTANGIBLE, Color.cyan);
        playerCollider.isTrigger = true;
        playerRb.useGravity = false;
        powerUpIndicator.color = Color.blue;
        yield return new WaitForSeconds(intangibleDuration);
        isIntangible = false;
        playerCollider.isTrigger = false;
        playerRb.useGravity = true;
        powerUpIndicator.color = Color.black;
        UIM.UpdatePowerUp("", Color.white);
        Debug.Log("Intangible Dropped");

    }
    IEnumerator MultiplierTimer()
    {
        Debug.Log("Multiplier Picked");
        powerUpIndicator.color = Color.yellow;
        UIM.UpdatePowerUp(MULTIPLIER, Color.yellow);
        yield return new WaitForSeconds(multiplierDuration);
        isMultiplier = false;
        powerUpIndicator.color = Color.black;
        UIM.UpdatePowerUp("", Color.white);
        Debug.Log("Mulitplier Dropped");

    }

    void GetMuteStatus()
    {
        int muteStatus = PlayerPrefs.GetInt(IS_MUTED, 0);
        if (muteStatus == 0)
        {
            isMuted = false;
        }
        else
        {
            isMuted = true;
        }
    }
    public static void PlayerMute() {
        AudioSource bgMusic = Camera.main.GetComponent<AudioSource>();
        AudioSource playerSound = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        if (isMuted)
        {
            playerSound.mute = true;
            bgMusic.mute = true;
        }
        else
        {
            playerSound.mute = false;
            bgMusic.mute = false;
        }
    }

}

