using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShoot : MonoBehaviour
{

    //public GameObject pickupEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            //Instantiate(pickupEffect, transform.position, transform.rotation); - Not yet, Maybe later

            Shooting shooting = other.GetComponent<Shooting>();

            shooting.PowerUpTimer += 8f;

            FindObjectOfType<AudioManager>().Play("PowerUpPickedup");

            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            Destroy(gameObject, 2f);
        }
    }




}
