using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WhatDidYaSee_act1_Manager : MonoBehaviour {

    AudioSource audSrc;
	void Start () {
        InventoryManager.ins.eDragDir = EDragDirection.x;
      
        audSrc = GetComponent<AudioSource>();
        Item.OnDrop += Check;
		Item.OnBeginDrag += BeginDrag;
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            InventoryManager.ins.items[i].GetComponent<Item>().Locked = true;
        }
		ScoreManager.ins.AW();
        Invoke("Go", 3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void Go()
    {
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            InventoryManager.ins.items[i].GetComponent<Item>().Locked = false;
        }
    }
    void Check()
    {
        int cnt = 0;
        if(InventoryManager.ins.slots[0].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("0"))
        {
            cnt++;
        }
        if (InventoryManager.ins.slots[1].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("1"))
        {
            cnt++;
        }
        if (InventoryManager.ins.slots[2].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("2"))
        {
            cnt++;
        }
        if (InventoryManager.ins.slots[3].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("3"))
        {
            cnt++;
        }
        if (InventoryManager.ins.slots[4].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("4"))
        {
            cnt++;
        }
        if(cnt == 5)
        {
            Invoke("Done", 2f);
            print("WIN!");
            audSrc.Play();
        }
    }

	void BeginDrag(GameObject o)
	{
		ScoreManager.ins.IncNumOfMoves();
	}

    void Done()
    {
        ActivityDone.instance.Done();
    }
   
}
