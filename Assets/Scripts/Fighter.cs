using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp=10;
    public int maxhp=10;
    public float pushRecoverySpeed=1.0f;

    protected float lastImmune;
    protected float immuneTime=1.0f;

    protected Vector3 pushDirection;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune >immuneTime)
        {
            lastImmune=Time.time;
            hp -= dmg.damageAmt;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            int x=dmg.damageAmt;
            GameManager.instance.ShowText(x.ToString(), 15 , Color.red,transform.position,Vector3.zero,0.5f);
            
            if(hp<=0)
            {
                hp=0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {

    }
}
