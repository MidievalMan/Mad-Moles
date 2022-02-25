using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public LandScape landScape;
    public GameObject powerUpShoot;
    public GameObject powerUpMana;
    public GameObject powerUpHealth;
    public GameObject powerUpResistance;

    public GameObject puPromise;
    private float puPromiseTimer;
    private float puPromiseTime;

    public float powerUpTimer;
    private float spawnRate;

    void Start()
    {
        puPromiseTime = Random.Range(10f, 30f);
        puPromiseTimer = puPromiseTime;

        spawnRate = Random.Range(15, 20);
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
        SpawnPUPromise();
        */
        
    }

    void Update()
    {
        if (powerUpTimer <= 0)
        {
            
            if (spawnRate > 7f)
            {
                spawnRate -= Random.Range(1, 6);
            } else
            {
                spawnRate = 7f;
            }

            powerUpTimer = spawnRate;

            SpawnPU();
        }

        if(puPromiseTimer < 0)
        {
            SpawnPUPromise();
            puPromiseTime = Random.Range(10f, 30f);
            puPromiseTimer = puPromiseTime;
        }

        puPromiseTimer -= Time.deltaTime;
        powerUpTimer -= Time.deltaTime;
    }

    private void SpawnPUPromise()
    {
        Vector3 spawnLocation = landScape.GetRandomLocation();

        FindObjectOfType<AudioManager>().Play("PowerUpPickup");

        Instantiate(puPromise, spawnLocation, transform.rotation);
    }

    public void SpawnPU()
    {
        int num = Random.Range(1, 5);

        switch (num)
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

    void SpawnShootPower()
    {
        Vector3 spawnLocation = landScape.GetRandomLocation();

        FindObjectOfType<AudioManager>().Play("PowerUpPickup");

        Instantiate(powerUpShoot, spawnLocation, transform.rotation);
    }

    void SpawnManaPower()
    {
        Vector3 spawnLocation = landScape.GetRandomLocation();

        Instantiate(powerUpMana, spawnLocation, transform.rotation);
    }

    void SpawnHealthPower()
    {
        Vector3 spawnLocation = landScape.GetRandomLocation();

        Instantiate(powerUpHealth, spawnLocation, transform.rotation);
    }

    void SpawnResistancePower()
    {
        Vector3 spawnLocation = landScape.GetRandomLocation();

        Instantiate(powerUpResistance, spawnLocation, transform.rotation);
    }

}
