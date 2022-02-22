using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoleSpawner : MonoBehaviour
{

    public GameObject diggingMole;
    public GameObject mommyMadMole;
    public GameObject machoMole;
    public GameObject moL;
    public TMP_Text finalWaveText;
    public GameObject ground;

    public float moleTimer;
    public float spawnRate;
    float num = 0;

    int diggingMolesInWave = 20;
    private bool theEnd;
    private float roundTimer;

    void Start()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-0.8f, 0.9f), 0);
        roundTimer = 0f;
    }

    void Update()
    {
        if (moleTimer < 0 && !theEnd)
        {
            moleTimer += spawnRate;
            num = Random.Range(1, 1001);
            if (num != 1 && num > roundTimer + 100)
            {
                Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-.8f, .9f), 0);
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
            else if (num <= roundTimer + 100)
            {
                Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-.8f, .9f), 0);
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
            spawnRate -= (spawnRate / 750);
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

        Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-0.8f, 0.9f), 0);

        diggingMolesInWave = Random.Range(least, most);

        for (int i = 0; i < diggingMolesInWave; i++)
        {
            spawnLocation += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            if(macho)
            {
                SpawnMacho(spawnLocation);
            } else
            {
                Spawn(spawnLocation);
            }


            yield return new WaitForSeconds(0.04f);
        }

        if(endingMommyMole)
        {
            FindObjectOfType<AudioManager>().Play("MommyMoleSpawn");
            Instantiate(mommyMadMole, spawnLocation, transform.rotation);
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

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 400; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-0.8f, 0.9f), 0);
            Spawn(spawnLocation);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 30; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-0.8f, 0.9f), 0);
            FindObjectOfType<AudioManager>().Play("MommyMoleSpawn");
            Instantiate(mommyMadMole, spawnLocation, transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(6f);

        finalWaveText.gameObject.SetActive(true);
        finalWaveText.text = "Oh no...";
        yield return new WaitForSeconds(1f);
        finalWaveText.text = "It's near...";
        yield return new WaitForSeconds(1f);
        finalWaveText.text = "It's near...";
        yield return new WaitForSeconds(1f);
        finalWaveText.text = "FLY YOU FOOL!";
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < 6; i++)
        {
            finalWaveText.text = "&$#(@$(";
            yield return new WaitForSeconds(0.03f);
            finalWaveText.text = "@)(Q!%#";
            yield return new WaitForSeconds(0.03f);
            finalWaveText.text = "J#)*(&*";
            yield return new WaitForSeconds(0.03f);
            finalWaveText.text = "FN#)@(%";
            yield return new WaitForSeconds(0.03f);
        }

        finalWaveText.gameObject.SetActive(false);

        ground.GetComponent<SpriteRenderer>().color = Color.black;

        FindObjectOfType<AudioManager>().Stop("BemyBMowdown");
        FindObjectOfType<AudioManager>().Play("Battle");
        FindObjectOfType<AudioManager>().Play("MoL");
        GameObject boss = Instantiate(moL, new Vector3(0, 0, 0), transform.rotation);

        while(boss != null)
        {
            Vector3 spawnLocation = new Vector3(0, 0, 0);
            int num = Random.Range(0, 4);
            if(num == 0)
            {
                spawnLocation = new Vector3(-1.8f, -0.8f, 0f);
            }
            else if(num == 1)
            {
                spawnLocation = new Vector3(1.8f, -0.8f, 0f);
            }
            else if (num == 2)
            {
                spawnLocation = new Vector3(1.8f, 0.8f, 0f);
            }
            else if (num == 3)
            {
                spawnLocation = new Vector3(-1.8f, 0.8f, 0f);
            }
            Instantiate(mommyMadMole, spawnLocation, transform.rotation);

            for(int i = 0; i < Random.Range(0, 15); i++)
            {
                spawnLocation = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-0.8f, 0.9f), 0);
                Spawn(spawnLocation);
            }


            yield return new WaitForSeconds(Random.Range(0.35f, 0.7f));
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
