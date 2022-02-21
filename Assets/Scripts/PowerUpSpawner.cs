using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerUpShoot;
    public GameObject powerUpMana;

    public float powerUpTimer = 0;
    float i;
    public float spawnRate = 20;
    public float spawnRateR = 5;



    void Update()
    {
        if (powerUpTimer > i)
        {
            i += spawnRate;
            if (spawnRate > 10)
            {
                spawnRate -= spawnRateR;
            }
            SpawnShootPower();
            SpawnManaPower();
        }
    }

    private void FixedUpdate()
    {
        powerUpTimer = powerUpTimer + 1 * Time.deltaTime;
    }


    void SpawnShootPower()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.9f), Random.Range(-.8f, .9f), 0);

        //FindObjectOfType<AudioManager>().Play("PowerUpPickup");

        Instantiate(powerUpShoot, spawnLocation, transform.rotation);
    }

    void SpawnManaPower()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.9f), Random.Range(-.8f, .9f), 0);

        Instantiate(powerUpMana, spawnLocation, transform.rotation);
    }

}
