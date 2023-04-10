using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : collideable
{
    public int[] damage={1,2,3,4,5,6,7};
    public float[] pushForce={2.0f,2.2f,2.5f,3f,3.2f,3.6f,4f};

    public int weaponLevel=0;
    private SpriteRenderer spriteRenderer;

    public float coolDown=1.0f;
    public float lastSwing;
    private Animator anime;
    private void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        spriteRenderer=GetComponent<SpriteRenderer>();
        anime=GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > coolDown)
            {
                Swing();
                lastSwing=Time.time;
            }
        }
    }

    protected override void OnCollide(Collider2D hit)
    {
        if(hit.tag=="interactable")
        {
            if(hit.name == "Player")
                return ;
            
            Damage dmg=new Damage
            {
                damageAmt=damage[weaponLevel],
                origin=transform.position,
                pushForce=pushForce[weaponLevel]
            };

            hit.SendMessage("ReceiveDamage",dmg);
        }
    }

    private void Swing()
    {
        anime.SetTrigger("swing");
    }

    public void Upgrade()
    {
        if(weaponLevel >= GameManager.instance.weaponPrices.Count)
            return;
        weaponLevel++;
        spriteRenderer.sprite=GameManager.instance.weaponSprites[weaponLevel];
    }
    public void setWeaponLevel(int lvl)
    {
        weaponLevel=lvl;
        spriteRenderer.sprite=GameManager.instance.weaponSprites[weaponLevel];
    }
}
