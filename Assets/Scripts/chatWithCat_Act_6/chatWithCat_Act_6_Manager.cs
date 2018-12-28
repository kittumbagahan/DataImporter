using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class chatWithCat_Act_6_Manager : MonoBehaviour {
    [SerializeField]
    AudioClip clipSolved, clipDrag;
    AudioSource audSrc;
    [SerializeField]
    Item[] itm;
    void Start()
    {
        InventoryManager.ins.eDragDir = EDragDirection.x;

        audSrc = GetComponent<AudioSource>();
        Item.OnDrop += Check;
        Item.OnBeginDrag += BeginDrag;
        for (int i = 0; i < itm.Length; i++ )
        {
            itm[i].Locked = true;
        }
        Invoke("UnlockItem", 5f);   
		ScoreManager.ins.AW();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UnlockItem()
    {
        for (int i = 0; i < itm.Length; i++)
        {
            itm[i].Locked = false;
        }
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void Check()
    {
        int cnt = 0;
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
        
        if (cnt == 4)
        {
            audSrc.PlayOneShot(clipSolved);
            Invoke("Done", 1f);
            print("WIN!");
           //audSrc.Play();
        }
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
