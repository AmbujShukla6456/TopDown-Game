using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : collideable
{
    protected bool collected;

    protected override void OnCollide(Collider2D hit)
    {
        if(hit.name=="Player")
            OnCollect();
    }
    protected virtual void OnCollect()
    {
        collected=true;
    }
}
