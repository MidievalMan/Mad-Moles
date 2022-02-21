using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShoot : MonoBehaviour
{

    //public GameObject pickupEffect;
    public float duration = 8f;

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
        Shooting shooting = player.GetComponent < Shooting > ();
        shooting.hasPowerup = true;
        //FindObjectOfType<AudioManager>().Play("PowerUpPickedup");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(duration);

        shooting.hasPowerup = false;

        Destroy(gameObject);
    }

}
