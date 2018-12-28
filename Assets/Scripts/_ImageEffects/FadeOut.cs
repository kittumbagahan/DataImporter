using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    Image img;
    public float startDelay, spd;
    public bool play = true;
    void Start()
    {
        img = GetComponent<Image>();
        InvokeRepeating("Out", startDelay, 0.01f);
    }

    public void ReviveColor()
    {
        img.color = new Color(255f, 255f, 255f, 255f);
    }

    public void Play()
    {
        play = true;
       // img.color = new Color(255f, 255f, 255f, 255f);
    }

    void Out()
    {
        if(play)
        {
            img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.b, img.color.g, 0f), Time.deltaTime * spd);
            //print(img.color.a);
            if(img.color.a <= 0)
            {
               play = false;
            }
           // print("FADE OUT PLAYING");
        }
       
    }

  
}
