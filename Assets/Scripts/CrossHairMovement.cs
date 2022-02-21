using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairMovement : MonoBehaviour
{

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float moveSpeed = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
