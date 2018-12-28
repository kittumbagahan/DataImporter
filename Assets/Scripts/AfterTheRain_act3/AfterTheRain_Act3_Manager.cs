using UnityEngine;
using System.Collections;

public class AfterTheRain_Act3_Manager : MatchingManager {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    //public AudioClip[] audClip;
    AudioSource audSrc;
    [SerializeField]
    int pts = 0;

   
    void Start()
    {
        audSrc = GetComponent<AudioSource>();
        SetLen = 4;
        
        //setting index is 0 and 4 only
        Index = SaveTest.Set;
        
        StartCoroutine(IENext());
		ScoreManager.ins.AW();
    }

    public void GameOn()
    {

        SpawnSet();
        slotContainer.gameObject.SetActive(true);
        Item.OnInsert += Insert;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;
    }

    void Insert(Transform parent, Transform item)
    {
        pts++;
        audSrc.PlayOneShot(clipFit);
        item.GetComponent<Item>().Locked = true;
        if (pts >= MaxPts)
        {
            print("WIN!");
            Invoke("Done",1f);
            //StartCoroutine(IENext());
        }
       
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }
    void WrongInsert(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }

    IEnumerator IENext()
    {
        yield return new WaitForSeconds(0.1f);
        Item.OnInsert -= Insert;
        Item.OnReturn -= WrongInsert;
        DestroyItems();
        EmptySlots();
        pts = 0;

        yield return new WaitForSeconds(0.1f);
        GameOn();
        yield return new WaitForSeconds(0.1f);
        ShuffleItems();
    }
}
