using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta; //the difference in the position in this frame and the next => next=curr+moveDelta
    protected RaycastHit2D hit;
    public float yspeed=0.75f;
    public float xspeed=1.0f;

    protected virtual void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset moveDelta
        moveDelta=new Vector3(input.x*xspeed,input.y*yspeed,0);

        if(moveDelta.x >0)
            transform.localScale = Vector3.one;
        else if( moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);
        
        //add push
        moveDelta+=pushDirection;

        //reduce push
        pushDirection=Vector3.Lerp(pushDirection,Vector3.zero,pushRecoverySpeed);

        //collision on y-Axis
        hit=Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y * Time.deltaTime),LayerMask.GetMask("character","blocking"));
        if(!hit.collider)
        {//moving
        transform.Translate(0,moveDelta.y * Time.deltaTime,0); //deltaTime return 1/FPS to make movement seem cohesive
        }

        //collision on x-Axis
        hit=Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(moveDelta.x,0),Mathf.Abs(moveDelta.x * Time.deltaTime),LayerMask.GetMask("character","blocking"));
        if(hit.collider==null)
        {//moving
        transform.Translate(moveDelta.x * Time.deltaTime,0,0); //deltaTime return 1/FPS to make movement seem cohesive
        }
    }
}
