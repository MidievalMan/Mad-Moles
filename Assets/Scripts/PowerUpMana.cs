using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMana : MonoBehaviour
{
    public float duration = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {

        //Instantiate(pickupEffect, transform.position, transform.rotation); - Not yet, Maybe later
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.manaAttackLevel += 1;
        playerHealth.manaRate += 3f;
        playerHealth.manaRate *= 5;
        FindObjectOfType<AudioManager>().Play("PowerUpPickedup");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerHealth.manaRate /= 5;

        Destroy(gameObject);
    }

}
