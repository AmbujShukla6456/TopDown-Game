using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int PesosAmt=5;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected=true;
            GetComponent<SpriteRenderer>().sprite=emptyChest;
            GameManager.instance.pesos+=PesosAmt;
            GameManager.instance.ShowText("+"+PesosAmt+" Pesos",25,Color.yellow,transform.position,Vector3.up * 50,2.0f);
        }
    }
}
