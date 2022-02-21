using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioManager sound;
    public float timer = 0;
    public bool playSound = true;


    private void FixedUpdate()
    {
        if (playSound == true)
        {
            timer++;
            if (timer >= 400 && sound != null)
            {

                AudioSource mommySound = sound.GetComponent<AudioSource>();
                mommySound.pitch = Random.Range(.35f, .45f);
                mommySound.volume = Random.Range(.01f, .1f);
                FindObjectOfType<AudioManager>().Play("MommyMoleSpawnQuiet");

                timer = 0;
            }
        }


    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MoleRoom");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        Score.score = 0f;
    }

    public void StopSound()
    {
        FindObjectOfType<AudioManager>().Stop("MommyMoleSpawnQuiet");
    }

    public void MenuSound()
    {
        FindObjectOfType<AudioManager>().Play("MoleHurt");
    }

    public void PlaySoundFalse()
    {
        playSound = false;
    }

    public void PlaySoundTrue()
    {
        playSound = true;
    }

}
