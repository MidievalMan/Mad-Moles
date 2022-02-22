using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerUpShoot;
    public GameObject powerUpMana;
    public GameObject powerUpHealth;
    public GameObject powerUpResistance;

    public float powerUpTimer;
    private float spawnRate;

    void Start()
    {
        spawnRate = Random.Range(20, 30);
        powerUpTimer = spawnRate;

        /*
        SpawnShootPower();
        SpawnManaPower();
        SpawnHealthPower();
        SpawnResistancePower();
        SpawnShootPower();
        SpawnManaPower();
        SpawnHealthPower();
        SpawnResistancePower();
        */
    }

    void Update()
    {
        if (powerUpTimer <= 0)
        {
            
            if (spawnRate > 7)
            {
                spawnRate -= Random.Range(0, 6);
            } else
            {
                spawnRate = 7f;
            }

            powerUpTimer = spawnRate;

            int num = Random.Range(1, 5);

            switch(num)
            {
                case 1:
                    SpawnShootPower();
                    break;
                case 2:
                    SpawnManaPower();
                    break;
                case 3:
                    SpawnHealthPower();
                    break;
                case 4:
                    SpawnResistancePower();
                    break;
            }
        }

        powerUpTimer -= Time.deltaTime;
    }

    void SpawnShootPower()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.9f), Random.Range(-.8f, .9f), 0);

        FindObjectOfType<AudioManager>().Play("PowerUpPickup");

        Instantiate(powerUpShoot, spawnLocation, transform.rotation);
    }

    void SpawnManaPower()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.9f), Random.Range(-.8f, .9f), 0);

        Instantiate(powerUpMana, spawnLocation, transform.rotation);
    }

    void SpawnHealthPower()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.9f), Random.Range(-.8f, .9f), 0);

        Instantiate(powerUpHealth, spawnLocation, transform.rotation);
    }

    void SpawnResistancePower()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.9f), Random.Range(-.8f, .9f), 0);

        Instantiate(powerUpResistance, spawnLocation, transform.rotation);
    }

}
