using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class  MatchingManager : MonoBehaviour {
    //Inherit this
    //not used in colorsMixed
    //separate the slot and DO NOT ADD the slotContainer childs to INVENTORYMANAGER.SLOTS when using this script it is just useless to add.
    public Transform slotItemContainer, slotContainer; 

    public GameObject[] objItem;
    public Sprite[] sprtSlot; //target slot image sprite


    
    public List<Slot> lstSlot; //add the slotContainer childs here
    List<GameObject> _lstTempObjItem = new List<GameObject>(); //only for shuffling the items
   [SerializeField]
    int index = 0;
    [SerializeField]
    int setLen;
    [SerializeField]
    int maxPts;
    public int SetLen {
        set { setLen = value;
            maxPts = setLen;
        }
        get { return setLen; }
    }

    public int Index {
        set { index = value; }
        get { return index; }
    }

    public int MaxPts{
        get { return maxPts; }
    }

 

   
    public virtual void SpawnSet()
    {
      
        GameObject o = null;
        Item itm = null;
        InventoryManager.ins.items.Clear();
        try
        {
            for (int i = 0; i < SetLen; i++)
            {
                o = (GameObject)Instantiate(objItem[index], new Vector3(0, 0, 0), Quaternion.identity);
                itm = o.GetComponent<Item>();
                InventoryManager.ins.items.Add(o);
                _lstTempObjItem.Add(o);
                //ReparentItemTo(slotItemContainer, o.transform);
                MatchSlotTo(itm, i);
                ChangeTargetSlotSprite(lstSlot[i], index);
                index++;

            }

            InventoryManager.ins.InitSlotEvents();
        }
        catch (Exception ex)
        {
            //
            Invoke("Done",1f);
            print("GAME OVER");
            print(ex.Message);
        }
       
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
    public void ShuffleItems()
    {
        //Randomize the position of items
        _lstTempObjItem.Shuffle();
        for (int i = 0; i < _lstTempObjItem.Count; i++)
        {
            ReparentItemTo(slotItemContainer, _lstTempObjItem[i].transform);
        }
        _lstTempObjItem.Clear();
        InventoryManager.ins.SetItemsOriginToItsCurrentSlot();
    }

    void ReparentItemTo(Transform parentContainer, Transform item)
    {
        bool isReparented = false;
        for (int i=0; i<parentContainer.childCount; i++)
        {
            Slot s = parentContainer.GetChild(i).GetComponent<Slot>();
            if (s.empty && !isReparented)
            {
                s.empty = false;
                item.SetParent(parentContainer.GetChild(i).transform);
                item.SetLocalXPos(0);
                item.SetLocalYPos(0);
                item.SetLocalWidth(1);
                item.SetLocalHeight(1);
                isReparented = true;
            }
        }
    }

    void MatchSlotTo(Item itm, int n)
    {
        Slot s = null;
        s = InventoryManager.ins.slots[n].GetComponent<Slot>();
        s.eColor = itm.eColor;
    }

    void ChangeTargetSlotSprite(Slot slot, int n)
    {
        Image img = slot.GetComponent<Image>();
        img.sprite = sprtSlot[n];
    }

    public void DestroyItems()
    {
        InventoryManager.ins.RemoveSlotEvents();
        for (int i=0; i<InventoryManager.ins.items.Count; i++)
        {
            Destroy(InventoryManager.ins.items[i]);
        }
    }

    public void EmptySlots()
    {
        
        for (int i=0; i<InventoryManager.ins.slots.Count; i++)
        {
            Slot s = null;
            s = InventoryManager.ins.slots[i].GetComponent<Slot>();
            s.empty = true;
        }
        
    }

   
}
