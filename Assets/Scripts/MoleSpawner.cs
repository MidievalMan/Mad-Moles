using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoleSpawner : MonoBehaviour
{

    public LandScape landScape;
    public GameObject diggingMole;
    public GameObject mommyMadMole;
    public GameObject machoMole;
    public GameObject moL;
    public TMP_Text finalWaveText;

    private float topSide;
    private float rightSide;
    private float bottomSide;
    private float leftSide;

    public float moleTimer;
    public float spawnRate;
    float num = 0;

    int diggingMolesInWave = 20;
    private bool theEnd;
    private float roundTimer;

    private bool takingLongTime = false;

    void Start()
    {
        topSide = landScape.topSide;
        rightSide = landScape.rightSide;
        bottomSide = landScape.bottomSide;
        leftSide = landScape.leftSide;

        Vector3 spawnLocation = landScape.GetRandomLocation();
        roundTimer = 0f;

    }

    void Update()
    {
        if (moleTimer < 0 && !theEnd)
        {
            moleTimer += spawnRate;
            num = Random.Range(1, 1001);
            if (num != 1 && num > (roundTimer * 3))
            {
                Vector3 spawnLocation = landScape.GetRandomLocation();
                Spawn(spawnLocation);
            }
            else if (num == 2)
            {
                num = Random.Range(0, 6);
                if(num == 0)
                {
                    StartCoroutine(SpawnWave(10, 40, true, true));
                }
                else
                {
                    StartCoroutine(SpawnWave(10, 40, false, true));
                }

            }
            else if (num <= (roundTimer * 3))
            {
                Vector3 spawnLocation = landScape.GetRandomLocation();
                SpawnMacho(spawnLocation);
            }

        }

        moleTimer -= Time.deltaTime;
        roundTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (spawnRate > 0.03f)
        {
            spawnRate -= (spawnRate / 500);
        } else if(!theEnd)
        {
            spawnRate = 0.03f;
            StartCoroutine(FinalWave());
            theEnd = true;
        }
    }

    void Spawn(Vector3 spawnLocation)
    {
        FindObjectOfType<AudioManager>().Play("MoleSpawn");

        Instantiate(diggingMole, spawnLocation, transform.rotation);
    }

    void SpawnMacho(Vector3 spawnLocation)
    {
        FindObjectOfType<AudioManager>().Play("MoleSpawn");

        Instantiate(machoMole, spawnLocation, transform.rotation);
    }

    public IEnumerator SpawnWave(int least, int most, bool macho, bool endingMommyMole)
    {

        Vector3 loc = new Vector3(Random.Range(leftSide, rightSide), Random.Range(topSide, bottomSide), 0);

        diggingMolesInWave = Random.Range(least, most);

        for (int i = 0; i < diggingMolesInWave; i++)
        {
            loc += new Vector3(Random.Range(0.1f, 0.2f), Random.Range(0.1f, 0.2f), 0);
            if(macho)
            {
                SpawnMacho(loc);
            } else
            {
                Spawn(loc);
            }


            yield return new WaitForSeconds(0.04f);
        }

        if(endingMommyMole)
        {
            FindObjectOfType<AudioManager>().Play("MommyMoleSpawn");
            Instantiate(mommyMadMole, loc, transform.rotation);
        }

        yield return null;
    }

    IEnumerator FinalWave()
    {

        for (int i = 0; i < 5; i++)
        {
            finalWaveText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            finalWaveText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 200; i++)
        {
            Vector3 spawnLocation = landScape.GetRandomLocation();
            Spawn(spawnLocation);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 25; i++)
        {
            Vector3 spawnLocation = landScape.GetRandomLocation();
            FindObjectOfType<AudioManager>().Play("MommyMoleSpawn");
            Instantiate(mommyMadMole, spawnLocation, transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(6f);

        finalWaveText.gameObject.SetActive(true);

        finalWaveText.text = "An abomina-chin draws near...";
        yield return new WaitForSeconds(2f);

        finalWaveText.gameObject.SetActive(false);

        landScape.ChangeRoom("stone");

        FindObjectOfType<AudioManager>().Stop("BemyBMowdown");
        FindObjectOfType<AudioManager>().Play("Battle");
        FindObjectOfType<AudioManager>().Play("MoL");
        GameObject boss = Instantiate(moL, new Vector3(0, 0, 0), transform.rotation);

        while(boss != null)
        {
            Vector3 spawnLocation = new Vector3(0, 0, 0);


            if(takingLongTime)
            {
                GameObject mole;
                spawnLocation = new Vector3(leftSide, bottomSide, 0f);
                FindObjectOfType<AudioManager>().Play("MoleSpawn");
                mole = Instantiate(diggingMole, spawnLocation, transform.rotation);
                mole.GetComponent<DiggingMoleMovement>().moveSpeed *= 5;
                spawnLocation = new Vector3(rightSide, bottomSide, 0f);
                FindObjectOfType<AudioManager>().Play("MoleSpawn");
                mole = Instantiate(diggingMole, spawnLocation, transform.rotation);
                mole.GetComponent<DiggingMoleMovement>().moveSpeed *= 5;
                spawnLocation = new Vector3(rightSide, topSide, 0f);
                FindObjectOfType<AudioManager>().Play("MoleSpawn");
                mole = Instantiate(diggingMole, spawnLocation, transform.rotation);
                mole.GetComponent<DiggingMoleMovement>().moveSpeed *= 5;
                spawnLocation = new Vector3(leftSide, topSide, 0f);
                FindObjectOfType<AudioManager>().Play("MoleSpawn");
                mole = Instantiate(diggingMole, spawnLocation, transform.rotation);
                mole.GetComponent<DiggingMoleMovement>().moveSpeed *= 5;

                yield return new WaitForSeconds(Random.Range(0.15f, 0.25f));
            } else
            {
                int num = Random.Range(0, 4);
                if (num == 0)
                {
                    spawnLocation = new Vector3(leftSide, bottomSide, 0f);
                }
                else if (num == 1)
                {
                    spawnLocation = new Vector3(rightSide, bottomSide, 0f);
                }
                else if (num == 2)
                {
                    spawnLocation = new Vector3(rightSide, topSide, 0f);
                }
                else if (num == 3)
                {
                    spawnLocation = new Vector3(leftSide, topSide, 0f);
                }
                Instantiate(mommyMadMole, spawnLocation, transform.rotation);

                for (int i = 0; i < Random.Range(0, 15); i++)
                {
                    spawnLocation = landScape.GetRandomLocation();
                    Spawn(spawnLocation);
                }


                yield return new WaitForSeconds(Random.Range(0.35f, 0.7f));
            }

            if(Time.timeSinceLevelLoad > 100)
            {
                takingLongTime = true;
            }


        }

        yield return null;
    }



    public void HasWon()
    {
        FindObjectOfType<AudioManager>().Stop("BemyBMowdown");
        FindObjectOfType<AudioManager>().Stop("Battle");
        FindObjectOfType<AudioManager>().Play("Win");
        SceneManager.LoadScene("WinMenu");
    }

}
