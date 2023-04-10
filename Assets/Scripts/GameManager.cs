using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(GameManager.instance !=null)
        {
            Destroy(gameObject);
            Destroy(Player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        instance=this;
        SceneManager.sceneLoaded+=LoadState;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(hpbar.gameObject);
        DontDestroyOnLoad(hud);
        DontDestroyOnLoad(menu);
        SceneManager.sceneLoaded+=OnLoad;
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //reference
    public player Player;
    public Weapon weapon;
    public FloatinTextManager floatingTextManager;
    public RectTransform hpbar;
    public GameObject hud;
    public GameObject menu;
    public Animator DeathMenuAnim;

    //count
    public int pesos;
    public int exp;

    private void Start()
    {
        Player.maxhp=20;
        Player.hp=Player.maxhp;
        pesos=0;
        exp=0;
        weapon.weaponLevel=0;
        weapon.setWeaponLevel(weapon.weaponLevel);
    }
    public void ShowText(string msg,int fontSize,Color color,Vector3 position,Vector3 motion,float duration)
    {
        floatingTextManager.Show(msg,fontSize,color,position,motion,duration);
    }

    public bool tryUpgradeWeapon()
    {
        if(weapon.weaponLevel>=weaponPrices.Count)
            return false;

        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos-=weaponPrices[weapon.weaponLevel];
            weapon.Upgrade();
            return true;
        }
        return false;
    }

    public void OnHpChange()
    {
        Debug.Log("dmg");
        float ratio=(float) Player.hp / (float) Player.maxhp;
        hpbar.localScale=new Vector3(1,ratio,1);
    }

    public int GetCurrentLevel()
    {
        int r=0;
        int add=0;
        
        while(exp>=add)
        {
            add+=xpTable[r];
            r++;
            if(r==xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXpToLvl(int level)
    {
        int r=0;
        int xp=0;

        while(r<level)
        {
            xp+=xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantExp(int xp)
    {
        int currlvl=GetCurrentLevel();
        exp+=xp;
        while(currlvl < GetCurrentLevel()){
            levelUp();
            currlvl++;
        }
    }
    public void levelUp()
    {
        Debug.Log("Level Up!");
        Player.OnLevelUp();
    }
    public void ReStart()
    {
        DeathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        Player.Respawn();
    }
    // Game 
    /*
    * INT player skin
    * INT player pesos
    * INT player exp
    * INT player weaponLevel
    */

    public void OnLoad(Scene s, LoadSceneMode mode)
    {
        Player.transform.position=GameObject.Find("SpawnPoint").transform.position;
    }
    public void SaveState()
    {
        Debug.Log("State Save");
        string s="";
        s+="0"+"|";
        s+=pesos.ToString() + "|";
        s+=exp.ToString() + "|";
        s+=weapon.weaponLevel.ToString();
        PlayerPrefs.SetString("SaveState",s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded-=LoadState;
        GameManager.instance.OnHpChange();
        Debug.Log("State Load");
        if(!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data= PlayerPrefs.GetString("SaveState").Split('|');

        //skin
        pesos=int.Parse(data[1]);
        exp=int.Parse(data[2]);
        Player.SetLvl(GetCurrentLevel());
        //weapon
        weapon.setWeaponLevel(int.Parse(data[3]));

        Player.transform.position=GameObject.Find("SpawnPoint").transform.position;
    }
        
}
