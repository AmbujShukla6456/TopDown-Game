using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpval=116;

    public float triggerLength=0.4f;
    public float chaseLength=1.0f;
    private bool chasing;
    private bool collidePlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits=new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform=GameManager.instance.Player.transform;
        startingPosition=transform.position;
        hitbox=transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate() 
    {
        //is player in range
        if(Vector3.Distance(playerTransform.position,startingPosition) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position,startingPosition) < triggerLength)
                chasing=true;
            if(chasing)
            {
                if(!collidePlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
            
        }
        else
            {
                UpdateMotor(startingPosition - transform.position);
                chasing=false;
            }
            //overlap
            collidePlayer=false;
            boxCollider.OverlapCollider(filter,hits);
            for(int i=0;i< hits.Length;i++)
            {
                if(hits[i]==null)
                    continue;
            
                if(hits[i].tag == "Fighter" && hits[i].name=="Player")
                {
                    collidePlayer=true;
                }

                hits[i]=null;
            }   
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantExp(xpval);
        GameManager.instance.ShowText("+"+xpval+"xp",30,Color.magenta,transform.position,Vector3.up*40,1.0f);
    }
}
