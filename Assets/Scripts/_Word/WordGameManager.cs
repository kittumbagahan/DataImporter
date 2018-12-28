using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class WordGameManager : MonoBehaviour {
	public static WordGameManager ins;
	public List<string> wordList;

	//inventory canvas
	public GameObject groupClue, groupLetters;
	public GameObject slot, itemLetter;
	//end inventory canvas
	public List<GameObject> lstPoolClueLetters;
	public List<GameObject> lstPoolMissingLetter;

    public delegate void ActionGenerateWord();
    //public ActionGenerateWord delGenerateWord;
    public static event ActionGenerateWord OnGenerate;
    public float dist = 50f;
    
	List<GameObject> emptyList = new List<GameObject>(); //for emptying list on next word
	string strExtracted;
	string strClue;  //clue
	string word;
    [SerializeField]
    int setIndex = 0, pts = 0;
    int rnd=0;
    static int colorIndex = 0;
    [SerializeField]
    Color32 clr32Correct;
  
    public int SetIndex {
        set { setIndex = value; }
		get{return setIndex;}
    }

    public int Pts { get { return pts; } }
    public string Word {
        get { return word; }
    }

    void Awake()
    {
        
    }

    void Start()
    {
        ins = this;
        //wordList.Shuffle();
        //GenerateWord();
        StartCoroutine(IEGoNext());
        InventoryManager.ins.uniqueKey = false;
        //print(MonoExtension.RandomLetter("BCDERFGHIJKLMNOPQRSTUVWXY"));
        
        /*dont do it here*/
        //setIndex = _SaveTest.Load(StoryBook.FAVORITE_BOX, Module.MISSINGLETTER);
        
    }

    public static void RemoveSubscribers()
    {
        OnGenerate = delegate { };
    }

	void ExtractWord()
	{
		strExtracted = "";
		int cnt = strClue.Length/2;

		while (cnt > 0) {
			rnd = UnityEngine.Random.Range(0,strClue.Length);
			strExtracted += strClue[rnd];
			strClue = strClue.Remove(rnd, 1);
			cnt--;
			//strClue = strClue.Replace(strClue[rnd]
		}

	}

	void SpawnSlotTo(Transform container, List<GameObject> lstInventory, char charVal)
	{
		GameObject o = null;
		/*
		bool f=false;

		for(int i=0; i<lstInventory.Count && !f; i++)
		{
			if(lstInventory[i].activeInHierarchy == false)
			{
				lstInventory[i].SetActive(true);
				f = true;
				print("wow");
			}
		}
		if(!f){
			o = (GameObject)Instantiate(slot,transform.position, Quaternion.identity);
			lstInventory.Add(o);
		}
		*/
		o = (GameObject)Instantiate(slot,transform.position, Quaternion.identity);
		o.transform.SetParent(container);
		o.transform.SetLocalXPos(0);
		o.transform.SetLocalYPos(0);
        o.transform.SetLocalWidth(1);
        o.transform.SetLocalHeight(1);
        //o.GetComponent<Slot>().eColor = Storybook.GetColor(colorIndex);
        o.GetComponent<Slot>().eAlphabet = Storybook.SetEnumAlphabetValue(charVal);
		lstInventory.Add(o);
	
	}

	void SpawnLetterTo(Transform parent, List<GameObject> lstInventory, List<GameObject> pool, char charVal)
	{
		GameObject o = null;
		//Text txt = null;
		/*
		bool f=false;
		for(int i=0; i<lstInventory.Count; i++)
		{
			if(lstInventory[i].activeInHierarchy == false)
			{
				lstInventory[i].SetActive(true);
				f = true;
			}
		}
		if(!f)
		{
			o = (GameObject)Instantiate(itemLetter,transform.position, Quaternion.identity);
			lstInventory.Add(o);
			pool.Add(o);
		}
		*/

		o = (GameObject)Instantiate(itemLetter,transform.position, Quaternion.identity);
		o.transform.SetParent(parent);
		o.transform.SetLocalXPos(0);
		o.transform.SetLocalYPos(0);
        o.transform.SetLocalWidth(1);
        o.transform.SetLocalHeight(1);
        o.GetComponent<Item>().eAlphabet = Storybook.SetEnumAlphabetValue(charVal);
        //txt = o.transform.GetChild(0).GetComponent<Text>();
		lstInventory.Add(o);
		pool.Add(o);
       
	}

	void ChangeExtractedTextsValueTo(List<GameObject> pool, string value)
	{
		int n = 0;

		for(int i=0; i<value.Length; i++)
		{
            Item itm = pool[i].GetComponent<Item>();
			Text txt = pool[i].transform.GetChild(0).GetComponent<Text>();
            txt.text = value[n].ToString();
            itm.eAlphabet = Storybook.SetEnumAlphabetValue(char.Parse(txt.text));
			n++;
		}
	}


	void AlignSlots(Transform container,float dist, string str)
	{
        float screenWidth = Screen.width/2; //center screen
        float tempDist = dist;
        //if odd tempDist -=50
        for (int i=3;i<str.Length; i++)
        {
            if (i%2 != 0) {
                tempDist -= dist;
                print(i + "odd");
            }

        }
        tempDist = screenWidth - ((tempDist * -1) * 2);
        for (int i=0; i<container.childCount; i++){
			Transform t = null;
			t = container.GetChild(i).GetComponent<Transform>();
			t.SetXPos(tempDist);
			tempDist += dist;
		}
	}

    public bool WordSolved() {
        string str = "";
        for (int i = 0; i < groupClue.transform.childCount; i++)
        {
            Transform slot = groupClue.transform.GetChild(i);
            Transform item = null;
            Text txt = null;
            if (slot.childCount >= 1)
            {
                item = slot.GetChild(0);
            }

            if (item != null && item.childCount >= 1)
            {
                txt = item.GetChild(0).GetComponent<Text>();
            }
            if (txt != null)
            {
                str += txt.text;
            }

        }
        if (str == word)
        {
            
            return true;
        }
      
        return false;
    }

	public void CheckWord()
	{
        //print("check");
		string str="";
		for(int i=0; i<groupClue.transform.childCount; i++)
		{
			Transform slot = groupClue.transform.GetChild(i);
			Transform item = null;
			Text txt = null;
			if(slot.childCount >= 1)
			{
				item = slot.GetChild(0);
			}
			
			if(item != null && item.childCount >= 1)
			{
				txt = item.GetChild(0).GetComponent<Text>();
			}
			if(txt != null)
			{
				str += txt.text;
			}

		}
		if(str == word)
		{
			print("Wow! Fantastic baby!");
            UI_SoundFX.ins.PlaySetFin();
			setIndex++;
            pts++;
           
			StartCoroutine(IEGoNext());
			//Next();
		}else if(str.Length == word.Length && str != word){
           // StartCoroutine(IEWrong(InventoryManager.ins.items));
			print("DONT GIVE UP");
		}
	}

	void DisableItems(List<GameObject> lstObj, bool val)
	{
		Item itm=null;
		//GameObject o=null;
		for(int i=0; i<lstObj.Count; i++)
		{
			itm = lstObj[i].GetComponent<Item>();
			//itm.enabled = false;
			itm.Locked = val;
		}
	}

	void DestroyAll()
	{
		//DisableItems(lstPoolClueLetters, false);
        Item.RemoveSubscribers();
		for(int i=0; i<InventoryManager.ins.slots.Count; i++){
            //InventoryManager.ins.slots[i].SetActive(false);
            InventoryManager.ins.slots[i].GetComponent<Slot>().RemoveEvent();
            Destroy(InventoryManager.ins.slots[i]);
		}
		//remove items from the slot
		for(int i=0; i<InventoryManager.ins.items.Count; i++){
			Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
			//itm.OnDrop -= CheckWord;
            
			Destroy(InventoryManager.ins.items[i]);
			/*
			Transform t = null;
			t = InventoryManager.ins.items[i].transform;
			t.SetParent(this.transform);
			t.gameObject.SetActive(false);
			*/
		}
        Item.OnDrop -= CheckWord;
        
		//remove slots from the container
		/*
		for(int i=0; i<InventoryManager.ins.slots.Count; i++){
			Destroy(InventoryManager.ins.slots[i]);

			Transform t = null;
			t = InventoryManager.ins.slots[i].transform;
			t.SetParent(this.transform);
			t.gameObject.SetActive(false);

		}
		*/

		InventoryManager.ins.slots.Clear();
		InventoryManager.ins.items.Clear();
		lstPoolClueLetters.Clear();
		lstPoolMissingLetter.Clear();
		/*
		InventoryManager.ins.slots = emptyList;
		InventoryManager.ins.items = emptyList;
		lstPoolClueLetters = emptyList;
		lstPoolMissingLetter = emptyList;
		*/
	}
	/*
	String RandomLetter(){
		string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		return str[UnityEngine.Random.Range(0,str.Length)].ToString();
	}
    */
	void GenerateWord()
	{
        if (pts < 3)
        {
            try
            {
                word = wordList[setIndex];
                print(word);
                strClue = word;

                //colorIndex = 0;

                ExtractWord();
                int clueIndex = 0; //for aligning clue letters
                colorIndex = 0;
                //spawn slots
                for (int i = 0; i < word.Length; i++)
                {
                    SpawnSlotTo(groupClue.transform, InventoryManager.ins.slots, word[i]);
                    //colorIndex++;
                }
                for (int i = 0; i < strExtracted.Length + 1; i++) //+1 extra slot for extra letter
                {
                    SpawnSlotTo(groupLetters.transform, InventoryManager.ins.slots, char.Parse("@")); // this is okay to let it like this any value wont affect the game
                }

                #region spawn letters
                for (int i = 0; i < strExtracted.Length + 1; i++) //+1 for extra letter
                {

                    Transform slot = groupLetters.transform.GetChild(i).transform;
                    SpawnLetterTo(slot, InventoryManager.ins.items, lstPoolMissingLetter, strExtracted[0]); //set Enum alphabet value fails here

                }

                for (int i = 0; i < word.Length; i++)
                {
                    Transform slot = groupClue.transform.GetChild(i).transform;
                    try
                    {
                        if (word[i] == strClue[clueIndex])//strClue[clueIndex])
                        {
                            clueIndex++;
                            SpawnLetterTo(slot, InventoryManager.ins.items, lstPoolClueLetters, word[i]);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
                #endregion

                #region read more..
                ChangeExtractedTextsValueTo(lstPoolClueLetters, strClue);
                lstPoolMissingLetter.Shuffle();
                ChangeExtractedTextsValueTo(lstPoolMissingLetter, strExtracted + MonoExtension.RandomLetter(word));
                DisableItems(lstPoolClueLetters, true);
                //Add Checkword method on Item delegates
                for (int i = 0; i < InventoryManager.ins.items.Count; i++)
                {
                    Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
                    //itm.OnDrop += CheckWord;
                }
                Item.OnDrop += CheckWord;
                //for(int i=0; i<InventoryManager.ins.slots.Count; i++)
                //{
                //    Slot s = InventoryManager.ins.slots[i].GetComponent<Slot>();
                //    s.CheckSlot();
                //}
                InventoryManager.ins.InitSlotEvents();
                // AlignSlots(groupClue.transform, dist, word);
                //AlignSlots(groupLetters.transform, dist, strExtracted);
                if (OnGenerate != null)
                {
                    OnGenerate();
                    print("wowoweee");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // ActivityDone.instance.Done();
                print("All words has been answered. GAME OVER");
            }
        }
        else 
        {
            Invoke("Done",0.2f);
        } //ActivityDone.instance.Done();
#endregion
    }

    void Done() {
		print(ScoreManager.ins.GetGrade());
        ActivityDone.instance.Done();
    }

	IEnumerator IEGoNext(){
        Text txt = null;
        Item itm = null;
        for (int i = 0; i <InventoryManager.ins.items.Count; i++)
        {
            itm = InventoryManager.ins.items[i].GetComponent<Item>();
            if (InventoryManager.ins.items[i].transform.parent.parent == groupClue.transform)
            {
                txt = InventoryManager.ins.items[i].transform.GetChild(0).GetComponent<Text>();
                txt.color = clr32Correct;
            }

        }
        yield return new WaitForSeconds(0.5f);
		DestroyAll();
		yield return new WaitForSeconds(0.5f);
		GenerateWord();
	}
  
     IEnumerator IEWrong(List<GameObject> list)
    {
        Text txt = null;
        Item itm = null;
        Slot s = null;
        for (int i = 0; i < list.Count; i++)
        {
            itm = list[i].GetComponent<Item>();
            if (list[i].transform.parent.parent == groupClue.transform)
            {
                txt = list[i].transform.GetChild(0).GetComponent<Text>();
                txt.color = Color.red;
            }

        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < list.Count; i++)
        {
            s = list[i].transform.parent.GetComponent<Slot>();
            s.empty = true;
            list[i].GetComponent<Item>().Return();
            txt = list[i].transform.GetChild(0).GetComponent<Text>();
            txt.color = Color.black;
        }
    }

     void OnDestroy()
     {
         OnGenerate = delegate { };
     }

     //EAlphabet SetEnumAlphabetValue(char c)
     //{ 
     //   switch(c.ToString().ToLower())
     //   {
     //       case "a": return EAlphabet.a;
     //       case "b": return EAlphabet.b;
     //       case "c": return EAlphabet.c;
     //       case "d": return EAlphabet.d;
     //       case "e": return EAlphabet.e;
     //       case "f": return EAlphabet.f;
     //       case "g": return EAlphabet.g;
     //       case "h": return EAlphabet.h;
     //       case "i": return EAlphabet.i;
     //       case "j": return EAlphabet.j;
     //       case "k": return EAlphabet.k;
     //       case "l": return EAlphabet.l;
     //       case "m": return EAlphabet.m;
     //       case "n": return EAlphabet.n;
     //       case "o": return EAlphabet.o;
     //       case "p": return EAlphabet.p;
     //       case "q": return EAlphabet.q;
     //       case "r": return EAlphabet.r;
     //       case "s": return EAlphabet.s;
     //       case "t": return EAlphabet.t;
     //       case "u": return EAlphabet.u;
     //       case "v": return EAlphabet.v;
     //       case "w": return EAlphabet.w;
     //       case "x": return EAlphabet.x;
     //       case "y": return EAlphabet.y;
     //       case "z": return EAlphabet.z;
             
     //       default: return EAlphabet.non;
     //   }
     //}
}
