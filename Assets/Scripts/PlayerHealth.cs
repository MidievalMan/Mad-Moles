using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 500;
    public float currentHealth;
    public float regen = 5f;
    private float regenTimer;
    private float regenTime = 1;

    public int maxMana = 1000;
    public float currentMana;
    public float bulletForce = 5f;
    public int manaAttackLevel = 1;
    public float manaRate = 1;
    //public float manaRateMultiplier = 1.01f;

    public MoleSpawner moleSpawner;
    public HealthBar healthBar;
    public ManaBar manaBar;
    public GameObject bulletPrefab;
    public TextMeshProUGUI fallenText;
    public PlayerMovement playerMovement;
    public Shooting shooting;
    public Animator playerAnimator;
    public Rigidbody2D rb;

    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        regenTimer = regenTime;
    }

    private void Update()
    {
        if(regenTimer < 0 && currentHealth + regen < maxHealth)
        {
            regenTimer = regenTime;
            currentHealth += regen;
            healthBar.SetHealth(currentHealth);
        } else if (regenTimer < 0 && currentHealth < maxHealth)
        {
            regenTimer = regenTime;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }


        regenTimer -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRate;
            manaBar.SetMana(currentMana);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Mole"))
        {
            TakeDamage(1);
        }
        if (col.gameObject.tag.Equals("MommyMole"))
        {
            TakeDamage(3);
        }
        if (col.gameObject.tag.Equals("MoL"))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {

            shooting.hasControl = false;
            playerMovement.hasControl = false;
            playerMovement.moveSpeed = 0f;
            playerMovement.movement = new Vector2(0, 0);
            playerAnimator.SetBool("Running", false);
            playerAnimator.SetFloat("Speed", 0f);
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            fallenText.gameObject.SetActive(true);

            Invoke("LoadDeathMenu", 2);

            moleSpawner.moleTimer = 0;
            moleSpawner.spawnRate = 1;
        }
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;

        manaBar.SetMana(currentMana);
    }



    void LoadDeathMenu()
    {
        FindObjectOfType<AudioManager>().Stop("BemyBMowdown");
        FindObjectOfType<AudioManager>().Stop("Battle");
        FindObjectOfType<AudioManager>().Play("Fallen");
        SceneManager.LoadScene("DeathMenu");
    }

}
