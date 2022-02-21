using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingMoleMovement : MonoBehaviour
{

    public Transform player;
    private Transform mole;
    private Rigidbody2D rb;
    public Vector2 movement;

    public float moveSpeed = 5f;
    public Vector3 direction;
    private float initializationTime;

    private void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = this.GetComponent<Rigidbody2D>();
        mole = this.GetComponent<Transform>();
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

}
