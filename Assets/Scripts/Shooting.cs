using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public BoxCollider2D playerCollider;
    private BoxCollider2D bulletCollider;

    public float bulletForce = 5f;
    public bool hasPowerup = false;
    private int timer = 1;
    public int timerCount = 1;
    public bool hasShootControl = true;

    void Update()
    {
        bulletCollider = bulletPrefab.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(9, 9);

        if (hasShootControl == true)
        {

            if (Input.GetButtonDown("Fire1") && hasPowerup == false)
            {
                Shoot();
            }

        }

    }

    private void FixedUpdate()
    {

        if (hasShootControl == true)
        {
            if (hasPowerup == true)
            {
                if (Input.GetButton("Fire1") && timer <= 0)
                {

                    timer = timerCount;
                    Shoot();
                }
            }
            timer -= 1;
        }

    }

    void Shoot()
    {
        //FindObjectOfType<AudioManager>().Play("Shoot");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        rb.rotation = rb.rotation + 180;

        
    }

}
