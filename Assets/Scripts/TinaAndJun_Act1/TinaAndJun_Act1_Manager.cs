using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class TinaAndJun_Act1_Manager : MonoBehaviour {

    public Sprite[] sprts;
    public GameObject item;
    public Transform itemContainer;

    public AudioClip[] audClip;
    AudioSource audSrc;

    int sprtIndex = 0;
    [SerializeField]
    int pts = 0;
    [SerializeField]
    int activityPts = 0;

	void Start () {
        //sprts.Shuffle();
        Generate();
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.AW();
	}


    void Generate() {
        Item.OnDrop += IncPts;
        Item.OnInsert += Insert;
        Item.OnReturn += WrongInsert;
		Item.OnBeginDrag += Drag;
        for (int i=0; i<4; i++)
        {
            GameObject o = (GameObject)Instantiate(item, new Vector3(0,0,0), Quaternion.identity);
            Reparent(o.transform);
            InventoryManager.ins.items.Add(o);
        }
        InventoryManager.ins.InitSlotEvents();
        ChangeSprite();
        SetItemsID();
        Shrink.OnShrinkEnd -= DestroyItem;
        Shrink.OnShrinkEnd += DestroyItem;
    }

	void Drag(GameObject o)
	{
		ScoreManager.ins.IncNumOfMoves();
	}

    void Reparent(Transform item)
    {
        Slot s = null;
        bool f = false;
       
        for(int i=0; i<InventoryManager.ins.slots.Count; i++)
        {
            s = InventoryManager.ins.slots[i].GetComponent<Slot>();
            if (s.empty && !f)
            {
                item.SetParent(InventoryManager.ins.slots[i].transform);
                item.SetLocalXPos(0);
                item.SetLocalYPos(0);
                item.GetComponent<RectTransform>().SetHeight(160);
                item.GetComponent<RectTransform>().SetWidth(160);
                item.SetLocalWidth(1);
                item.SetLocalHeight(1);
                //item.SetLocalHeight(160);
                //item.SetLocalWidth(160);
                s.empty = false;
                f = true;
            }
        }
        
    }

    void ChangeSprite()
    {
        if (sprtIndex < sprts.Length - 1)
        {
            for (int i = 0; i < InventoryManager.ins.items.Count; i++)
            {
                Image img = null;
                img = InventoryManager.ins.items[i].GetComponent<Image>();
                img.sprite = sprts[sprtIndex];
                sprtIndex++;
            }
        }
        else {
            //GAME OVER
            print("Game over");
            ActivityDone.instance.Done();
            Destroy(itemContainer.gameObject);
        }
        
    }

    void SetItemsID()
    {
        Item itm = null;
        for (int i=0; i<InventoryManager.ins.items.Count; i++)
        {
            itm = InventoryManager.ins.items[i].GetComponent<Item>();
            string s = InventoryManager.ins.items[i].GetComponent<Image>().sprite.name.ToUpper();
            if (s.Contains("EE") || s.Contains("EA"))
            {
                itm.eColor = EColor.red;
            }
            else if (s.Contains("OO"))
            {
                itm.eColor = EColor.green;
            }
        }
    }

    void DestroyItems(){
        Item.OnDrop -= IncPts;
        Item.OnReturn -= WrongInsert;
        Item.OnInsert -= Insert;
		Item.OnBeginDrag -= Drag;
        for(int i=0; i<InventoryManager.ins.items.Count; i++)
        {
            Destroy(InventoryManager.ins.items[i]);
        }
        //for(int i=0; i<InventoryManager.ins.slots.Count; i++)
        //{
        //    InventoryManager.ins.slots[i].GetComponent<Slot>().CheckSlot();
        //}
        InventoryManager.ins.items.Clear();
    }

    void Insert(Transform slot, Transform item)
    {
        print(slot.name);
        Slot s = slot.GetComponent<Slot>();
        Shrink shrink = item.GetComponent<Shrink>();
        shrink.Play();
        //Destroy(item.gameObject);
        s.empty = true;
    }

    void DestroyItem(GameObject obj)
    {
        Destroy(obj);
    }

    IEnumerator IENext()
    {
        
        yield return new WaitForSeconds(1f);
        DestroyItems();
        Generate();
    }
    void IncPts()
    {
        pts++;
        audSrc.clip = audClip[0];
        audSrc.Play();
        if (pts == 4)
        {
            activityPts++;
            pts = 0;
            StartCoroutine(IENext());
        }
    }

    void WrongInsert(Transform t)
    {
        audSrc.clip = audClip[1];
        audSrc.Play();
    
    }
}
