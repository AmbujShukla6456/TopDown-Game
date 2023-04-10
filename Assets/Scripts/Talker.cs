using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : collideable
{
    public string msg;

    private float coolDown=4.0f;
    private float lastShown=-4.0f;

    protected override void OnCollide(Collider2D coll)
    {
        if(Time.time - lastShown >= coolDown)
        {
            lastShown=Time.time;
            GameManager.instance.ShowText(msg,25,Color.yellow,transform.position+new Vector3(0,0.16f,0),Vector3.zero,coolDown);
        }
    }
}
