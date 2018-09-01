using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    public float Vel;
    Vector2 vel;
    Rigidbody2D rb;

    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        vel = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) vel += Vector2.up;
        if (Input.GetKey(KeyCode.S)) vel += Vector2.down;
        if (Input.GetKey(KeyCode.A)) vel += Vector2.left;
        if (Input.GetKey(KeyCode.D)) vel += Vector2.right;

    }

    private void FixedUpdate()
    {
        rb.velocity = vel.normalized * Vel;
    }
}
