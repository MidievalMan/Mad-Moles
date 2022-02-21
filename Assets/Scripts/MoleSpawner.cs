using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{

    public GameObject diggingMole;
    public GameObject mommyMadMole;

    public float moleTimer = 0;
    public float spawnRate;
    float num = 0;

    void Update()
    {
        if (moleTimer < 0)
        {
            moleTimer += spawnRate;
            num = Random.Range(1, 1001);
            if (num != 1)
            {
                Spawn();
            }
            else
            {
                SpawnMommy();
                SpawnMommy();
                SpawnMommy();
                SpawnMommy();
                SpawnMommy();
                SpawnMommy();
                SpawnMommy();
                SpawnMommy();
            }
        }


    }

    private void FixedUpdate()
    {
        moleTimer -= 1;
        if (spawnRate > 5)
        {
            spawnRate -= spawnRate / 1000;
        }
        
    }
    

    void Spawn()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-.8f, .9f), 0);

        //FindObjectOfType<AudioManager>().Play("MoleSpawn");

        Instantiate(diggingMole, spawnLocation,transform.rotation);
    }

    void SpawnMommy()
    {

        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-.8f, .9f), 0);

        //FindObjectOfType<AudioManager>().Play("MommyMoleSpawn");

        Instantiate(mommyMadMole, spawnLocation, transform.rotation);
    }

}
