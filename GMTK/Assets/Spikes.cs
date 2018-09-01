using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

	private GameObject enemy;
	private Inimigo e;

	[SerializeField]
	private float damage;
	private float stunTime;

	void Start () 
	{
		
	}
	
	void OnCollisionEnter(Collision col)
	{
		enemy = col.gameObject;
		e = enemy.GetComponent<Inimigo>();
		
		e.damage = damage;

		e.Stun(stunTime);

	}
}
