using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatinTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts=new List<FloatingText>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() 
    {
        foreach(FloatingText txt in floatingTexts)
            txt.updateFloatingText();
    }
    public void Show(string msg,int fontSize,Color color,Vector3 position,Vector3 motion,float duration)
    {
        FloatingText ftext=GetText();

        ftext.txt.text=msg;
        ftext.txt.fontSize=fontSize;
        ftext.txt.color=color;
        ftext.go.transform.position=Camera.main.WorldToScreenPoint(position);
        ftext.motion=motion;
        ftext.duration=duration;
        
        ftext.Show();
    }

    private FloatingText GetText()
    {
        FloatingText txt=floatingTexts.Find(t=>!t.active);
        if(txt==null)
        {
            txt=new FloatingText();
            txt.go=Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt=txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    }
}
