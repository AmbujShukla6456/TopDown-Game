using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Mover 
{
    private SpriteRenderer spriteRender;
    private bool isAlive=true;

    protected override void Start()
    {
        base.Start();
        spriteRender=GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if(!isAlive) return;
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHpChange();
    }
    protected override void Death()
    {
        isAlive=false;
        GameManager.instance.DeathMenuAnim.SetTrigger("Show");
    }
    private void FixedUpdate()
    {
        float x= Input.GetAxisRaw("Horizontal");
        float y= Input.GetAxisRaw("Vertical");
        
        if(isAlive) UpdateMotor(new Vector3(x,y,0));
    }
    public void swapSprite(int val)
    {
        spriteRender.sprite=GameManager.instance.playerSprites[val];
    }
    public void OnLevelUp()
    {
        maxhp+=10;
        hp=maxhp;
        GameManager.instance.OnHpChange();
    }
    public void SetLvl(int lvl)
    {
        for(int i=0;i<lvl;i++)
            OnLevelUp();
    }
    public void Heal(int healAmt)
    {
        if(hp==maxhp)
            return;
        
        hp+=healAmt;
        if(hp>maxhp)
            hp=maxhp;
        GameManager.instance.ShowText("+"+healAmt+"hp",25,Color.green,transform.position,Vector3.up*30,1.0f);
        GameManager.instance.OnHpChange();
    }
    public void Respawn()
    {
        hp=maxhp;
        isAlive=true;
        lastImmune=Time.time;
        pushDirection=Vector3.zero;
    }
}
