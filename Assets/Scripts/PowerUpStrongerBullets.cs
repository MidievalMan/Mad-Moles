using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStrongerBullets : MonoBehaviour
{
    public GameObject bullet;
    private Bullet bulletScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Shooting shooting = other.GetComponent<Shooting>();
            shooting.resistance += 1;

            FindObjectOfType<AudioManager>().Play("PowerUpPickedup");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            Destroy(gameObject, 2f);
        }
    }
}
