using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundsFantastic_Act1_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipSwap, clipDrag;
    AudioSource audSrc;
    void Start()
    {
        InventoryManager.ins.eDragDir = EDragDirection.x;
        audSrc = GetComponent<AudioSource>();
        Item.OnDrop += Check;
        Item.OnBeginDrag += BeginDrag;
        Item.OnInsert += Swap;
        //Item.Lock();
        Invoke("UnlockItems", 3f);
		ScoreManager.ins.AW();
    }


    public void Swap(Transform parent, Transform dis)
    {
        audSrc.PlayOneShot(clipSwap);
    }

    public void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void Update()
    {

    }

    void Check()
    {
        int cnt = 0;
        Swap(null,null);
        if (InventoryManager.ins.slots[0].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("0"))
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
        //
        if (InventoryManager.ins.slots[5].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("5"))
        {
            cnt++;
        }
        if (InventoryManager.ins.slots[6].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("6"))
        {
            cnt++;
        }
        if (InventoryManager.ins.slots[7].transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("7"))
        {
            cnt++;
        }
        print(cnt);
        if (cnt == 8)
        {
            for (int i = 0; i < InventoryManager.ins.items.Count; i++ )
            {
                Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
                itm.Locked = true;
            }
            Invoke("Done",1f); 
            print("WIN!");
        }
    }

    void UnlockItems()
    {
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
            itm.Locked = false;
        }
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
