using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {

    public float  maxHealth        = 100f;
    public float  fireDamagePerSec = 5f;
    public float  stunTime         = 3f;
    public float  onFireTime       = 2f;

    private float health;
    private float timeWasStunned;
    private float timeSetOnFire;

    private bool  isStunned = false;
    private bool  isOnFire  = false;

    public  bool  IsStunned { get; set; }
    public  bool  IsOnFire  { get; set; }

    public void StartCode ()
    {
        health         = maxHealth;
        timeWasStunned = -stunTime;
        timeSetOnFire  = -onFireTime;
	}
	
	public void UpdateCode ()
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

    public void Damage (float damage)
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
