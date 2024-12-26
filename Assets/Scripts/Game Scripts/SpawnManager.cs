using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] pickUps;
    public GameObject[] ammoPickUps;
    public GameObject[] coins;
    private int[] posX;
    private float distance = 40.0f;
    private int spawnX = 5;
    private UIManager UIM;
    
    void Start()
    {
        UIM = GameObject.Find("UIManager").GetComponent<UIManager>();
        posX =new int[3];
        posX[0] = -spawnX;
        posX[1] = 0;
        posX[2] = spawnX;
        InvokeRepeating(nameof(SpawnEnemies), 5, 3);
        InvokeRepeating(nameof(spawnPickups), 10, 21);
        InvokeRepeating(nameof(spawnAmmoPickUps), 5, 10);
        InvokeRepeating(nameof(spawnCoins), 3, 5);
    }



    private void SpawnEnemies() {
        if (!UIM.isGameOver)
        {
            int rSpawnX = Random.Range(0, 3);
            int rSpawnEnemy = Random.Range(0, enemies.Length);
            float spawnZ = GameObject.Find("Player").transform.position.z + distance;
            Vector3 spawnPos = new Vector3(posX[rSpawnX], enemies[rSpawnEnemy].transform.position.y, spawnZ);
            Instantiate(enemies[rSpawnEnemy], spawnPos, enemies[rSpawnEnemy].transform.rotation);
        }

    }
    private void spawnPickups() {
        if (!UIM.isGameOver)
        {
            int rSpawnX = Random.Range(0, 3);
            int rSpawnPickUp = Random.Range(0, pickUps.Length);
            float spawnZ = GameObject.Find("Player").transform.position.z + distance;
            Vector3 spawnPos = new Vector3(posX[rSpawnX], pickUps[rSpawnPickUp].transform.position.y, spawnZ);
            Instantiate(pickUps[rSpawnPickUp], spawnPos, pickUps[rSpawnPickUp].transform.rotation);
        }
    }
    private void spawnAmmoPickUps()
    {
        if (!UIM.isGameOver)
        {
            int rSpawnX = Random.Range(0, 3);
            int rAmmoSpawnPickUp = Random.Range(0, ammoPickUps.Length);
            float spawnZ = GameObject.Find("Player").transform.position.z + distance;
            Vector3 spawnPos = new Vector3(posX[rSpawnX], ammoPickUps[rAmmoSpawnPickUp].transform.position.y, spawnZ);
            Instantiate(ammoPickUps[rAmmoSpawnPickUp], spawnPos, ammoPickUps[rAmmoSpawnPickUp].transform.rotation);
        }

        
    }
    private void spawnCoins() 
    {
        if (!UIM.isGameOver)
        {
            int randCoin = Random.Range(0, coins.Length);
            int randSpawnX = Random.Range(0, 3);
            float spawnZ = GameObject.Find("Player").transform.position.z + distance;
            Vector3 spawnPos = new Vector3(posX[randSpawnX], coins[randCoin].transform.position.y, spawnZ);
            Instantiate(coins[randCoin], spawnPos, coins[randCoin].transform.rotation);
        }
    }
  


}
