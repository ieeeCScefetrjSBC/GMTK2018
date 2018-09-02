using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {

        Player playerScript = collision.gameObject.GetComponent<Player>();
        if(playerScript != null) playerScript.SetOnFire();

        Inimigo enemyScript = collision.gameObject.GetComponent<Inimigo>();
        if (enemyScript != null)
        {
            if (!enemyScript.isOnFire)
                enemyScript.SetOnFire();
        }
        
    }
}