using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingFountain : collideable
{
    private int healAmt=1;
    private float coolDown=1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            if(Time.time-lastHeal >coolDown)
            {
                GameManager.instance.Player.Heal(healAmt);
                lastHeal=Time.time;
            }
        }
    }
}
