using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingMoleMovement : MonoBehaviour
{

    public int health;

    public Transform player;
    public MoleSpawner moleSpawner;
    private Transform mole;
    private Rigidbody2D rb;
    public Animator anim;
    private CapsuleCollider2D col;
    public Vector2 movement;

    public float moveSpeed;
    public Vector3 direction;
    private float initializationTime;

    private float elapsedTime;
    bool dashing = false;
    bool dashTrigger = true;
    bool spawning = false;
    bool spawnTrigger = true;
    float maxDashTime = 10f;
    bool pauseColoring;

    private void Start()
    {


        initializationTime = Time.timeSinceLevelLoad;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = this.GetComponent<Rigidbody2D>();
        mole = this.GetComponent<Transform>();
        col = this.GetComponent<CapsuleCollider2D>();

        float num = Random.Range(0.9f, 1.1f);
        moveSpeed *= num;

        StartCoroutine(EnableCollider());
    }
    void Update()
    {

        direction = player.position - transform.position;
        direction.Normalize();


        if (direction.x < 0)
        {
            transform.localScale = new Vector3 (1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        movement = direction;

        if(dashing)
        {
            StartCoroutine(Dash());
        }
        if (spawning)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Dash()
    {
        dashing = false;

        pauseColoring = true;
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
        if (anim != null)
        {
            anim.SetBool("dashWarning", true);
        }

        yield return new WaitForSeconds(0.3f);
        FindObjectOfType<AudioManager>().Play("MoL_Dash");
        yield return new WaitForSeconds(0.2f);


        pauseColoring = false;
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color((255 / 255), ((health / 7f) / 255), ((health / 7f) / 255));
        if (anim != null)
        {
            anim.SetBool("dashWarning", false);
        }



        moveSpeed *= 10f;
        yield return new WaitForSeconds(0.5f);
        moveSpeed /= 10f;


        yield return new WaitForSeconds(Random.Range(1f, maxDashTime));
        dashing = true;
    }


    IEnumerator Spawn()
    {
        spawning = false;
        StartCoroutine(moleSpawner.SpawnWave(5, 10, true, false));
        yield return new WaitForSeconds(Random.Range(0f, 5f));
        spawning = true;
    }


    private void FixedUpdate()
    {

        float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;

        if (timeSinceInitialization > 1f)
        {
            MoveMole(movement);
        }

    }
    void MoveMole(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        col.enabled = true;
    }

    public void TakeDamage()
    {
        health--;
        if(this.CompareTag("MoL")) {
            if(health < 1800)
            {
                if(!pauseColoring)
                {
                    transform.gameObject.GetComponent<SpriteRenderer>().color = new Color((255 / 255), ((health / 7f) / 255), ((health / 7f) / 255));
                }
                
            }
            if(health < 1350 && dashTrigger)
            {
                dashing = true;
                dashTrigger = false;
            }
            if (health < 900 && spawnTrigger)
            {
                spawning = true;
                spawnTrigger = false;
            }
            if (health < 450)
            {
                maxDashTime = 3f;
            }
            if (health <= 0)
            {
                moleSpawner.HasWon();
            }
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
/*
    IEnumerator ScaleEffect()
    {
        elapsedTime = 0;

        bool decrease = true;
        bool increase = false;
        float t = 1f;

        while (true)
        {
            if(t > 1f)
            {
                decrease = true;
                increase = true;
            }
            if(t < 0.05f)
            {
                decrease = false;
                increase = true;
            }

            if(decrease)
            {
                t -= (0.02f);
            }
            if(increase)
            {
                t += (0.02f);
            }

            transform.localScale += new Vector3(Mathf.Lerp(0, 1, t), Mathf.Lerp(0, 1, t), transform.localScale.z);

            yield return new WaitForSeconds(0.02f);
        }


    }
*/

}
