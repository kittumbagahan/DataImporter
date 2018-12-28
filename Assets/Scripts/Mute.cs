using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Mute : MonoBehaviour {

	// Use this for initialization
    
    AudioSource bg;
    [SerializeField]
    Sprite muteIcon;
    Sprite defaultIcon;
    Image img;
    void Awake()
    {
       // MuteAll();
        img = GetComponent<Image>();
        defaultIcon = img.sprite;
        //MuteAudio();
        
        
    }
	void Start () {
        try
        {
            bg = GameObject.Find("BG_MUSIC(Clone)").GetComponent<AudioSource>();
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
        if (bg != null)
        {
            if (bg.mute)
            {
                img.sprite = muteIcon;
            }
            else
            {
                img.sprite = defaultIcon;
            }
        }

		if (muteIcon != false)
		{
			if (bg.mute == true) img.sprite = muteIcon;
			else img.sprite = defaultIcon;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MuteAudio()
    {
        /*AudioSource[] aud = GameObject.FindObjectsOfType<AudioSource>();
        if (Singleton.mute == true)
        {
            for (int i = 0; i < aud.Length; i++)
            {
                aud[i].mute = true;
            }
            //Singleton.mute = true;
        }*/
		if(bg.mute)
			bg.mute = true;
			//BG_Music.ins.SetVolume(0f);
    }

    //use for button ON/OFF

    public void MuteBG()
    {
        if (bg != null)
        {
			if (bg.mute)
            {
                bg.mute = false;
				//BG_Music.ins.SetVolume(BG_Music.ins.RecentClipVolume);
				//BG_Music.ins.MuteBG();
				img.sprite = defaultIcon;
            }
            else
            {
                bg.mute = true;
				//BG_Music.ins.SetVolume(0f);
                img.sprite = muteIcon;
            }
        }
        else
        {
            bg = GameObject.Find("BG_MUSIC(Clone)").GetComponent<AudioSource>();
           // MuteBG();
        }
         
       
    }

   
   
}
