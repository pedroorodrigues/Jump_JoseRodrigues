using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float targetDistance = 7;
    private Rigidbody2D rb;
    private bool target;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    void Update()
    {
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, direction, Color.green);


        Debug.DrawRay(transform.position, playerTransform.position, Color.red);

        //Vector2 enemy = new Vector2(transform.position.x - player, transform.position.y);
        //Debug.DrawRay(enemy, Vector2.down, Color.red);
        if (target == true)
        {
            //Vector2 player = playerTransform.position;
            //Vector2 direcion = transform.position;
            //rb.velocity = (player - direcion);
        }
    }

}
