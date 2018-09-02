using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traphole : MonoBehaviour
{

	private Inimigo e;

	
	void OnTriggerEnter2D(Collider2D enemy)
	{
		e = enemy.GetComponent<Inimigo>();
		if(e != null) e.Kill();
	}
}
