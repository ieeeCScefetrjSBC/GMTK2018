using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Parallax : MonoBehaviour {
 
    private Renderer back;
    private Vector2 vel;
    private string bklayer;
    private GameObject Player;
    private Rigidbody2D rb;
    private Vector2 pvel;
 
    void Start () {
 
        Player = GameObject.FindWithTag("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        pvel = rb.velocity;

        back = GetComponent<Renderer>();
        bklayer = this.gameObject.tag;
 
    }
 
    void Update () {
 
 
 
        switch (bklayer)
        {
 
        case "bk3":
            vel = -0.1f*pvel;
            break;
 
        case "bk2":
            vel = -0.5f*pvel;
            break;
 
        case "bk1":
            vel = -1.0f*pvel;
            break;
 
        }
 
        Vector2 offset = vel * Time.deltaTime;
 
        back.material.mainTextureOffset += offset;
 
    }
}