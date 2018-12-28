using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FavoriteBox_Act1_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag, clipSolved;
    public Sprite itemBG;
   // public AudioClip[] audClip;
    AudioSource audSrc;
    int slotIndex;
    [SerializeField]
    WordGameManager wordGameManager;

	void Start () {
        //WordGameManager.OnGenerate += DisableItemsImage;
        // WordGameManager.OnGenerate += ChageItemBG;
        WordGameManager.OnGenerate += ResizeItems;
        WordGameManager.OnGenerate += SetSlotsValue;
        WordGameManager.OnGenerate += DisableGroupLettersSlotImage;
       
        audSrc = GetComponent<AudioSource>();
        //wordGameManager.SetIndex = _SaveTest.Load(StoryBook.FAVORITE_BOX, Module.WORD);
		wordGameManager.SetIndex = SaveTest.Set;

		if(StoryBookSaveManager.ins.selectedBook == StoryBook.FAVORITE_BOX)
		{
			switch(wordGameManager.SetIndex)
			{
			case 0:
				ScoreManager.ins.maxMove = 5;
				break;
			case 3: 
				ScoreManager.ins.maxMove = 7;
				break;
			case 6: 
				ScoreManager.ins.maxMove = 6;
				break;
			case 9: 
				ScoreManager.ins.maxMove = 8;
				break;
			case 12: 
				ScoreManager.ins.maxMove = 8;
				break;

			default: 
				break;
			}
		}
		else if(StoryBookSaveManager.ins.selectedBook == StoryBook.WHAT_DID_YOU_SEE)
		{
			switch(wordGameManager.SetIndex)
			{
			case 0:
				ScoreManager.ins.maxMove = 6;
				break;
			case 3: 
				ScoreManager.ins.maxMove = 7;
				break;
			case 6: 
				ScoreManager.ins.maxMove = 5;
				break;
			case 9: 
				ScoreManager.ins.maxMove = 5;
				break;
			case 12: 
				ScoreManager.ins.maxMove = 5;
				break;

			default: 
				break;
			}
		}

		ScoreManager.ins.AW();
    }

    void DisableItemsImage()
    {
        Image img = null;
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            img = InventoryManager.ins.items[i].GetComponent<Image>();
            img.enabled = false;
        }
    }


    void ResizeItems()
    {
        RectTransform rect = null;
        Text txt = null;
        for(int i=0; i<InventoryManager.ins.items.Count; i++)
        {
            rect = InventoryManager.ins.items[i].GetComponent<RectTransform>();
            rect.SetHeight(70);
            rect.SetWidth(70);
            txt = InventoryManager.ins.items[i].transform.GetChild(0).GetComponent<Text>();
            txt.resizeTextMaxSize = 70;
        }
    }

    void DisableGroupLettersSlotImage()
    {
        for (int i = 0; i < WordGameManager.ins.groupLetters.transform.childCount; i++)
        {
            WordGameManager.ins.groupLetters.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }

    //void ChageItemBG()
    //{
    //    Image img = null;
    //    for (int i = 0; i < InventoryManager.ins.items.Count; i++)
    //    {
    //        img = InventoryManager.ins.items[i].GetComponent<Image>();
    //        img.sprite = itemBG;
    //    }
       
    //}

    void SetSlotsValue()
    {
        SlotStringValue slot = null;

        for(int i=0; i<WordGameManager.ins.groupClue.transform.childCount; i++)
        {
            slot = WordGameManager.ins.groupClue.transform.GetChild(i).GetComponent<SlotStringValue>();
            slot.strAlphabetValue = WordGameManager.ins.Word[i].ToString();
           // print(WordGameManager.ins.Word[i].ToString());
        }
    
        Item.OnInsert += Insert;
        Item.OnBeginDrag += BeginDrag;
        Item.OnReturn += WrongInsert;
    }

    void Insert(Transform parent, Transform item)
    {
        SlotStringValue slot = null;
        Text txt = null;
        slot = parent.GetComponent<SlotStringValue>();
        txt = item.GetChild(0).GetComponent<Text>();
        //print(txt.text + " " + slot.strAlphabetValue);
        if (txt.text != slot.strAlphabetValue)
        {
            item.GetComponent<Item>().Return();
            item.GetComponent<Item>().RecentParent = item.GetComponent<Item>().StartOrigin;
            parent.GetComponent<Slot>().CheckSlot();
            //audSrc.clip = audClip[1];
            //audSrc.Play();
           // audSrc.PlayOneShot(clipFit);
           
        }
        else {
            item.GetComponent<Item>().Locked = true;
            if (wordGameManager.WordSolved())
            {
                audSrc.PlayOneShot(clipSolved);
            }
            else {
                audSrc.PlayOneShot(clipFit); 
            }
            
        }
    }

    void WrongInsert(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
        print("wrong");
		ScoreManager.ins.IncNumOfMistakes();
    }

    void BeginDrag(GameObject obj)
    {
        audSrc.PlayOneShot(clipDrag);
		ScoreManager.ins.IncNumOfMoves();
    }
}
