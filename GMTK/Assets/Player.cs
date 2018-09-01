using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Vel;
    Rigidbody2D rb;
	
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
        Vector2 vel = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) vel += Vector2.up;
        if (Input.GetKey(KeyCode.S)) vel += Vector2.down;
        if (Input.GetKey(KeyCode.A)) vel += Vector2.left;
        if (Input.GetKey(KeyCode.D)) vel += Vector2.right;

        rb.velocity = vel.normalized * Vel;
    }
}
