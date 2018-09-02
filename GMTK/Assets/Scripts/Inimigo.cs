using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {

    public float maxHealth = 100f;
    public float fireDamagePerSec = 5f;
    public float stunTime = 3f;
    public float onFireTime = 2f;
    public float forceStun;

    private float health;
    private float timeWasStunned;
    private bool right;
    private Vector3 facingOld;
    private Vector3 facingNew;


    protected bool isStunned = false;
    public bool isOnFire = false;

    //public bool IsStunned { get; set; }
    //public bool IsOnFire { get; set; }

    protected float doubleStun;

    protected bool possibleHit;
    protected bool hitting;

    public SpriteRenderer sp;
    protected Rigidbody2D rb;

    protected Sprite Original;
    public Sprite Ataque;

    public void StartCode()
    {
        health = maxHealth;
        facingOld = transform.localScale;
        facingNew = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void UpdateCode()
    {

        if (rb.velocity.x != 0) right = rb.velocity.x > 0;

        if (right) transform.localScale = facingOld;
        else transform.localScale = facingNew;


        if (isOnFire)
        {
            if (isOnFire)
                health -= fireDamagePerSec * Time.deltaTime;
        }

        if (health <= 0f)
            Kill();
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

    public void Stun(float stunTime)
    {
        if (!isStunned)
        {
            isStunned = true;
            Invoke("UnStun", stunTime);
        }
        else
        {
            doubleStun = stunTime;
        }
    }

    public void UnStun()
    {
        if (doubleStun > 0)
        {
            Invoke("UnStun", doubleStun);
            doubleStun = 0;
        }
        else
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

    public void Stop(Vector2 dir) {
        CancelInvoke();
        sp.sprite = Original;
        hitting = false;
        Player.Instance.defenceWindow = false;
        Player.Instance.Atacker.Remove(this);
        Stun(stunTime);
        rb.AddForce(forceStun* dir.normalized);
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
