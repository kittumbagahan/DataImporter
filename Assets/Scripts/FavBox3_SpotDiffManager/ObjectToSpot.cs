using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectToSpot : MonoBehaviour {

    public bool different = false;
    public AudioClip[] audClip;
    //public AudioSource audSrc;
    public delegate void ActionCorrect(GameObject obj);
    public static event ActionCorrect OnCorrect;
    public static bool ready;
    Image img;
    void Start () {
       // audSrc = GetComponent<AudioSource>();
        img = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void RemoveSubscribers()
    {
        OnCorrect = delegate { };
    }

    void Generate()
    {
        SpotDiffManager.ins.GenerateSpot();
    }

    public void Click()
    {
        if(ready)
        {
			ScoreManager.ins.IncNumOfMoves();
            if (different)
            {
                ready = false;
                print("YOU FOUND IT!");
                //audSrc.clip = audClip[0];
                //audSrc.Play();
                UI_SoundFX.ins.Play(audClip[0]);
                if (OnCorrect != null)
                {
                    OnCorrect(this.gameObject);
                }
                Invoke("Generate", 0f);
            }
            else
            {
                //audSrc.clip = audClip[1];
                //audSrc.Play();
                UI_SoundFX.ins.Play(audClip[1]);
                print("TRY AGAIN");
                //SpotDiffManager.ins.PlayWrongSound();
                ChangeColor(Color.red);
                Invoke("ResetColor", 1f);
            }
        }
       
    }

    void ChangeColor(Color color)
    {
        img.color = color;
        
    }

    void ResetColor()
    {
        img.color = Color.white;
    }
}
