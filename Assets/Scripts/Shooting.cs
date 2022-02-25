using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public LandScape landScape;
    private PlayerHealth playerHealth;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject rapidBulletPrefab;
    public CapsuleCollider2D playerCollider;
    private BoxCollider2D bulletCollider;

    private float manaTimer;
    private float manaTime;

    private float topSide;
    private float rightSide;
    private float leftSide;

    public int resistance = 0;
    public float bulletForce = 5f;
    public bool hasPowerup = false;
    private float shootTimer;
    public float normalShootTime;
    public float powerUpShootTime;
    public bool hasControl = true;
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

    void Start()
    {
        shootTimer = normalShootTime;
        manaTime = 0.1f;
        manaTimer = manaTime;

        playerHealth = GetComponent<PlayerHealth>();

        topSide = landScape.topSide;
        rightSide = landScape.rightSide;
        leftSide = landScape.leftSide;
    }

    void Update()
    {
        bulletCollider = bulletPrefab.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(9, 9);

        if (hasControl)
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


        if (hasControl == true)
        {

            if (manaTimer < 0 && Input.GetButton("Fire2") && playerHealth.currentMana >= 50)
            {
                playerHealth.UseMana(50);
                manaTimer = manaTime;

                for (int i = 0; i < playerHealth.manaAttackLevel; i++)
                {
                    ManaAttack();
                }
            }

        }
        manaTimer -= Time.deltaTime;
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

    public void ManaAttack()
    {
        FindObjectOfType<AudioManager>().Play("PowerUpManaUse");

        Vector3 spawnLocation = new Vector3(Random.Range(leftSide, rightSide), topSide, 0);

        GameObject bullet = Instantiate(bulletPrefab, spawnLocation, transform.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetResistance(resistance);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(0, -1, 0) * bulletForce / 2, ForceMode2D.Impulse);

        rb.rotation = rb.rotation + 180;
    }
}
