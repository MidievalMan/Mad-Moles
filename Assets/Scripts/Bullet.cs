using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private int resistance = 0;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, .3f);
        if(resistance <= 0)
        {
            Destroy(gameObject);
        } else
        {
            resistance--;
        }
        
    }
    public void SetResistance(int resistance)
    {
        this.resistance = resistance;
    }

    public int GetResistance()
    {
        return resistance;
    }
}
