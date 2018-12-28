using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TinaAndJun_Act3_Manager : MonoBehaviour {

    public Sprite[] sprts;
    public AudioClip[] clips;

    public Button btnPlay;
    public Image imgObj;
    public Button btnOO, btnEE;
    public AudioClip[] audClip;
    public AudioSource audSrcBtns;
    AudioSource audSrc;
    //SpriteState tempSprtState;
    [SerializeField]
    Button btnRecent;
    int sprtIndex = -1, pts = 0;

    Grow grow;
    FadeIn fadeIn;

	void Start () {

        grow = imgObj.GetComponent<Grow>();
        fadeIn = imgObj.GetComponent<FadeIn>();
        //sprts.Shuffle();
        sprtIndex = SaveTest.Set;
        audSrc = GetComponent<AudioSource>();
        Generate();

		ScoreManager.ins.AW();
	}
    void Update()
    {
    }
    void Generate()
    {
        if (sprtIndex < sprts.Length && pts < 4)
        {
            sprtIndex++;
            imgObj.sprite = sprts[sprtIndex];

            grow.Play();
            fadeIn.Play();
        }
        else {
            ActivityDone.instance.Done();
        
        }
        //try
        //{
        //    sprtIndex++;
        //    imgObj.sprite = sprts[sprtIndex];
          
        //    grow.Play();
        //    fadeIn.Play();
        //}catch(Exception ex)
        //{
        //    ActivityDone.instance.Done();
        //    print("GAME OVER");
        //}
     
        
    }

    public void SetBtnRecent(Button btn)
    {
        btnRecent = btn;
    }

    public void Play(Button btnPlay)
    {
        //if (clips[i].name.ToUpper() == sprts[sprtIndex].name.ToUpper())
        //{
            audSrc.clip = clips[sprtIndex];
            audSrc.Play();
            btnPlay.interactable = false;
            StartCoroutine(IEPlay(btnPlay));
        //}

        //try {
        //    for (int i = 0; i < clips.Length; i++)
        //    {
        //        if (clips[i].name.ToUpper() == sprts[sprtIndex].name.ToUpper())
        //        {
        //            audSrc.clip = clips[i];
        //            audSrc.Play();
        //            btnPlay.interactable = false;
        //            StartCoroutine(IEPlay(btnPlay));
        //        }
        //    }
        //}
        //catch(IndexOutOfRangeException ex)
        //{
        //    print(ex.Message);
        //}
       
    }

    IEnumerator IEPlay(Button btn)
    {
        while (audSrc.isPlaying)
        {
            yield return new WaitForSeconds(0.05f);
        }
        btn.interactable = true;
    }


    public void Choose(string keyword) {
        string tempStr = sprts[sprtIndex].name.ToUpper();
        btnPlay.enabled = false;
       	
		ScoreManager.ins.IncNumOfMoves();
        if (tempStr.Contains("EA")){ tempStr = tempStr.Replace("EA","EE");}
   
        if (tempStr.Contains(keyword) && keyword == "OO")
        {
            pts++;
            btnOO.interactable = false;
            btnEE.enabled = false;
            StartCoroutine(IEGenerate(1f));
            StartCoroutine(IEEnableBtns(1f));
            print("correct OO");
            audSrcBtns.clip = audClip[0];
            audSrcBtns.Play();
        }
        else if (tempStr.Contains(keyword) && keyword == "EE")
        {
            pts++;
            btnEE.interactable = false;
            btnOO.enabled = false;
            StartCoroutine(IEGenerate(1f));
            StartCoroutine(IEEnableBtns(1f));
            print("correct EE");
            audSrcBtns.clip = audClip[0];
            audSrcBtns.Play();
        }
        else {
            print("wrong");
            btnRecent.interactable = false;
            if (btnRecent == btnOO) { btnEE.enabled = false; }
            if (btnRecent == btnEE) { btnOO.enabled = false; }
            StartCoroutine(IEEnableBtns(1f));
            audSrcBtns.clip = audClip[1];
            audSrcBtns.Play();
        }
    }

    IEnumerator IEEnableBtns(float time)
    {
        //print("IEWROING");
        yield return new WaitForSeconds(time);
        print("bruno mars");
        btnEE.enabled = true;
        btnOO.enabled = true;
        btnEE.interactable = true;
        btnOO.interactable = true;
        btnPlay.enabled = true;
    }

    IEnumerator IEGenerate(float time)
    {
        yield return new WaitForSeconds(time);
        Generate();
    }
}
