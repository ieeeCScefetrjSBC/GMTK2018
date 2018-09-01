using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

	private GameObject enemy;
	private Inimigo e;

	[SerializeField]
	private float damage;
    [SerializeField]
    private float stunTime;

	void Start () 
	{
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		enemy = col.gameObject;
		e = enemy.GetComponent<Inimigo>();
        if (e != null)
        {

            e.Damage(damage);
          //  Debug.Log("")

            e.Stun(stunTime);
        }
	}
}
