using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FadeIn : MonoBehaviour {

    Image img;
    [SerializeField]
    Text txt;
	[SerializeField]
	TextMeshPro tmpro;
    //Color clr;
    float r, g, b;
    public float startDelay, spd;
    public bool play = true;
	void Start () {
        img = GetComponent<Image>();
        txt = GetComponent<Text>();  
        InvokeRepeating("In", startDelay, 0.01f);
        if(play)
        {
            if (img != null)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
                r = img.color.r;
                g = img.color.g;
                b = img.color.b;
            }
            else if (txt != null)
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0f);
            }
        }
        
        
	}

    public void Play()
    {
        if (img != null)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
        else if (txt != null)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0f);
        }
        play = true;
    }


    void In()
    {
        if(play)
        {
            if(img != null)
            {
				img.enabled = true;
                img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 1f), Time.deltaTime * spd);
                if (img.color.a >= 0.999)
				{
                    play = false;
                }
            }
            else if (txt != null)
            {
                txt.color = Color.Lerp(txt.color, new Color(txt.color.r, txt.color.g, txt.color.b, 1f), Time.deltaTime * spd);
                if (txt.color.a >= 0.999)
                {
                    play = false;
                }
            }
           
        }
     
    }
}
