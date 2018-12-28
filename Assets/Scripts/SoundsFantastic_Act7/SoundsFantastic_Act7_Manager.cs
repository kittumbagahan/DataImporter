using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundsFantastic_Act7_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    //public AudioClip[] audClip;
    AudioSource audSrc;
    [SerializeField]
    int pts = 0, maxPts;

	void Start () {
        audSrc = GetComponent<AudioSource>();
        Item.OnInsert += Insert;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;

		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Insert(Transform parent, Transform item)
    {
        Shrink shrink = item.GetComponent<Shrink>();
        pts++;
        audSrc.PlayOneShot(clipFit);
        item.GetComponent<Item>().Locked = true;
        parent.GetComponent<Slot>().empty = true;
        shrink.Play();
        Destroy(item.gameObject, 1f);
        if (pts >= maxPts)
        {
            print("WIN!");
            Invoke("Done",1.5f);
            
        }
        print("test");
    }

    void BeginDrag(GameObject obj)
    {
        audSrc.PlayOneShot(clipDrag);
		ScoreManager.ins.IncNumOfMoves();
    }
    void WrongInsert(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
