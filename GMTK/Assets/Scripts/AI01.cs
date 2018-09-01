using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI01 : Inimigo
{

    Collider2D shortAtack;
    Collider2D mediumAtack;
    Collider2D longAtack;

    [SerializeField]
    float walkingTime;
    [SerializeField]
    float thinkingTime;
    [SerializeField]
    float tellTime;
    float restartTimer;
    float thinkingTimer;

    

    [SerializeField]
    float Vel;
    [SerializeField]
    float MaxVel;
    [SerializeField]
    float smallValue;
    


    

    void Start()
    {
        StartCode();
        rb = GetComponent<Rigidbody2D>();
        //sp.GetComponent<SpriteRenderer>();
        Original = sp.sprite;

        Collider2D[] Atacks = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D at in Atacks)
        {
            if (at.tag == "Short") shortAtack = at;
            if (at.tag == "Medium") mediumAtack = at;
            if (at.tag == "Long") longAtack = at;
        }
    }

    void Update()
    {
        UpdateCode();
        int Turn = (int)(walkingTime + thinkingTime);

        if (!isStunned)
        {
            if (!possibleHit)
            {
                string seen = Look((((Vector2)Player.Instance.transform.position) - ((Vector2)transform.position)), 1000);
                Debug.Log(seen);
                if (seen == "Player")
                {
                    if ((Time.timeSinceLevelLoad - restartTimer) % Turn < walkingTime)
                    {
                        Move(Front(0.7f));
                        restartTimer = Time.timeSinceLevelLoad;
                    }
                    else
                        Move(Vector2.zero);
                }
                else
                {
                    if (Look(Vector2.up) != "Trap") Move(Vector2.up);
                    else if (Look(Vector2.down) != "Trap") Move(Vector2.up);
                    else Move(Vector2.left);
                }


            }
            else
            {
                Move(Vector2.zero);
                if (!hitting && (thinkingTimer + thinkingTime < Time.timeSinceLevelLoad))
                {
                    Atack();
                    hitting = true;
                }
            }
        }
        else
        {
            Move(Vector2.zero);

        }
    }

    string Look(Vector2 dir, float dis = 1f) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dis);

        if (hit.transform != null)
        {
            return hit.transform.name;
        }
        return "null";
    }

    private void Atack()
    {
        sp.sprite = Ataque;
        Player.Instance.defenceWindow = true;
        Player.Instance.Atacker.Add(this);
        Invoke("AtackEnd", tellTime);
    }

    private void AtackEnd()
    {
        sp.sprite = Original;
        hitting = false;
        Player.Instance.defenceWindow = false;
        Player.Instance.Atacker.Remove(this);
        thinkingTimer = Time.timeSinceLevelLoad;
    }

    private Vector2 Follow()
    {
        Vector2 dir = (((Vector2)Player.Instance.transform.position) - ((Vector2)transform.position));

        if (dir.magnitude > smallValue)
            return dir.normalized;
        else
            return Vector2.zero;
    }

    private Vector2 Front(float distance)
    {
        Vector2 dir = (((Vector2)Player.Instance.transform.position + (distance * Vector2.right)) - ((Vector2)transform.position));

        if (dir.magnitude > smallValue)
            return dir.normalized;
        else
            return Vector2.zero;
    }

    private Vector2 Back(float distance)
    {
        Vector2 dir = (((Vector2)Player.Instance.transform.position + (distance * Vector2.left)) - ((Vector2)transform.position));

        if (dir.magnitude > smallValue)
            return dir.normalized;
        else
            return Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D ou)
    {
        Player pl = ou.GetComponent<Player>();
        if (pl != null) possibleHit = true;
    }

    private void OnTriggerExit2D(Collider2D ou)
    {
        Player pl = ou.GetComponent<Player>();
        if (pl != null) possibleHit = false;
    }

    private void Move(Vector2 dir)
    {
        if(rb.velocity.magnitude < MaxVel)
            rb.velocity += dir * Vel;
    }
}
