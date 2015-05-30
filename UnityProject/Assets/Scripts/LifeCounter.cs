using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour {

    public Image[] Lives;
    public Image Background;
	// Use this for initialization

	public void Init() 
    {
        Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 132.0f/255.0f);
        for(int i=0;i<Lives.Length;i++)
        {
            Lives[i].color = new Color(Lives[i].color.r, Lives[i].color.g, Lives[i].color.b, 1);
        }
	}
	
	public void LifeLose(int lives)
    {
        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i].color = new Color(Lives[i].color.r, Lives[i].color.g, Lives[i].color.b, i<lives?1:0);
        }
        if(lives<=0)
        {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 0);
        }
    }
}
