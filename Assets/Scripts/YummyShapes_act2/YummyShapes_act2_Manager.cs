using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YummyShapes_act2_Manager : MonoBehaviour {

    public Sprite[] sprtFruitIdle;
    public Sprite[] sprtFruitWrong;
    public Sprite[] sprtFruitRight;
    public Button[] btnFruits;

    public AudioClip[] audClip;
    AudioSource audSrc;

    [SerializeField]
    int index = 0, pts = 0;
    Sprite sprtDifferentGray;
    Sprite sprtDifferentColored;
    string strFruitAns, strFruitWrong;
    SpriteState sprtTempState;

	void Start () {
        index = SaveTest.Set;
        audSrc = GetComponent<AudioSource>();
        GameOn();
		ScoreManager.ins.AW();
	}

    void GameOn()
    {
        Grow grow;
        Spin spin;
        btnFruits.Shuffle();
        for (int i = 0; i < btnFruits.Length; i++)
        {
            //btnFruits[i].spriteState = sprtTempState;
            btnFruits[i].interactable = true;
        }
        btnFruits[0].GetComponent<Image>().sprite = sprtFruitIdle[index];
        strFruitAns = GetFruitSpriteName(sprtFruitIdle[index].name);
        //
        sprtDifferentGray = GetDifferentFrom(strFruitAns);
        //strFruitWrong = GetFruitSpriteName(sprtDifferent.name);
        btnFruits[1].GetComponent<Image>().sprite = sprtDifferentGray;
        btnFruits[1].spriteState = sprtTempState;
        btnFruits[2].GetComponent<Image>().sprite = sprtDifferentGray;
        btnFruits[2].spriteState = sprtTempState;
        btnFruits[3].GetComponent<Image>().sprite = sprtDifferentGray;
        btnFruits[3].spriteState = sprtTempState;

        grow = btnFruits[0].GetComponent<Grow>();
        spin = btnFruits[0].GetComponent<Spin>();
        grow.Play();
        spin.SpinIt();
        grow = btnFruits[1].GetComponent<Grow>();
        spin = btnFruits[1].GetComponent<Spin>();
        grow.Play();
        spin.SpinIt();
        grow = btnFruits[2].GetComponent<Grow>();
        spin = btnFruits[2].GetComponent<Spin>();
        grow.Play();
        spin.SpinIt();
        grow = btnFruits[3].GetComponent<Grow>();
        spin = btnFruits[3].GetComponent<Spin>();
        grow.Play();
        spin.SpinIt();

    }

    Sprite GetDifferentFrom(string str)
    {
        Sprite sprt = null;

        while(sprt == null)
        {
            int rnd = Random.Range(0, sprtFruitIdle.Length);
            sprt = sprtFruitIdle[rnd];
            if (GetFruitSpriteName(sprt.name) == str)
            {
                sprt = null;
            }
            else
            {
                sprtTempState.disabledSprite = sprtFruitWrong[rnd];
                sprtDifferentColored = sprtFruitRight[rnd];
            }
        }
        return sprt;
        
    }

    string GetFruitSpriteName(string sprtName)
    {
     
        if (sprtName.Contains("apple")) { return "apple"; }
        if (sprtName.Contains("banana")) { return "banana"; }
        if (sprtName.Contains("strawberry")) { return "strawberry"; }
        if (sprtName.Contains("orange")) { return "orange"; }
        if (sprtName.Contains("jackfruit")) { return "jackfruit"; }
        if (sprtName.Contains("mango")) { return "mango"; }

        return "";
    }

    public void Choose(Image img)
    {
		ScoreManager.ins.IncNumOfMoves();
        string strChoice = GetFruitSpriteName(img.sprite.name);
        Button btn = img.GetComponent<Button>();
        if (strFruitAns == strChoice)
        {
            print("wow");
            audSrc.clip = audClip[0];
            audSrc.Play();
            img.sprite = sprtFruitRight[index];
            Correct();
        }
        else
        {
            audSrc.clip = audClip[1];
            audSrc.Play();
            print("ngek");
            //img.sprite = sprtFruitWrong[index];
            btn.interactable = false;
        }
    }

    void Correct()
    {
        sprtTempState.disabledSprite = sprtDifferentColored;
        //if (index < sprtFruitIdle.Length - 1 && pts < 3)
        if(pts < 3)
        {
            index++;
            pts++;
            if (pts >= 3)
            {
                Invoke("Done", 1f);
                print("GAME OVER");
            }
        }
        else {
            //Invoke("Done",0.2f);
            //print("GAME OVER");
        }
        for(int i=1; i<btnFruits.Length; i++)
        {
            btnFruits[i].spriteState = sprtTempState;
            btnFruits[i].interactable = false;
        }
        if(pts < 3)
        StartCoroutine(IENext());
    }

    IEnumerator IENext()
    {
        yield return new WaitForSeconds(1f);
        GameOn();
    }

    void Done() {
        ActivityDone.instance.Done();
    }
}
