using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ABCCircus_act6_Manager : MonoBehaviour {
    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    public GameObject txtDonePuzzle;
    public Sprite[] setTextDoneSprite;
    public Sprite[] setDoneSprite;
    public Sprite[] setGraySprite;
    public GameObject[] setItems;
    public AudioClip[] txtAudClip;
    [SerializeField]
    GameObject slotContainer;
    [SerializeField]
    Image tvBG;
    [SerializeField]
    int index=0, pts=0;
    AudioSource audSrc;
	void Start () {
        Item.OnInsert += Insert;
        Item.OnReturn += Return;
        Item.OnBeginDrag += BeginDrag;
        tvBG.sprite = setGraySprite[index];
        setItems[index].SetActive(true);
        audSrc = GetComponent<AudioSource>();

		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void Insert(Transform paret, Transform dis)
    {
        pts++;
        if (pts == 4)
        {

            tvBG.sprite = setDoneSprite[index];
            slotContainer.SetActive(false);
            txtDonePuzzle.GetComponent<AudioSource>().clip = txtAudClip[index];
            txtDonePuzzle.SetActive(true);
            txtDonePuzzle.GetComponent<Grow>().Play();
            txtDonePuzzle.GetComponent<Shrink>().Play();
            txtDonePuzzle.GetComponent<Image>().sprite = setTextDoneSprite[index];
           
            Invoke("Done", 3f);
        }
        else
        {
            dis.GetComponent<Item>().Locked = true;
            audSrc.PlayOneShot(clipDrag);
            //Destroy(dis.gameObject);
        }
    }

    void Return(Transform itm)
    {
        Item _itm = itm.GetComponent<Item>();
        if (_itm.Locked == false)
        {

            itm.GetComponent<RectTransform>().SetWidth(_itm.tempWidth);
            itm.GetComponent<RectTransform>().SetHeight(_itm.tempHeight);

            audSrc.PlayOneShot(clipWrong);
        }
    }
    void Done()
    {
        ActivityDone.instance.Done();
    }
}
