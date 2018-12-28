using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class MemoryManager : MonoBehaviour {
    public static MemoryManager ins;

    public GameObject memory;
    public Transform setContainer;
    public Sprite[] sprtMem;
    GameObject obj;
    RectTransform rect;

    
    [SerializeField]
    List<Sprite> tempLstSprt = new List<Sprite>();
    
    [SerializeField]
    int score, index;
    [SerializeField]
    string strMemory1, strMemory2;
    bool next = true;
    GameObject objMemory1, objMemory2;

    AudioSource audSrc;
    [SerializeField]
    AudioClip wrongClip, correctClip;
    public List<Button> lstBtns; // this will help to avoid opening more than two items
    #region setter and getter

    public GameObject ObjMemory1 {
        set { objMemory1 = value; }
        get { return objMemory1; }
    }

    public GameObject ObjMemory2
    {
        set { objMemory2 = value; }
        get { return objMemory2; }
    }

    public string Memory1 {
        set { strMemory1 = value; }
        get { return strMemory1; }
    }

    public string Memory2
    {
        set { strMemory2 = value; }
        get { return strMemory2; }
    }

    #endregion

   
    void Awake()
    {
        ins = this;
     
    }

	void Start () {

        rect = setContainer.GetComponent<RectTransform>();
        //score acts like an index 0 and 4
        score = SaveTest.Set;
        audSrc = GetComponent<AudioSource>();
        Generate();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Generate()
    {
        if (next)
        {
            if (score == 0 || score == 2)
            {
				ScoreManager.ins.maxMove = 8;
                DestroyItems();
                Invoke("Level1", 0.5f);
            }
            else if (score == 4 || score == 7)
            {
				ScoreManager.ins.maxMove = 12;
                DestroyItems();
                Invoke("Level2", 0.5f);
            }
            else if (score == 10 || score == 14)
            {
				ScoreManager.ins.maxMove = 16;
                DestroyItems();
                Invoke("Level3", 0.5f);
            }
            else if (score == 18 || score == 23)
            {
				ScoreManager.ins.maxMove = 20;
                DestroyItems();
                Invoke("Level4", 0.5f);
                //Level2();
            }
            else if (score == 28 || score == 34)
            {
				ScoreManager.ins.maxMove = 24;
                DestroyItems();
                Invoke("Level5", 0.5f);
                //Level2();
            }
         
        }
        
        next = false;
    }
    void Done()
    {
        ActivityDone.instance.Done();
    }
    void SetMemories()
    {
        lstBtns.Clear();
        tempLstSprt.Clear();
        //lstBtns = new List<Button>();
        //tempLstSprt = new List<Sprite>();
        //store set sprites
        for (int i = 0; i < setContainer.childCount / 2; i++)
        {
            tempLstSprt.Add(RandomSprite(tempLstSprt));
        }
        tempLstSprt.Shuffle();
        //set the stored sprites to the item
        for (int i = 0; i < setContainer.childCount; i++)
        {
            Image img = setContainer.GetChild(i).GetChild(0).GetComponent<Image>();
            img.sprite = tempLstSprt[i];
          
        }
       
    }

    void DestroyItems() {
       //
        for (int i = 0; i < setContainer.childCount; i++)
        {
            Destroy(setContainer.GetChild(i).gameObject);
        }
    }

    public void ActivateItems(bool b)
    {
        for (int i = 0; i < lstBtns.Count; i++ )
        {
            lstBtns[i].enabled = b;
        }
    }

    void Next()
    {
        UI_SoundFX.ins.PlaySetFin();
    }

    public void Check()
    {
        if (strMemory1 == strMemory2 && (strMemory1 != "" && strMemory2 != ""))
        {
            print("Matched!");
            strMemory1 = "";
            strMemory2 = "";
            score++;
            audSrc.PlayOneShot(correctClip);
            if (score == 2 || score == 7 || score == 14 || score == 23 || score == 34)
            {
                Invoke("Next",1f);
                next = true;
            }
            else if(score == 4 || score == 10 || score == 18 || score == 28 || score == 40) {
                Invoke("Done", 1f);
                print("GAME OVER");
            }
           
            Invoke("FadeAwayMatchedMemories", 0.2f);
            Invoke("Generate", 3f);
            ActivateItems(false);
          //  return true;
        }
        else
        {
            ActivateItems(false);
            strMemory1 = "";
            strMemory2 = "";
            Invoke("CoverRecentMemories", 0.5f);
           
        }
       // return false;
    }

    void CoverRecentMemories()
    {
        try
        {
            //UI_SoundFX.ins.Play(wrongClip);
            audSrc.PlayOneShot(wrongClip);
            objMemory2.GetComponent<MemoryItem>().Cover();
            objMemory1.GetComponent<MemoryItem>().Cover();
            objMemory1 = null;
            objMemory2 = null;
        }
        catch(Exception ex){
            print(ex.Message);
        }
       
    }

    void FadeAwayMatchedMemories()
    {
        try
        {
            objMemory1.transform.parent.GetComponent<FadeOut>().Play();
            objMemory2.transform.parent.GetComponent<FadeOut>().Play();
            objMemory1 = null;
            objMemory2 = null;
            ActivateItems(true);
            audSrc.Play();
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
      
    }

    bool hasDuplicate(Sprite sprt)
    {
        for (int i = 0; i < tempLstSprt.Count; i++ )
        {
            if(sprt == tempLstSprt[i])
            {
                return true;
            }
        }
        return false;
    }

    Sprite RandomSprite(List<Sprite> tempList)
    {
        int x = 0;
        bool b = false;

        while (!b)
        {
            x = UnityEngine.Random.Range(0, sprtMem.Length);
            if (!hasDuplicate(sprtMem[x]))
            {
                tempList.Add(sprtMem[x]);
                return sprtMem[x];
            }
        }

        return null;
    }

    #region levels

    void Level1()
    {
        rect.SetHeight(220f);
        rect.SetWidth(220f);
        for (int i = 0; i < 4; i++)
        {
            obj = (GameObject)Instantiate(memory, transform.position, Quaternion.identity);
          
            obj.transform.SetParent(setContainer);
            obj.transform.SetLocalHeight(1f);
            obj.transform.SetLocalWidth(1f);
           
        }
        SetMemories();
      
    }

    void Level2()
    {
        rect.SetHeight(220f);
        rect.SetWidth(320f);
        for (int i = 0; i < 6; i++)
        {
            obj = (GameObject)Instantiate(memory, transform.position, Quaternion.identity);
       
            obj.transform.SetParent(setContainer);
            obj.transform.SetLocalHeight(1f);
            obj.transform.SetLocalWidth(1f);
          
        }
        SetMemories();
    }

    void Level3()
    {
        rect.SetHeight(220f);
        rect.SetWidth(420f);
        for (int i = 0; i < 8; i++)
        {
            obj = (GameObject)Instantiate(memory, transform.position, Quaternion.identity);

            obj.transform.SetParent(setContainer);
            obj.transform.SetLocalHeight(1f);
            obj.transform.SetLocalWidth(1f);

        }
        SetMemories();
    }
    void Level4()
    {
        rect.SetHeight(100f);
        rect.SetWidth(600f);
        for (int i = 0; i < 10; i++)
        {
            obj = (GameObject)Instantiate(memory, transform.position, Quaternion.identity);

            obj.transform.SetParent(setContainer);
            obj.transform.SetLocalHeight(1f);
            obj.transform.SetLocalWidth(1f);

        }
        SetMemories();
    }
    void Level5()
    {
        rect.SetHeight(300f);
        rect.SetWidth(400f);
        setContainer.SetLocalYPos(90f);
        for (int i = 0; i < 12; i++)
        {
            obj = (GameObject)Instantiate(memory, transform.position, Quaternion.identity);

            obj.transform.SetParent(setContainer);
            obj.transform.SetLocalHeight(1f);
            obj.transform.SetLocalWidth(1f);

        }
        SetMemories();
    }
    #endregion levels
}
