using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ABCCircus_Act2_Manager : MonoBehaviour {

	public static ABCCircus_Act2_Manager ins;
    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag, clipSetFin;
    public ETiltDirection eTiltDir;
    public float maxTilt = 20;
    public Font font;
    public Color32[] clr;
    public Sprite[] sprtStage;
    public Image imgChar;
    public Text txtTop, txtBottom;
    [SerializeField]
    Transform transRecentSlot;
    //AudioSource audSrc;
    

	void Awake()
	{
		WordGameManager_2.OnGenerate += Generate;
		ins = this;
	}
    void Start()
    {
        //audSrc = GetComponent<AudioSource>();
        //WordGameManager_2.OnGenerate += TiltObjects;
        //WordGameManager_2.OnGenerate += DisableItemsImage;
        //WordGameManager_2.OnGenerate += ChangeItemsFont;
       
        
        //WordGameManager_2.OnGenerate += InsertItem;
		Invoke("StartGame",0.5f);
      	
    }

	void StartGame()
	{
		imgChar.gameObject.SetActive(true);
		WordGameManager_2.ins.GenerateWord();

		switch(WordGameManager_2.ins.GetSet())
		{
		case 0:
			ScoreManager.ins.maxMove = 14;
			break;
		case 3:
			ScoreManager.ins.maxMove = 16;
			break;
		case 6:
			ScoreManager.ins.maxMove = 20;
			break;
		case 9:
			ScoreManager.ins.maxMove = 21;
			break;
		case 12:
			ScoreManager.ins.maxMove = 18;
			break;
		}

		ScoreManager.ins.AW();
	}

	public void ConnectOnGenerate()
	{

		WordGameManager_2.OnGenerate += Generate;
	}

    void Generate()
    {
        Item.OnInsert -= InsertItem;
        Item.OnBeginDrag -= SetRecentSlot;
        Item.OnReturn -= Return;
        switch (WordGameManager_2.ins.GetWord())
        {
            case "DONUT": imgChar.sprite = sprtStage[0];
                txtTop.text = "Delicious Dancing"; txtBottom.text = "";
                txtTop.color = clr[0];
                ChangeLetterColor(clr[0]);
                break;
            case "ANT": imgChar.sprite = sprtStage[1];
                txtTop.text = "Awesome Acrobat"; txtBottom.text = "";
                txtTop.color = clr[1];
                ChangeLetterColor(clr[1]);
                break;
            case "CRAYON": imgChar.sprite = sprtStage[2];
                txtTop.text = ""; txtBottom.text = "Coloring Cow";
                txtBottom.color = clr[2];
                ChangeLetterColor(clr[2]);
                break;
            case "BUNNY": imgChar.sprite = sprtStage[3];
                txtTop.text = "Ball Dancing"; txtBottom.text = "";
                txtTop.color = clr[3];
                ChangeLetterColor(clr[3]);
                break;
            case "FUNNY": imgChar.sprite = sprtStage[4];
                txtTop.text = ""; txtBottom.text = "Face Fox";
                txtBottom.color = clr[4];
                ChangeLetterColor(clr[4]);
                break;
            case "ORANGE": imgChar.sprite = sprtStage[5];
                txtTop.text = "One"; txtBottom.text = "Ostrich";
                txtBottom.color = clr[5]; txtTop.color = clr[5];
                ChangeLetterColor(clr[5]);
                break;
            case "ELEGANT": imgChar.sprite = sprtStage[6];
                txtTop.text = ""; txtBottom.text = "Evening Eel";
                txtBottom.color = clr[6];
                ChangeLetterColor(clr[6]);
                break;
            case "GOATS": imgChar.sprite = sprtStage[7];
                txtTop.text = "Gallant Galloping"; txtBottom.text = "";
                txtTop.color = clr[7];
                ChangeLetterColor(clr[7]);
                break;
            case "KANGAROO": imgChar.sprite = sprtStage[8];
                txtTop.text = "Kicking"; txtBottom.text = "Kite";
                txtTop.color = clr[8]; txtBottom.color = clr[8];
                ChangeLetterColor(clr[8]);
                break;
            case "SCISSORS": imgChar.sprite = sprtStage[9];
                txtTop.text = "Sam's Star"; txtBottom.text = "";
                txtTop.color = clr[9];
                ChangeLetterColor(clr[9]);
                break;
            case "TIME": imgChar.sprite = sprtStage[10];
                txtTop.text = "Teddy Tells"; txtBottom.text = "";
                txtTop.color = clr[10];
                ChangeLetterColor(clr[10]);
                break;
            case "XYLOPHONE": imgChar.sprite = sprtStage[11];
                txtTop.text = "Yan's Zebra"; txtBottom.text = "";
                txtTop.color = clr[11];
                ChangeLetterColor(clr[11]);
                break;
            case "VIOLIN": imgChar.sprite = sprtStage[12];
                txtTop.text = "Very Violet"; txtBottom.text = "";
                txtTop.color = clr[12];
                ChangeLetterColor(clr[12]);
                break;
            case "WALRUS": imgChar.sprite = sprtStage[13];
                txtTop.text = "Wendy's Wacky"; txtBottom.text = "";
                txtTop.color = clr[13];
                ChangeLetterColor(clr[13]);
                break;
            case "INDIGO": imgChar.sprite = sprtStage[14];
                txtTop.text = ""; txtBottom.text = "Iguana Island";
                txtTop.color = clr[14];
                ChangeLetterColor(clr[14]);
                break;
            default: break;
        }
        TiltObjects();
        //DisableItemsImage();
        ChangeItemsFont();
        FadeIn fIn = null;
        fIn = imgChar.GetComponent<FadeIn>();
        fIn.Play();

        ReScaleObjects(1.5f);
        Item.OnInsert += InsertItem;
        Item.OnBeginDrag += SetRecentSlot;
        Item.OnReturn += Return;
        //Item.OnBeginDrag += BeginDrag;
       
    }

    void ChangeLetterColor(Color32 clr32) {
        Text txt = null;
        for(int i=0; i<WordGameManager_2.ins.lstLetters.Count; i++)
        {
            txt = WordGameManager_2.ins.lstLetters[i].transform.GetChild(0).GetComponent<Text>();
            txt.color = clr32;
        }
    }

    //void DisableItemsImage()
    //{
    //    Image img = null;
    //    print("dasd");
    //    for(int i=0; i<InventoryManager.ins.items.Count; i++)
    //    {
    //        img = InventoryManager.ins.items[i].GetComponent<Image>();
    //        img.enabled = false;
    //    }
    //}

    void TiltObjects()
    {
        //delay the wordgamemanager to make this work on Start()
        for (int i = 0; i < WordGameManager_2.ins.lstLetters.Count; i++)
        {
            RandomTilt(WordGameManager_2.ins.lstLetters[i].transform);
        }
    }

    void RandomTilt(Transform t)
    {
        int rnd = Random.Range(0, 2);
     
        switch (rnd)
        {
            case 0:

                t.SetLocalZRot(Random.Range(0, 20f));
         
                break;
            case 1:

                t.SetLocalZRot(Random.Range(340, 360));
              
                break;
            default: break;
        }
    }

    void ChangeItemsFont()
    {
        Text txt = null;
        for(int i=0; i<WordGameManager_2.ins.lstLetters.Count; i++)
        {
            txt = WordGameManager_2.ins.lstLetters[i].transform.GetChild(0).GetComponent<Text>();
            txt.font = font;
        }
    }

    void ReScaleObjects(float scale) {
        for (int i=0; i<InventoryManager.ins.slots.Count; i++)
        {
            InventoryManager.ins.slots[i].transform.SetLocalWidth(scale);
            InventoryManager.ins.slots[i].transform.SetLocalHeight(scale);
        }
    }

  
    void Return(Transform t)
    {
        //audSrc.PlayOneShot(clipWrong);
        UI_SoundFX.ins.Play(clipWrong);
    }

    void InsertItem(Transform parent, Transform dis) {
        Text txt1 = null;
        Text txt2 = null;
        print("1. inserted item=" + parent.name);
        if (parent.GetComponent<Text>() != null) {
            txt1 = parent.GetComponent<Text>();
            txt2 = dis.GetChild(0).GetComponent<Text>();
            print("2." + txt1.text + "=" + txt2.text);
            if (txt1.text != txt2.text)
            {
                print("3. return=" + parent.name);
                Item itm = dis.GetComponent<Item>();
                itm.RecentParent = transRecentSlot;
                dis.SetParent(transRecentSlot);
                dis.SetLocalXPos(0);
                dis.SetLocalYPos(0);
                //audSrc.PlayOneShot(clipWrong);
            }
            else
            {
               
                //audSrc.PlayOneShot(clipFit);
                UI_SoundFX.ins.Play(clipFit);
            }
        }
      
    }

    void BeginDrag(GameObject obj)
    {
        
    }

    void SetRecentSlot(GameObject obj)
    {
        //audSrc.PlayOneShot(clipDrag);
		ScoreManager.ins.IncNumOfMoves();
        UI_SoundFX.ins.Play(clipDrag);
        transRecentSlot = obj.GetComponent<Item>().RecentParent;
    }

	public void RemoveSubscribers()
	{
		WordGameManager_2.RemoveSubscribers();
	}
}
