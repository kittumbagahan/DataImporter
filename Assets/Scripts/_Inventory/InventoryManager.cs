using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
/*
ONLY ONE ITEM CAN BE DRAG AT TIME
 */

//public enum EColor
//{
    
//    non, red, green, blue, purple, orange, yellow, pink, teal, white, black, brown, cyan
//}
//public enum EDragDirection
//{
//    all, x, y
//}


public class InventoryManager : MonoBehaviour {
	public static InventoryManager ins;

    public bool EnSwap = false; //if items swapping is allowed, RECOMMENDED USE FOR PUZZLE
    public EDragDirection eDragDir;

	public List<GameObject> items;
	public List<GameObject> slots;
    public float checkDist = 20f;
    public bool uniqueKey=true; //by alphabet = false or not unique or by color = true or unique
    bool dragging=false;
	GameObject draggedObj=null;
    [SerializeField]
    Transform transClickedItemRecentSlot; //use for swapping

    //public delegate void ActionReparent(GameObject obj);
    //public ActionReparent delReparent;

	#region setter X getter
	public bool Dragging{
		set {dragging = value;}
		get {return dragging;}
	}

	public GameObject DraggedObject{
		set{draggedObj = value;}
		get{return draggedObj;}
	}

    public Transform TransItemRecentSlot {
        set { transClickedItemRecentSlot = value; }
        get { return transClickedItemRecentSlot; }
    }
	#endregion

	void Awake(){
        RemoveSubscribers();
		ins = this;
	}
	void Start () 
	{
			
	}

    public void RemoveSubscribers()
    {
       // delReparent = delegate { };
    }
	public List<GameObject> ListSlot{
		set { slots = value;}
		get {return slots; }
	}

    //if matching manager reparenting does not work use this
    public void SetItemsOriginToItsCurrentSlot() {
        if (items.Count > 1)
        {
            try
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Item itm = items[i].GetComponent<Item>();
                    itm.SetParentToParent();
                }
            } catch (MissingReferenceException ex)
            {
                print(ex.ToString().ToUpper());
                print("GAME OVER");
            }
            
        }
    }

    //DO N0T USE THIS
    /*
    public bool Swap(Slot s, Item itm, Transform trans_SlotB, GameObject obj_A)
    {
        Transform trans_SlotA = transClickedItemRecentSlot;
        GameObject obj_B = null;
        if (trans_SlotB.childCount == 1){
            obj_B = trans_SlotB.GetChild(0).gameObject;
        }
        print(trans_SlotB.name);
        print(trans_SlotA.name);
        try
        {
            if (!s.empty && Vector2.Distance(trans_SlotB.position, obj_A.transform.position) < checkDist && itm.eColor == s.eColor)
            {
                print("wow");
                obj_B.transform.SetParent(trans_SlotA);
                obj_B.transform.SetLocalXPos(0);
                obj_B.transform.SetLocalYPos(0);
                obj_A.transform.SetParent(trans_SlotB);
                obj_A.transform.SetLocalXPos(0);
                obj_A.transform.SetLocalYPos(0);
                return true;
            }
        }
        catch (NullReferenceException ex)
        {
            print(ex.ToString());
        }
        return false;
    }
	*/
    public bool IsSwapped(GameObject obj_A)
    {
        if(EnSwap == true)
        {
            Slot s = null;
            Item itm_A, itm_B = null;
            Transform trans_SlotB = null;
            itm_A = obj_A.GetComponent<Item>();
            //---------------------------------------------
            Transform trans_SlotA = transClickedItemRecentSlot;
            GameObject obj_B = null;
            //---------------------------------------------

            for (int i = 0; i < slots.Count; i++)
            {
                s = slots[i].GetComponent<Slot>();
                trans_SlotB = slots[i].GetComponent<Transform>();
                if (trans_SlotB.childCount == 1)
                {
                    obj_B = trans_SlotB.GetChild(0).gameObject;
                    itm_B = obj_B.GetComponent<Item>();
                }
                //print(transClickedItemRecentSlot.name + " " + itm_A.Origin);
                if (!s.empty && Vector2.Distance(trans_SlotB.position, obj_A.transform.position) < checkDist && itm_A.eColor == s.eColor
                    && transClickedItemRecentSlot != trans_SlotB)
                {
                    obj_B.transform.SetParent(trans_SlotA);
                    obj_B.transform.SetLocalXPos(0);
                    obj_B.transform.SetLocalYPos(0);
                    obj_A.transform.SetParent(trans_SlotB);
                    obj_A.transform.SetLocalXPos(0);
                    obj_A.transform.SetLocalYPos(0);
                    itm_A.SetParentToParent();
                    itm_B.SetParentToParent();
                    return true;
                }
            }
        }
       
        return false;
    }


	public bool IsReparented(GameObject obj)
	{
		Slot s=null;
        Item itm = null;
		Transform sTrans=null;
       
        itm = obj.GetComponent<Item>();
      
		for(int i=0; i<slots.Count; i++)
		{
			s= slots[i].GetComponent<Slot>();
			sTrans = slots[i].GetComponent<Transform>();
            ////print(Vector2.Distance(sTrans.position, obj.transform.position) + " " + obj.transform.GetChild(0).GetComponent<Text>().text);
            ////print(Vector2.Distance(sTrans.position, obj.transform.position));
            ////print("slot=" + s.eColor.ToString() + "| item=" + itm.eColor.ToString());
            /*
             changes 11/18/2016
             * (s.empty && s.gameObject.activeInHierarchy)
             */
            if (!uniqueKey)
            {
                if ((s.empty && s.gameObject.activeInHierarchy) && (itm.eAlphabet == s.eAlphabet))// itm.eAlphabet == s.eAlphabet))
                {
                    // print(Vector2.Distance(sTrans.position, obj.transform.position) + " " + obj.transform.GetChild(0).GetComponent<Text>().text + " " + sTrans.GetComponent<Text>().text );
                    if (Vector2.Distance(sTrans.position, obj.transform.position) < checkDist)
                    {
                        obj.transform.SetParent(sTrans);
                        obj.transform.SetLocalXPos(0);
                        obj.transform.SetLocalYPos(0);
                        return true;
                    }
                }
            }
            else {
                if ((s.empty && s.gameObject.activeInHierarchy) && (itm.eColor == s.eColor))// itm.eAlphabet == s.eAlphabet))
                {
                    // print(Vector2.Distance(sTrans.position, obj.transform.position) + " " + obj.transform.GetChild(0).GetComponent<Text>().text + " " + sTrans.GetComponent<Text>().text );
                    if (Vector2.Distance(sTrans.position, obj.transform.position) < checkDist)
                    {
                        obj.transform.SetParent(sTrans);
                        obj.transform.SetLocalXPos(0);
                        obj.transform.SetLocalYPos(0);
                        return true;
                    }
                }
            }
          
            
		}
        return false;
	}

    public void InitSlotEvents()
    {
        Slot s = null;
       
        for (int i = 0; i < slots.Count; i++)
        {
            s = slots[i].GetComponent<Slot>();
            s.AddEvent();
        }
    }

    public void RemoveSlotEvents()
    {
        Slot s = null;

        for (int i = 0; i < slots.Count; i++)
        {
            s = slots[i].GetComponent<Slot>();
            s.RemoveEvent();
        }
    }
}
