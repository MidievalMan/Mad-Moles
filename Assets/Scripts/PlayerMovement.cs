using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    private float MOVE_SPEED;
    public float moveSpeed;
    public int walkTimerAmount = 10;
    private int walkTimer = 0;
    public bool hasControl = true;

    public Rigidbody2D rb;
    public Animator animator;


    public Vector2 movement;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BemyBMowdown");

        MOVE_SPEED = moveSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale *= 2;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale /= 2;
        }

        // Receive Player Input
        if (hasControl == true)
        {

            if (Input.GetButton("Jump"))
            {
                moveSpeed = MOVE_SPEED * 1.5f;
                animator.SetBool("Running", true);
            }
            else
            {
                moveSpeed = MOVE_SPEED;
                animator.SetBool("Running", false);
            }
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            if(movement.magnitude < 0.05f)
            {
                movement = Vector2.zero;
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }


    }
    void FixedUpdate()
    {
        // Execute Player Movement
        if (hasControl == true)
        {

            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

            if (movement.x != 0 || movement.y != 0)
            {
                if (walkTimer <= 0)
                {
                    FindObjectOfType<AudioManager>().Play("PlayerMove");
                    walkTimer += walkTimerAmount;
                }

                walkTimer -= 1;

            }
        }

    }
}