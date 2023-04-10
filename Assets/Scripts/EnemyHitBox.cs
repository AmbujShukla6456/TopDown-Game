using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : collideable
{
    public int damage=1;
    public float pushForce=3;

    protected override void OnCollide(Collider2D hit)
    {
        if(hit.name=="Player")
        {
            Damage dmg=new Damage
            {
                damageAmt=damage,
                origin=transform.position,
                pushForce=pushForce
            };

            hit.SendMessage("ReceiveDamage",dmg);
        }
    }
}
