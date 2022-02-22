using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject rapidBulletPrefab;
    public BoxCollider2D playerCollider;
    private BoxCollider2D bulletCollider;

    public int resistance = 0;
    public float bulletForce = 5f;
    public bool hasPowerup = false;
    private float shootTimer;
    public float normalShootTime = 0.2f;
    public float powerUpShootTime = 0.05f;
    public bool hasShootControl = true;
    private float powerUpTimer;
    public float PowerUpTimer {
        get
        {
            return powerUpTimer;
        }
        set
        {
            powerUpTimer = value;
        }
    }

    void Start() { shootTimer = normalShootTime; }

    void Update()
    {
        bulletCollider = bulletPrefab.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(9, 9);

        if (hasShootControl)
        {
            if (Input.GetButton("Fire1") && shootTimer <= 0f)
            {
                Shoot(powerUpTimer > 0);
            }
        }

        if(powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
        }
        shootTimer -= Time.deltaTime;
    }

    void Shoot(bool powerUp)
    {
        FindObjectOfType<AudioManager>().Play("Shoot");

        GameObject bullet;
        if (powerUp)
        {
            shootTimer = powerUpShootTime;
            bullet = Instantiate(rapidBulletPrefab, firePoint.position, firePoint.rotation);
        } else
        {
            shootTimer = normalShootTime;
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetResistance(resistance);



        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        rb.rotation = rb.rotation + 180;
    }

}
