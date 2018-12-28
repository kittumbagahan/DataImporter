using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Analytics;
using System.Collections.Generic;
public class BookshelfManager : MonoBehaviour {
    public static BookshelfManager ins;
    [SerializeField]
    AudioClip audClipBookClick, audClipBookHighlight;
    AudioSource audSrc;
    [SerializeField]
    GameObject BGMUSIC;
    public bool aBookIsActive;
    public AudioSource AudioSrc {
        get { return audSrc; }
    }
    //public AudioClip AudClipBookClick{
    //    get { return audClipBookClick; }
    //    set { audClipBookClick = value; }
    //}

    //public AudioClip AudClipBookHighlight {
    //    get { return audClipBookHighlight; }
    //    set { audClipBookHighlight = value; }
    //}

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        GameObject obj = GameObject.Find("BG_MUSIC(Clone)");
        try
        {
            if (!obj.activeInHierarchy)
            {
                Instantiate(BGMUSIC, transform.position, Quaternion.identity);
            }
        }
        catch (Exception ex)
        {
            Instantiate(BGMUSIC, transform.position, Quaternion.identity);
            //print(ex.Message);
        }
        
    }
	void Start () {
        ins = this;
        audSrc = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("openedTimes", PlayerPrefs.GetInt("openedTimes") + 1);
        //Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        //{
        //    { "play_times", PlayerPrefs.GetInt("openedTimes") }
        //});
    }


    public void PlayBookClick() 
    {
        audSrc.PlayOneShot(audClipBookClick);
    }

    public void PlayBookActive()
    {
        if(!audSrc.isPlaying)
        audSrc.PlayOneShot(audClipBookHighlight);
        //audSrc.clip = audClipBookHighlight;
        //audSrc.Play();
    }
}
