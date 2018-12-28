using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TinaAndJun_Act6_Manager : MonoBehaviour {

    public static TinaAndJun_Act6_Manager ins;

    //public List<GameObject> lstColorItem;
    //public List<GameObject> lstColorSlot;
    public AudioClip[] audClip;
   
    [SerializeField]
    int point = 0, maxPts;
    AudioSource audSrc;
    public int Point
    {
        set { point = value; }
        get { return point; }
    }

    public void IncPoints()
    {
        point++;
    }

    public void IncPoints(Transform parent, Transform item)
    {
        Slot slot = parent.GetComponent<Slot>();
        Item itm = item.GetComponent<Item>();

        if (itm.eColor == slot.eColor)
        {
            point++;
            //parent.parent.GetComponent<Image>().enabled = false;
            parent.GetComponent<Image>().enabled = false;
            Destroy(item.gameObject);
            audSrc.clip = audClip[0];
            audSrc.Play();

        }

        GameOver();
    }

    void WrongInsert(Transform t)
    {
        audSrc.clip = audClip[1];
        audSrc.Play();
    }

    void Start()
    {
        ins = this;
        audSrc = GetComponent<AudioSource>();
        // StartCoroutine(IELate());
        Invoke("Late", 0.5f);

    }
    void Late()
    {

        // ColorObject.OnInsert += IncPoints;
        Item.OnDrop += GameOver;
        Item.OnInsert += IncPoints;
        Item.OnReturn += WrongInsert;
    }

    void GameOver()
    {
        if (point == maxPts)
        {
            ActivityDone.instance.Done();
            print("done");
        }
    }
}
