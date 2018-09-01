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

    bool possibleHit;

    [SerializeField]
    float Vel;
    [SerializeField]
    float MaxVel;
    [SerializeField]
    float smallValue;
    Rigidbody2D rb;
    public SpriteRenderer sp;


    Sprite Original;
    public Sprite Ataque;

    void Start()
    {
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
        int Turn = (int)(walkingTime + thinkingTime);

        if (!possibleHit)
        {
            if (Time.timeSinceLevelLoad % Turn < walkingTime)
                Move( Front(0.8f));
            else
                Move(Vector2.zero);
        }
        else {
            Move(Vector2.zero);
            Atack();
        }
    }

    private void Atack() {
        sp.sprite = Ataque;
        Invoke("AtackEnd", tellTime);
    }

    private void AtackEnd()
    {
        sp.sprite = Original;
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
        Vector2 dir = (((Vector2)Player.Instance.transform.position + (distance*Vector2.right)) - ((Vector2)transform.position));

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
