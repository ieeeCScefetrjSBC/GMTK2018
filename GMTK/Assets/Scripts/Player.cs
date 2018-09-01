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
    public float vel_Dodge = 2;
    private float Timer = 0;
    private float TimeToDParry = 0.7f;
    private float TimeDodge = 0.4f;
    private float DTimer = 0;

    private bool isStunned = false;
    private bool isOnFire = false;
    private bool canParry = true;
    private bool canDodge = true;
    private bool canDParry = true;
    private bool TimerActive = false;
    private bool Dodging = false;

    public bool IsStunned { get; set; }
    public bool IsOnFire { get; set; }

    public static Player Instance;
    public float Vel;
    public float parryDelay = 1.5f;
    public float DodgeDelay = 0.55f;
    public float DParryDelay = 0.55f;

    public Collider2D coli;

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

        if (TimerActive)
            Timer += Time.deltaTime;
        if (Dodging)
        {

            DTimer += Time.deltaTime;
            if (DTimer > TimeDodge)
            {
                Dodging = false;
                DTimer = 0;
            }

            if (Dodging) coli.enabled = false;
            else coli.enabled = true;
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

        if (!Dodging)
        {
            rb.velocity = vel.normalized * Vel;
        }

        if (Input.GetKey(KeyCode.Return)) Parry();

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.magnitude > 0.1f)
        {
            Dodging = true;
           // Debug.Log("OPA");
            TimerActive = true;
            Vector2 direction = this.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            if (Input.GetKeyDown(KeyCode.Return) && Timer < TimeToDParry)
            {
                DParry(direction);
                Timer = 0;
                TimerActive = false;
            }
            else if (Input.GetKeyDown(KeyCode.Return) && Timer > TimeToDParry)
            {
                Dodge(direction);
                Timer = 0;
                TimerActive = false;
                Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity);
            }
            else
            {
                Dodge(direction);
                Timer = 0;
                TimerActive = false;
            }
        }
    }

    public void Parry()
    {
        if (canParry)
        {
            Inimigo[] Atackers = new Inimigo[Atacker.Count];
            Atacker.CopyTo(Atackers);
            if (defenceWindow) foreach(Inimigo At in Atackers) At.Stop( (Dodging) ? (Player.Instance.transform.position - At.transform.position) : (At.transform.position - Player.Instance.transform.position));
        }
        canParry = false;
        Invoke("ReParry", parryDelay);
    }

    public void ReParry()
    {
        canParry = true;
    }

    public void ReDodge()
    {
        canDodge = true;
    }

    public void ReDParry()
    {
        canDParry = true;
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

    public void Dodge(Vector2 direction)
    {
        if (canDodge)
        {
            if (Mathf.Atan(direction.y / direction.x) > 1 || Mathf.Atan(direction.y / direction.x) < -1)
            {
                Debug.Log(Mathf.Atan(direction.y / direction.x));
               // Debug.Log("Dodgou Vert");
            }
            else
            {
                Debug.Log(Mathf.Atan(direction.y / direction.x));
               // Debug.Log("Dodgou Hor");
            }
            this.gameObject.GetComponent<Rigidbody2D>().velocity += direction.normalized * vel_Dodge;
            canDodge = false;
            Invoke("ReDodge", DodgeDelay);
        }
    }

    public void DParry(Vector2 direction)
    {
        if (canDParry)
        {
            if (Mathf.Atan(direction.y / direction.x) > 1 && Mathf.Atan(direction.y / direction.x) < -1)
            {
                Debug.Log(Mathf.Atan(direction.y / direction.x));
               // Debug.Log("DParrou Vert");
            }
            else
            {
                Debug.Log(Mathf.Atan(direction.y / direction.x));
               // Debug.Log("DParrou Hor");
            }
            this.gameObject.GetComponent<Rigidbody2D>().velocity += direction.normalized * vel_Dodge;
            canDParry = false;
            Invoke("ReDParry", DParryDelay);
        }
    }
}
