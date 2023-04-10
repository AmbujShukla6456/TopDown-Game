using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //values
    public Text lvlVal,healthVal,pesosVal,xpVal,upgradeVal;

    //character Selection
    private int currentCharacterSelection=0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Change
    public void ArrowClick(bool right){
        if(right){
            currentCharacterSelection++;
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection=0;
            OnSelectionChange();
        }
        else{
            currentCharacterSelection--;
            if(currentCharacterSelection < 0)
                currentCharacterSelection=GameManager.instance.playerSprites.Count-1;
            OnSelectionChange();
        }
    }

    private void OnSelectionChange(){
        characterSelectionSprite.sprite=GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.Player.swapSprite(currentCharacterSelection);
    }

    public void onUpgradeClick(){
        if(GameManager.instance.tryUpgradeWeapon())
            UpdateMenu();
    }

    public void UpdateMenu(){
        //weapon
        weaponSprite.sprite=GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        upgradeVal.text=GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //meta
        lvlVal.text=GameManager.instance.GetCurrentLevel().ToString();
        healthVal.text=GameManager.instance.Player.hp.ToString();
        pesosVal.text=GameManager.instance.pesos.ToString();

        //xp
        int currlvl=GameManager.instance.GetCurrentLevel();
        if(currlvl==GameManager.instance.xpTable.Count)
        {
            xpVal.text=GameManager.instance.exp.ToString()+" EXP points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevlvlxp=GameManager.instance.GetXpToLvl(currlvl-1);
            int currlvlxp=GameManager.instance.GetXpToLvl(currlvl);

            int diff=currlvlxp-prevlvlxp;
            int progXp=GameManager.instance.exp - prevlvlxp;
            float compeletionRatio = (float)progXp / (float)diff;
            xpBar.localScale=new Vector3(compeletionRatio, 1, 1);
            xpVal.text=progXp.ToString() + "/" + diff.ToString();
        }
    }
}
