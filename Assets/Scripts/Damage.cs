using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public HealthBar healthBar;
    public GameObject player;

    public float amount = 0.5f;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        GameObject health = GameObject.FindWithTag("HealthBar");
        healthBar = health.GetComponent<HealthBar>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Mole"))
        {
            col.gameObject.GetComponent<DiggingMoleMovement>().TakeDamage();
            //FindObjectOfType<AudioManager>().Play("MoleHurt");
        }
        if (col.gameObject.CompareTag("MommyMole"))
        {
            //FindObjectOfType<AudioManager>().Play("MoleHurt");
            col.gameObject.GetComponent<DiggingMoleMovement>().TakeDamage();
        }
        if (col.gameObject.CompareTag("MoL"))
        {
            //FindObjectOfType<AudioManager>().Play("MoleHurt");
            col.gameObject.GetComponent<DiggingMoleMovement>().TakeDamage();
        }
    }

}
