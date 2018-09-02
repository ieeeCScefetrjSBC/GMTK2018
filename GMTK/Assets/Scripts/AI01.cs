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
    bool moveUp;

    [SerializeField]
    float Vel;
    [SerializeField]
    float MaxVel;
    [SerializeField]
    float smallValue;

    private bool fronter;
    


    

    void Start()
    {
        StartCode();
        fronter = Random.value > 0.5f;
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

        if (!isStunned && Player.Instance.enabled)
        {
            if (!possibleHit)
            {

                if ((Time.timeSinceLevelLoad - restartTimer) % Turn < walkingTime)
                {
                    string seen = Look((((Vector2)Player.Instance.transform.position) - ((Vector2)transform.position)), 1000);
                    if (seen == "Player")
                    {
                        if(fronter) Move(Front(0.7f)); 
                        else Move(Back(0.7f));

                    }
                    else
                    {
                        Move(FindClearWay(Follow()));
                    }
                }
                else
                {
                    Move(Vector2.zero);
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

    private Vector2 RotatedVector2D(Vector2 vector, float angle)
    {
        float x = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float y = vector.y * Mathf.Cos(angle) + vector.x * Mathf.Sin(angle);

        return new Vector2(x, y);
    }

    private Vector2 FindClearWay(Vector2 direction)
    {
        //Vector2 direction = Player.Instance.transform.position - transform.position;
        Vector2 posRotated = direction;
        Vector2 negRotated = direction;
        float angleDelta = 0.1f;

        if (direction == Vector2.zero)
            return direction;

        RaycastHit2D[] hitPos, hitNeg;
        hitPos = Physics2D.RaycastAll(transform.position, direction);

        foreach (RaycastHit2D hit in hitPos)
        {
            if ((hit.collider == null || hit.collider.gameObject.tag == "Player") && hitPos.Length == 1)
                return direction.normalized;
            //else
            //{
            //    Atack();
            //    Debug.Log(hit.collider.gameObject.name);
            //    Debug.Log(hit.collider.transform.position);

            //}
        }


        while (angleDelta < 2 * Mathf.PI / 2)
        {
            posRotated = RotatedVector2D(direction, angleDelta);
            negRotated = RotatedVector2D(direction, -angleDelta);

            //hitPos = Physics2D.CircleCastAll(transform.position, 0.5f, posRotated, Mathf.Infinity, LayerMask.NameToLayer("Ignore Raycast"));
            //hitNeg = Physics2D.CircleCastAll(transform.position, 0.5f, negRotated, Mathf.Infinity, LayerMask.NameToLayer("Ignore Raycast"));

            hitPos = Physics2D.RaycastAll(transform.position, posRotated);
            hitNeg = Physics2D.RaycastAll(transform.position, negRotated);

            foreach (RaycastHit2D hit in hitPos)
            {
                if ((hit.collider == null || hit.collider.gameObject.tag == "Player") && hitPos.Length == 1)
                    return posRotated.normalized;
            }
            foreach (RaycastHit2D hit in hitNeg)
            {
                if ((hit.collider == null || hit.collider.gameObject.tag == "Player") && hitPos.Length == 1)
                    return negRotated.normalized;
            }
            angleDelta += angleDelta;
        }

        return posRotated.normalized;
    }

    private void Atack()
    {
        if (Player.Instance.enabled)
        {
            sp.sprite = Ataque;
            Player.Instance.defenceWindow = true;
            Player.Instance.Atacker.Add(this);
            Invoke("AtackEnd", tellTime);
        }
    }

    private void AtackEnd()
    {
        sp.sprite = Original;
        hitting = false;
        
            
        Player.Instance.Atacker.Remove(this);
        Player.Instance.defenceWindow = Player.Instance.Atacker.Count > 0;
        thinkingTimer = Time.timeSinceLevelLoad;

        if (possibleHit)
        {
            Player.Instance.Damage(damageCaused);
        }
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
