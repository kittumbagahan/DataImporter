using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

    public EColor eColor = new EColor();
    public EAlphabet eAlphabet = new EAlphabet();
    public bool empty = true;
	void Start () {
        //InventoryManager.ins.slots.Add(this.gameObject);
        //InvokeRepeating("CheckSlot", 0f, 2f);   
	}
    void Update()
    {
        CheckSlot();
    }

    public void AddEvent()
    {
        //Item itm = null;
        //for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        //{
        //    itm = InventoryManager.ins.items[i].GetComponent<Item>();
        //    //itm.OnDrop += CheckSlot;

        //}
       // Item.OnDrop += CheckSlot;
        //CheckSlot();
    }

    public void RemoveEvent()
    {
        //Item itm = null;

        //for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        //{
        //    itm = InventoryManager.ins.items[i].GetComponent<Item>();
        //    //itm.OnDrop -= CheckSlot;
        //}
        //Item.OnDrop -= CheckSlot;
    }

    public void CheckSlot()
    {
        //print("CheckSlot");
        if (transform.childCount > 0)
        {
            empty = false;
        }
        else
        {
            empty = true;
        }
    }

   
}
