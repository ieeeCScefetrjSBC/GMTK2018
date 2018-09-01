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

    private void FindClearWay()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Player.Instance.transform.position);

        if (hit.collider.gameObject.tag != "Player")
        {

        }
    }


    private void Atack()
    {
        sp.sprite = Ataque;
        Player.Instance.defenceWindow = true;
        Player.Instance.Atacker = this;
        Invoke("AtackEnd", tellTime);
    }

    private void AtackEnd()
    {
        sp.sprite = Original;
        hitting = false;
        Player.Instance.defenceWindow = false;
        Player.Instance.Atacker = null;
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
