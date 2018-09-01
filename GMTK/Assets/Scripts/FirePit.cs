using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        string colliderTag = collision.gameObject.tag;

        // LEMBRAR DE CRIAR TAGS
        if (colliderTag == "Player")
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            //playerScript.SetOnFire();
        }
        if (colliderTag == "Enemy")
        {
            Inimigo enemyScript = collision.gameObject.GetComponent<Inimigo>();
            //if (!enemyScript.IsOnFire)
            //  enemyScript.SetOnFire();
        }
    }
}