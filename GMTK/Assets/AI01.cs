using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI01 : Inimigo {

    Collider2D shortAtack;
    Collider2D mediumAtack;
    Collider2D longAtack;

	void Start () {
        Collider2D[] Atacks = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D at in Atacks) {
            if (at.tag == "Short") shortAtack = at;
            if (at.tag == "Medium") mediumAtack = at;
            if (at.tag == "Long") longAtack = at;
        }
	}
	
	void Update () {
		
	}

    private Vector2 Follow() {
        return (((Vector2)Player.Instance.transform.position) - ((Vector2)transform.position)).normalized;
    }
}
