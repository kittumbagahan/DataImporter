using UnityEngine;
using System.Collections;

public class YummyShapes_act3_Manager : MatchingManager {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
   // public AudioClip[] audClip;
    AudioSource audSrc;
    [SerializeField]
    int pts = 0;

	void Start () {
        SetLen = 4;
        Index = SaveTest.Set;
        //GameOn();
        StartCoroutine(IENext());
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.AW();
	}

    public void GameOn()
    {

        for (int i = 0; i < slotContainer.childCount; i++ )
        {
            FadeIn fadeIn = slotContainer.GetChild(i).GetComponent<FadeIn>();
            fadeIn.Play();
        }
        if (Index < objItem.Length - 1)
        {

            SpawnSet();
            for (int i = 0; i < InventoryManager.ins.items.Count; i++)
            {
                Item itm = InventoryManager.ins.items[i].GetComponent<Item>();
                //itm.OnDrop += IncPts;
            }
        }
        else {
            //print("GAme OVER");
            //Invoke("Done", 1f);
        }
        Item.OnDrop -= IncPts;
        Item.OnDrop += IncPts;
        Item.OnInsert -= Insert;
        Item.OnInsert += Insert;
        Item.OnReturn -= OnReturn;
        Item.OnReturn += OnReturn;
        Item.OnBeginDrag -= BeginDrag;
        Item.OnBeginDrag += BeginDrag;
        
    }

    void IncPts()
    {
        audSrc.PlayOneShot(clipFit);
        pts++;
        if(pts >= MaxPts)
        {
            print("WIN!");
            Invoke("Done", 1f);
            //StartCoroutine(IENext());
        }
    }

    void OnReturn(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }

    void Insert(Transform parent, Transform item)
    {
        Item itm = item.GetComponent<Item>();
        itm.Locked = true;
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.AW();
        audSrc.PlayOneShot(clipDrag);
    }

    IEnumerator IENext()
    {
        yield return new WaitForSeconds(1f);
        DestroyItems();
        EmptySlots();
        pts = 0;
       
        yield return new WaitForSeconds(0.1f);
        GameOn();
        yield return new WaitForSeconds(0.1f);
        ShuffleItems();
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
