using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class WordGameManager_2 : MonoBehaviour {
    public static WordGameManager_2 ins;

    public string[] wordArr;
    //inventory canvas
    public GameObject groupClue, groupLetters;
    public GameObject slot, itemLetter;
    //end inventory canvas
    public List<GameObject> lstLetters;
    public delegate void ActionGenerate();
    public static event ActionGenerate OnGenerate;

    [SerializeField]
    int wordArrIndex = 0, pts = 0;
   
    //static int colorIndex = 0;
    public string GetWord() {
        return wordArr[wordArrIndex];
    }
    
    void Awake()
    {
        wordArrIndex = SaveTest.Set;
    }
    void Start()
    {
        ins = this;
        //wordArr.Shuffle();
        //DO NOT SHUFFLE
        //GenerateWord();
        InventoryManager.ins.uniqueKey = false;
    }

	public int GetSet()
	{
		return wordArrIndex;
	}

    public static void RemoveSubscribers()
    {
        OnGenerate = delegate { };
    }

    void SpawnSlotTo(Transform container, List<GameObject> lstInventory)
    {
        GameObject o = null;
     
        o = (GameObject)Instantiate(slot, transform.position, Quaternion.identity);
        o.transform.SetParent(container);
        o.transform.SetLocalXPos(0);
        o.transform.SetLocalYPos(0);
        o.transform.SetLocalWidth(1);
        o.transform.SetLocalHeight(1);
        //o.GetComponent<Slot>().eColor = Storybook.GetColor(colorIndex);
        lstInventory.Add(o);

    }

    public void GenerateWord()
    {
      
        if (wordArrIndex < wordArr.Length - 1 && pts < 3)
        {
            //colorIndex = 0;
            for (int i = 0; i < wordArr[wordArrIndex].Length; i++)
            {
                SpawnSlotTo(groupClue.transform, InventoryManager.ins.slots);
                SpawnSlotTo(groupLetters.transform, InventoryManager.ins.slots);
                //colorIndex++;
            }
            ChangeClueLetters();
            DisableSlotsText();
            //colorIndex = 0;
            for (int i = 0; i < wordArr[wordArrIndex].Length; i++)
            {
                SpawnLetterTo(groupLetters.transform.GetChild(i), InventoryManager.ins.items, lstLetters);
                //colorIndex++;
            }

            ChangeItemsText();

            //add check event on item`s drop event
            for (int i = 0; i < InventoryManager.ins.items.Count; i++)
            {
                Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
                //itm.OnDrop += CheckWord;
            }
            Item.OnDrop += CheckWord;
            //check slot if empty
            for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
            {
                Slot s = InventoryManager.ins.slots[i].GetComponent<Slot>();
                s.CheckSlot();
            }
            //add slot events to item`s drop event
            InventoryManager.ins.InitSlotEvents();

            if (OnGenerate != null)
            {
                OnGenerate();
			}else{

				ABCCircus_Act2_Manager.ins.ConnectOnGenerate();
				GenerateWord();
			}
        }
        else {
			RemoveSubscribers();
            ActivityDone.instance.Done();
            print("GAMEOVER!");
        }
      
    }

    void ChangeClueLetters()
    {
        for (int i=0; i<groupClue.transform.childCount; i++)
        {
            //Text txt = groupClue.transform.GetChild(i).GetComponent<Text>();
            //txt.text = wordArr[wordArrIndex][i].ToString();
            groupClue.transform.GetChild(i).GetComponent<Text>().text = wordArr[wordArrIndex][i].ToString();
            groupClue.transform.GetChild(i).GetComponent<Slot>().eAlphabet = Storybook.SetEnumAlphabetValue(wordArr[wordArrIndex][i]);
        }
    }

    void DisableSlotsText() {
        for (int i=0; i<groupLetters.transform.childCount; i++)
        {
            groupLetters.transform.GetChild(i).GetComponent<Text>().enabled = false;
        }
    }

    void SpawnLetterTo(Transform parent, List<GameObject> lstInventory, List<GameObject> lstLetters)
    {
        GameObject o = null;
        
        o = (GameObject)Instantiate(itemLetter, transform.position, Quaternion.identity);
        o.transform.SetParent(parent);
        o.transform.SetLocalXPos(0);
        o.transform.SetLocalYPos(0);
        o.transform.SetLocalWidth(1);
        o.transform.SetLocalHeight(1);
        //o.GetComponent<Item>().eColor = Storybook.GetColor(colorIndex); //letters will be suffled wont work here
        lstInventory.Add(o);
        lstLetters.Add(o);
    }

    void ChangeItemsText()
    {
        //Text txt = null;
        lstLetters.Shuffle();
       // colorIndex = 0;
        for(int i=0; i<lstLetters.Count; i++)
        {
            //lstLetters[i].GetComponent<Item>().eColor = Storybook.GetColor(colorIndex);
            //txt = lstLetters[i].transform.GetChild(0).GetComponent<Text>();
            //txt.text = wordArr[wordArrIndex][i].ToString();
            lstLetters[i].transform.GetChild(0).GetComponent<Text>().text = wordArr[wordArrIndex][i].ToString();
            lstLetters[i].GetComponent<Item>().eAlphabet = Storybook.SetEnumAlphabetValue(wordArr[wordArrIndex][i]);
            //colorIndex++;
        }
        //lstLetters.Shuffle();
    }

    public bool WordSolved()
    {
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
        if (str == wordArr[wordArrIndex])
        {
           
            return true;
            //next
        }
        return false;
    }

    public void CheckWord()
    {
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
        if (str == wordArr[wordArrIndex])
        {
            print("Wow! Fantastic baby!");
            UI_SoundFX.ins.PlaySetFin();
            wordArrIndex++;
            pts++;
            StartCoroutine(IEGoNext());
         //next
        }
        else
        {
            print("DONT GIVE UP");
        }
    }

    void DestroyAll()
    {
        //DisableItems(lstPoolClueLetters, false);
        for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
        {
            //InventoryManager.ins.slots[i].SetActive(false);
            InventoryManager.ins.slots[i].GetComponent<Slot>().RemoveEvent();
            Destroy(InventoryManager.ins.slots[i]);
        }
        for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
        {
            //InventoryManager.ins.slots[i].SetActive(false);
            Destroy(InventoryManager.ins.slots[i]);
        }
        //remove items from the slot
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            //Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
            //itm.OnDrop -= CheckWord;
            Destroy(InventoryManager.ins.items[i]);
        }
     
        InventoryManager.ins.slots.Clear();
        InventoryManager.ins.items.Clear();
        lstLetters.Clear();
       
    }

    IEnumerator IEGoNext()
    {
		yield return new WaitForSeconds(0.5f);
        DestroyAll();

        yield return new WaitForSeconds(1f);
        GenerateWord();
    }
}
