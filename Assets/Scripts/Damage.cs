using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public HealthBar healthBar;
    public GameObject player;

    public float amount = 10f;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        GameObject health = GameObject.FindWithTag("HealthBar");
        healthBar = health.GetComponent<HealthBar>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Mole"))
        {

            if (playerHealth != null)
            {

                if (playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    playerHealth.currentHealth += amount;
                    healthBar.SetHealth(playerHealth.currentHealth);
                }

            }
            //FindObjectOfType<AudioManager>().Play("MoleHurt");
            Destroy(col.gameObject);

        }
        if (col.gameObject.CompareTag("MommyMole"))
        {

            if (playerHealth != null)
            {

                if (playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    playerHealth.currentHealth += amount * 10;
                    healthBar.SetHealth(playerHealth.currentHealth);
                }

            }

            //FindObjectOfType<AudioManager>().Play("MoleHurt");
            Destroy(col.gameObject);
        }
    }

}
