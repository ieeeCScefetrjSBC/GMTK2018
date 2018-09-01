using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public bool defenceWindow;
    [HideInInspector]
    public Inimigo Atacker;

    public float maxHealth = 100f;
    public float fireDamagePerSec = 5f;
    public float stunTime = 3f;
    public float onFireTime = 2f;
    private float health;
    private float timeWasStunned;
    private float timeSetOnFire;

    private bool isStunned = false;
    private bool isOnFire = false;
    private bool canParry = true;

    public bool IsStunned { get; set; }
    public bool IsOnFire { get; set; }

    public static Player Instance;
    public float Vel;
    public float parryDelay = 1.5f;
   
    Rigidbody2D rb;

    void Start()
    {
        health = maxHealth;
        timeWasStunned = -stunTime;
        timeSetOnFire = -onFireTime;

        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        if (isStunned && Time.time - timeWasStunned > stunTime)
            isStunned = false;

        if (isOnFire)
        {
            if (Time.time - timeSetOnFire > onFireTime)
                isOnFire = false;
            else
                health -= fireDamagePerSec * Time.deltaTime;
        }

        if (health <= 0f)
            Kill();
    }

    private void FixedUpdate()
    {
        Vector2 vel = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) vel += Vector2.up;
        if (Input.GetKey(KeyCode.S)) vel += Vector2.down;
        if (Input.GetKey(KeyCode.A)) vel += Vector2.left;
        if (Input.GetKey(KeyCode.D)) vel += Vector2.right;
        rb.velocity = vel.normalized * Vel;

        if (Input.GetKey(KeyCode.Return)) Parry();
    }

    public void Parry()
    {
        if (canParry)
        {
            if (defenceWindow) Atacker.Stop();
        }
        canParry = false;
        Invoke("ReParry", parryDelay);
    }

    public void ReParry()
    {
        canParry = true;
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

    public void Stun(float stunTime)
    {
        timeWasStunned = Time.time;
        this.stunTime = stunTime;
        isStunned = true;
    }

    public void SetOnFire()
    {
        timeSetOnFire = Time.time;
        isOnFire = true;
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
