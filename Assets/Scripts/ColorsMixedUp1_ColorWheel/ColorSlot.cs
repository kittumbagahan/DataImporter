using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorSlot : MonoBehaviour {

    //public EColor ecolor = new EColor();
   public Image img = null;
    Slot s = null;
    void Start () {

        if (transform.parent.GetComponent<Image>() != null)
        {
            img = transform.parent.GetComponent<Image>();
        }
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            //Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
            //itm.OnDrop += CheckSlot;
        }
        Item.OnDrop += CheckSlot;
    }

    public void CheckSlot()
    {
        Item itm = null;
        
        if (transform.childCount == 1)
        {
            itm = transform.GetChild(0).GetComponent<Item>();
            s = GetComponent<Slot>();
            if (itm.eColor == s.eColor)
            {
                img.enabled = false;
            }
        }
    }
	
}
