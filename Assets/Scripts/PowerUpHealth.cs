using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();

            FindObjectOfType<AudioManager>().Play("PowerUpPickedup");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            playerHealth.regen += 5;

            Destroy(gameObject, 2f);
        }
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

}
