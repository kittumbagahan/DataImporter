using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AfterTheRain_Act1_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    public ETiltDirection eTiltDir;
    public float maxTilt = 20;
    public Sprite sprtSlotXitemBG;
 

    AudioSource audSrc;
    Image _img = null;
    Color _clr = Color.white;
    Transform _transSlot = null;
   
    void Start () {

        WordGameManager.OnGenerate += ResizeItems;
        WordGameManager.OnGenerate += TiltSlots;
        WordGameManager.OnGenerate += SetSlotsValue;
       // WordGameManager.OnGenerate += ChangeClueSlotsSprite;
        WordGameManager.OnGenerate += DisableGroupLettersSlotImage;
        WordGameManager.ins.SetIndex = SaveTest.Set;
        audSrc = GetComponent<AudioSource>();
		switch(WordGameManager.ins.SetIndex)
		{
		case 0: 
			ScoreManager.ins.maxMove  = 6;
			break;
		case 3: 
			ScoreManager.ins.maxMove  = 8;
			break;
		case 6: 
			ScoreManager.ins.maxMove  = 7;
			break;
		case 9: 
			ScoreManager.ins.maxMove  = 7;
			break;
		case 12: 
			ScoreManager.ins.maxMove  = 5;
			break;
		}
		ScoreManager.ins.AW();
    }


    void TiltSlots()
    {
        //delay the wordgamemanager to make this work on Start()
        for (int i=0; i<InventoryManager.ins.slots.Count; i++)
        {
            RandomTilt(InventoryManager.ins.slots[i].transform);
        }
    }

    void RandomTilt(Transform t)
    {
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
             
                t.SetLocalZRot(Random.Range(0,20f));
                break;
            case 1:
                
                t.SetLocalZRot(Random.Range(340,360));
                break;
            default: break;
        }
    }

    void ResizeItems()
    {
        RectTransform rect = null;
        Text txt = null;
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            rect = InventoryManager.ins.items[i].GetComponent<RectTransform>();
            rect.SetHeight(90);
            rect.SetWidth(90);
            txt = InventoryManager.ins.items[i].transform.GetChild(0).GetComponent<Text>();
            txt.resizeTextMaxSize = 90;
        }
    }

    void SetSlotsValue()
    {
        SlotStringValue slot = null;

        for (int i = 0; i < WordGameManager.ins.groupClue.transform.childCount; i++)
        {
            slot = WordGameManager.ins.groupClue.transform.GetChild(i).GetComponent<SlotStringValue>();
            slot.strAlphabetValue = WordGameManager.ins.Word[i].ToString();
            // print(WordGameManager.ins.Word[i].ToString());
        }

        Item.OnInsert += Insert;
        Item.OnReturn += Return;
        Item.OnBeginDrag += BeginDrag;
    }

    void Insert(Transform parent, Transform item)
    {
        SlotStringValue slot = null;
        Text txt = null;
        slot = parent.GetComponent<SlotStringValue>();
        txt = item.GetChild(0).GetComponent<Text>();
       // print(txt.text + " " + slot.strAlphabetValue);
        if (txt.text != slot.strAlphabetValue)
        {
            item.GetComponent<Item>().Return();
            item.GetComponent<Item>().RecentParent = item.GetComponent<Item>().StartOrigin;
            parent.GetComponent<Slot>().CheckSlot();
            item.SetLocalZRot(item.GetComponent<Item>().StartOrigin.GetLocalZRot());
            //audSrc.PlayOneShot(clipWrong);
        }
        else
        {
          
            audSrc.PlayOneShot(clipFit);
            // print("wow");
        }
    }

    void Return(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void DisableGroupLettersSlotImage()
    {
        for (int i = 0; i < WordGameManager.ins.groupLetters.transform.childCount; i++)
        {
            WordGameManager.ins.groupLetters.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }
    /*  SPRITE CHANGERS
     * 
    void ChangeClueSlotsSprite()
    {
        
        _clr.a = 0.5f;
        for (int i=0; i<WordGameManager.ins.groupClue.transform.childCount; i++)
        {
            _img = WordGameManager.ins.groupClue.transform.GetChild(i).GetComponent<Image>();
            _img.sprite = sprtSlotXitemBG;
            _img.color = _clr;
        }
        ChangeSlotsItemSprite(WordGameManager.ins.groupClue.transform);
        ChangeSlotsItemSprite(WordGameManager.ins.groupLetters.transform);
    }

   

    void ChangeSlotsItemSprite(Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            _transSlot = container.GetChild(i);
            //print(_transSlot.childCount);
            if(_transSlot.childCount == 1)
            {
                _img = _transSlot.GetChild(0).GetComponent<Image>();
                _img.sprite = sprtSlotXitemBG;
            }
        }
    }
    */
}
