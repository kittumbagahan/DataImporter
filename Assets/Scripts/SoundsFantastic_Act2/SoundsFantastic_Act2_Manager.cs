using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundsFantastic_Act2_Manager : MonoBehaviour {

    public AudioClip[] clipAnimals;
    public Sprite[] sprtAnimals;
    public Sprite[] sprtBtnIdle;
    public Sprite[] sprtBtnPressed;
    public Image[] imgs;
    public Button[] btns;
    public Button btnListen;

    public AudioClip[] audClip;
    public AudioSource audSrcBtns;
   
    int clipIndex = -1;
   // int sprtAnimalIndex = -1;
    [SerializeField]
    int setIndex = -1;
    List<Sprite> lstGroupSprt = new List<Sprite>();
    List<Sprite> lstPrevSprt = new List<Sprite>();
    AudioSource audSrc;
    [SerializeField]
    int pts = 0;
    [SerializeField]
    List<string> lstRecent;

	void Start () {
        sprtAnimals.Shuffle();
        //setIndex = SaveTest.Set;
        audSrc = GetComponent<AudioSource>();
        Generate();
		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void Generate()
    {
        //if (setIndex <= sprtAnimals.Length - 1 && pts < 4)
        if (setIndex <= sprtAnimals.Length - 1)
        {
        //    imgs[0].sprite = sprtAnimals[++sprtAnimalIndex];
        //    imgs[1].sprite = sprtAnimals[++sprtAnimalIndex];
        //    imgs[2].sprite = sprtAnimals[++sprtAnimalIndex];
        //    imgs[3].sprite = sprtAnimals[++sprtAnimalIndex];
            
            imgs[0].sprite = sprtAnimals[setIndex];
            imgs[1].sprite = RandomSprite();
            imgs[2].sprite = RandomSprite();
            imgs[3].sprite = RandomSprite();

         
          
            audSrc.clip = GetClip();
            setIndex++;
            lstGroupSprt.Clear();
        }
        else {
            ActivityDone.instance.Done();
            print("GAME OVER");
        }
        
    }

    bool hasDuplicate(Sprite sprt)
    {
        for (int i = 0; i < lstGroupSprt.Count; i++ )
        {
            if(sprt == lstGroupSprt[i])
            {
                return true;
            }
        }

        return false;
    }

  
    Sprite RandomSprite()
    {
        
        int x = 0;
        bool b = false;
       
        while (!b)
        {
            x = Random.Range(0, 8);
            if(sprtAnimals[x] != sprtAnimals[setIndex] && !hasDuplicate(sprtAnimals[x]))
            {
                lstPrevSprt.Add(sprtAnimals[x]);
                lstGroupSprt.Add(sprtAnimals[x]);
                return sprtAnimals[x];
            }
        
        }

        //lstPrevSprt.Add(sprt);
        return null;
    }

   
    public void Choose(Image img)
    {
		ScoreManager.ins.IncNumOfMoves();
        if (img.sprite.name.ToUpper().Contains(audSrc.clip.name.ToUpper()))
        {
            print("correct!");
            audSrcBtns.clip = audClip[0];
            audSrcBtns.Play();
            Fade(true, false);
            btnListen.interactable = false;
            StartCoroutine(IECorrect());
        }
        else {
            print("wrong");
            audSrcBtns.clip = audClip[1];
            audSrcBtns.Play();
            StartCoroutine(IEWrong());
        }
    }

    public void Choose(Button btn) {
        btn.interactable = false;
    }

    public void DisableButton(Button btn) {
        btn.enabled = false;
    }

    public void Listen()
    {
        btnListen.interactable = false;
        StartCoroutine(IEPlay());
    }

    void Fade(bool fadeOut, bool fadeIn)
    {
        FadeOut fOut;
        FadeIn fIn;
        for (int i = 0; i < imgs.Length; i++ )
        {
            fOut = imgs[i].GetComponent<FadeOut>();
            fIn = imgs[i].GetComponent<FadeIn>();
            fOut.play = fadeOut;
            fIn.play = fadeIn;
        }

        for (int i = 0; i < btns.Length; i++ )
        {
            fOut = btns[i].GetComponent<FadeOut>();
            fIn = btns[i].GetComponent<FadeIn>();
            fOut.play = fadeOut;
            fIn.play = fadeIn;
        }
    }

    IEnumerator IEPlay()
    {
        audSrc.Play();
        while (audSrc.isPlaying) {
            yield return new WaitForSeconds(0.01f);
        }
        btnListen.interactable = true;
    }

    IEnumerator IECorrect()
    {
      
        yield return new WaitForSeconds(1.5f);
        pts++;
        Fade(false, true);
        btns[0].enabled = true;
        btns[1].enabled = true;
        btns[2].enabled = true;
        btns[3].enabled = true;
        btns[0].interactable = true;
        btns[1].interactable = true;
        btns[2].interactable = true;
        btns[3].interactable = true;
        btnListen.interactable = true;
        Generate();
    }

    IEnumerator IEWrong()
    {
        yield return new WaitForSeconds(1f);
        btns[0].enabled = true;
        btns[1].enabled = true;
        btns[2].enabled = true;
        btns[3].enabled = true;
        btns[0].interactable = true;
        btns[1].interactable = true;
        btns[2].interactable = true;
        btns[3].interactable = true;
    }

    void Shuffle(Image[] _img)
    {
        Image tempImg = null;
        int tempIndex = 0;
         for (int i = 0; i < lstRecent.Count; i++ )
         {
                if (_img[0].sprite.name.ToUpper() == lstRecent[i])
                {
                    tempIndex++;
                    tempImg = _img[0];
                    _img[0] = _img[tempIndex];
                    _img[tempIndex] = tempImg;
                    if (tempIndex >= 3) tempIndex = 0;
                    i = 0;
                }
         }

         ChangeButtonSprite(btns[0], imgs[0].sprite);
         ChangeButtonSprite(btns[1], imgs[1].sprite);
         ChangeButtonSprite(btns[2], imgs[2].sprite);
         ChangeButtonSprite(btns[3], imgs[3].sprite);
    }

    AudioClip GetClip()
    {
        bool b = false;
        AudioClip _clip = null;
        // temporarily store the current images
        Image[] _Imgs = new Image[4]; //= imgs; ; //for getting a randon audio clip based on the 4 images
        _Imgs[0] = imgs[0];
        _Imgs[1] = imgs[1];
        _Imgs[2] = imgs[2];
        _Imgs[3] = imgs[3];
        //shuffle
        _Imgs.Shuffle(); // shuffle image to random the position of image[0]
        Shuffle(_Imgs); //shuffle the image if the image[0] has already appeared
       

        for (int i = 0; i < clipAnimals.Length; i++)
        {
            //set the name of the sprite and clip to its animal name only
            //GET THE CLIP BASED ON TEMPORARY IMAGE[0]
            if (_Imgs[0].sprite.name.ToUpper() == clipAnimals[i].name.ToUpper())
            {
                lstRecent.Add(clipAnimals[i].name.ToUpper());
                return clipAnimals[i];
            }
        }
        return null;
      
    }

    void ChangeButtonSprite(Button btn, Sprite sprt)
    {
        Image img = btn.GetComponent<Image>();
        SpriteState sprtState = btn.spriteState;
        if(sprt.name.ToUpper().Contains("COW")){
            img.sprite = GetButtonSprite("COW", 0);
            sprtState.pressedSprite = GetButtonSprite("COW", 1);
        }
        else if (sprt.name.ToUpper().Contains("BIRD")) {
            img.sprite = GetButtonSprite("BIRD", 0);
            sprtState.pressedSprite = GetButtonSprite("BIRD", 1);
        }
        else if (sprt.name.ToUpper().Contains("CAT")) {
            img.sprite = GetButtonSprite("CAT", 0);
            sprtState.pressedSprite = GetButtonSprite("CAT", 1);
        }
        else if (sprt.name.ToUpper().Contains("DOG")) {
            img.sprite = GetButtonSprite("DOG", 0);
            sprtState.pressedSprite = GetButtonSprite("DOG", 1);
        }
        else if (sprt.name.ToUpper().Contains("CHICKEN")) {
            img.sprite = GetButtonSprite("CHICKEN", 0);
            sprtState.pressedSprite = GetButtonSprite("CHICKEN", 1);
        }
        else if (sprt.name.ToUpper().Contains("PIG")){
            img.sprite = GetButtonSprite("PIG", 0);
            sprtState.pressedSprite = GetButtonSprite("PIG", 1);
        }
        else if (sprt.name.ToUpper().Contains("MOUSE")){
            img.sprite = GetButtonSprite("MOUSE", 0);
            sprtState.pressedSprite = GetButtonSprite("MOUSE", 1);
        }
        else if (sprt.name.ToUpper().Contains("DUCK")) {
            img.sprite = GetButtonSprite("DUCK", 0);
            sprtState.pressedSprite = GetButtonSprite("DUCK", 1);
        }

        sprtState.disabledSprite = sprtState.pressedSprite;
        btn.spriteState = sprtState;
    }

    Sprite GetButtonSprite(string keyword, int state)
    {
        Sprite sprt = null;
        for (int i = 0; state == 0 && i < sprtBtnIdle.Length; i++)
        {
            if(sprtBtnIdle[i].name.ToUpper().Contains(keyword.ToUpper()))
            {
               // print(sprtBtnIdle[i].name);
                return sprtBtnIdle[i];
            }
        }

        for (int i = 0; state == 1 && i < sprtBtnPressed.Length; i++)
        {
            if (sprtBtnPressed[i].name.ToUpper().Contains(keyword.ToUpper()))
            {
                return sprtBtnPressed[i];
            }
        }

        return sprt;
    }
}
