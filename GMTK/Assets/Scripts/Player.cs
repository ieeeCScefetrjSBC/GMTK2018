using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public bool defenceWindow;
    [HideInInspector]
    public List<Inimigo> Atacker;

    public float maxHealth = 100f;
    public float fireDamagePerSec = 5f;
    public float stunTime = 3f;
    public float onFireTime = 2f;
    private float health;

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

        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

       

        if (isOnFire)
        {
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
            Inimigo[] Atackers = new Inimigo[Atacker.Count];
            Atacker.CopyTo(Atackers);
            if (defenceWindow) foreach(Inimigo At in Atackers) At.Stop();
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

        isStunned = true;
        Invoke("UnStun", stunTime);

    }

    public void UnStun()
    {
        isStunned = false;
    }

    public void SetOnFire()
    {
        isOnFire = true;
        Invoke("UnSetOnFire", onFireTime);
    }

    public void UnSetOnFire()
    {
        isOnFire = false;
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
