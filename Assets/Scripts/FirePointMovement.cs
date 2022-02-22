using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointMovement : MonoBehaviour
{

    public Transform crossHair;
    public Transform player;
    //public Rigidbody2D rb;

    private Vector2 direction;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        direction = crossHair.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //rb.rotation = angle;
        transform.position = player.position;
    }



}
