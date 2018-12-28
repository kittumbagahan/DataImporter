using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class JoeyGoSchool_Act5_Manager : MonoBehaviour {

   // public AudioClip[] audClip;
    [SerializeField]
    AudioClip clipFit, clipSolved, clipWrong, clipDrag;
    public Sprite[] setA;
    public Sprite[] setB;
    public Sprite[] setC;
    public Sprite[] setD;
    public Sprite[] setE;


    [SerializeField]
    int index;
    //[SerializeField]
    AudioSource audSrc;
    Vector3 recentRotation;
    [SerializeField]
    int pts;
    void Start()
    {
        Item.OnInsert += OnInsert;
        Item.OnBeginDrag += OnBeginDrag;
        Item.OnReturn += OnReturn;
        Shrink.OnShrinkEnd += EnableItems;
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            Item itm = InventoryManager.ins.items[i].GetComponentInChildren<Item>();
            itm.Locked = true;
        }
        audSrc = GetComponent<AudioSource>();
        Generate();

		ScoreManager.ins.AW();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
    void Generate()
    {
        int slotIndex = 0;
        int itmIndex = 0;
        slotIndex = Random.Range(0, 4);
        switch (index)
        {
            case 0:
                //pick piece  to solve
                for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
                {
                    InventoryManager.ins.slots[i].GetComponent<Image>().sprite = setA[i];
                }
                for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
                {
                    if (slotIndex != i)
                        InventoryManager.ins.slots[i].GetComponent<FadeIn>().enabled = true;
                }
                InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().enabled = true;
                InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().eColor = EColor.red;
                InventoryManager.ins.slots[slotIndex].GetComponent<Image>().enabled = false;
               
                //set a ramd om item key to picked piece key
                itmIndex = Random.Range(0, 3);
                InventoryManager.ins.items[itmIndex].GetComponent<Item>().eColor = EColor.red;
                InventoryManager.ins.items[itmIndex].GetComponent<Image>().sprite = InventoryManager.ins.slots[slotIndex].GetComponent<Image>().sprite;
                
                //random items
                for (int i = 0; i < 3; i++ )
                {
                    
                   if(i == 0 && i != itmIndex)
                   {
                         InventoryManager.ins.items[i].GetComponent<Image>().sprite = setB[Random.Range(0, 4)];
                   }
                   else if (i == 1 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setC[Random.Range(0, 4)];
                   }
                   else if (i == 2 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setD[Random.Range(0, 4)];
                   }
                }
             break;
            case 1:
                 for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
                 {
                     InventoryManager.ins.slots[i].GetComponent<Image>().sprite = setB[i];
                   
                 }
                 InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().enabled = true;
                InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().eColor = EColor.red;
                InventoryManager.ins.slots[slotIndex].GetComponent<Image>().enabled = false;
                itmIndex = Random.Range(0, 3);
                InventoryManager.ins.items[itmIndex].GetComponent<Item>().eColor = EColor.red;
                InventoryManager.ins.items[itmIndex].GetComponent<Image>().sprite = InventoryManager.ins.slots[slotIndex].GetComponent<Image>().sprite;
                for (int i = 0; i < 3; i++ )
                {
                   if(i == 0 && i != itmIndex)
                   {
                         InventoryManager.ins.items[i].GetComponent<Image>().sprite = setA[Random.Range(0, 4)];
                   }
                   else if (i == 1 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setC[Random.Range(0, 4)];
                   }
                   else if (i == 2 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setD[Random.Range(0, 4)];
                   }
                }
                break;

            case 2:
                for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
                {
                    InventoryManager.ins.slots[i].GetComponent<Image>().sprite = setC[i];
                }
                 InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().enabled = true;
                InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().eColor = EColor.red;
                InventoryManager.ins.slots[slotIndex].GetComponent<Image>().enabled = false;
                itmIndex = Random.Range(0, 3);
                InventoryManager.ins.items[itmIndex].GetComponent<Item>().eColor = EColor.red;
                InventoryManager.ins.items[itmIndex].GetComponent<Image>().sprite = InventoryManager.ins.slots[slotIndex].GetComponent<Image>().sprite;
                for (int i = 0; i < 3; i++ )
                {
                   if(i == 0 && i != itmIndex)
                   {
                         InventoryManager.ins.items[i].GetComponent<Image>().sprite = setB[Random.Range(0, 4)];
                   }
                   else if (i == 1 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setA[Random.Range(0, 4)];
                   }
                   else if (i == 2 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setD[Random.Range(0, 4)];
                   }
                }
                break;

            case 3: 
                for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
                {
                    InventoryManager.ins.slots[i].GetComponent<Image>().sprite = setD[i];
                }
                 InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().enabled = true;
                InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().eColor = EColor.red;
                InventoryManager.ins.slots[slotIndex].GetComponent<Image>().enabled = false;
                itmIndex = Random.Range(0, 3);
                InventoryManager.ins.items[itmIndex].GetComponent<Item>().eColor = EColor.red;
                InventoryManager.ins.items[itmIndex].GetComponent<Image>().sprite = InventoryManager.ins.slots[slotIndex].GetComponent<Image>().sprite;
                for (int i = 0; i < 3; i++ )
                {
                   if(i == 0 && i != itmIndex)
                   {
                         InventoryManager.ins.items[i].GetComponent<Image>().sprite = setB[Random.Range(0, 4)];
                   }
                   else if (i == 1 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setC[Random.Range(0, 4)];
                   }
                   else if (i == 2 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setE[Random.Range(0, 4)];
                   }
                }
                break;
            case 4:
                for (int i = 0; i < InventoryManager.ins.slots.Count; i++)
                {
                    InventoryManager.ins.slots[i].GetComponent<Image>().sprite = setE[i];
                }
                 InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().enabled = true;
                InventoryManager.ins.slots[slotIndex].GetComponent<Slot>().eColor = EColor.red;
                InventoryManager.ins.slots[slotIndex].GetComponent<Image>().enabled = false;
                itmIndex = Random.Range(0, 3);
                InventoryManager.ins.items[itmIndex].GetComponent<Item>().eColor = EColor.red;
                InventoryManager.ins.items[itmIndex].GetComponent<Image>().sprite = InventoryManager.ins.slots[slotIndex].GetComponent<Image>().sprite;
                for (int i = 0; i < 3; i++ )
                {
                   if(i == 0 && i != itmIndex)
                   {
                         InventoryManager.ins.items[i].GetComponent<Image>().sprite = setA[Random.Range(0, 4)];
                   }
                   else if (i == 1 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setC[Random.Range(0, 4)];
                   }
                   else if (i == 2 && i != itmIndex)
                   {
                       InventoryManager.ins.items[i].GetComponent<Image>().sprite = setD[Random.Range(0, 4)];
                   }
                }
                break;

            default: break;

        }
    }

    void Set() {
        
    }

    void OnInsert(Transform parent, Transform dis)
    {
        dis.rotation = parent.rotation;
        dis.GetComponent<RectTransform>().SetWidth(parent.GetComponent<RectTransform>().GetWidth());
        dis.GetComponent<RectTransform>().SetHeight(parent.GetComponent<RectTransform>().GetHeight());
        pts++;

        if (pts == 1)
        {
            Invoke("Done",0.5f);
            //audSrc.PlayOneShot(clipSolved);
            UI_SoundFX.ins.Play(clipSolved);
        }
        else
        {
            //audSrc.clip = audClip[0];
            audSrc.PlayOneShot(clipFit);
        }
    }

    void OnBeginDrag(GameObject obj)
    {
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        audSrc.PlayOneShot(clipDrag);

		ScoreManager.ins.IncNumOfMoves();
    }


    void OnReturn(Transform dis)
    {
        dis.GetComponent<RectTransform>().SetWidth(180f);
        dis.GetComponent<RectTransform>().SetHeight(141f);
        dis.rotation = dis.parent.rotation;//Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        audSrc.PlayOneShot(clipWrong);
    }

    void EnableItems(GameObject obj)
    {
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            InventoryManager.ins.items[i].GetComponentInChildren<Item>().Locked = false;
            //InventoryManager.ins.items[i].GetComponentInChildren<Image>().color = Color.white;

        }
        Shrink.OnShrinkEnd -= EnableItems;
    }

    void Done()
    {
        ActivityDone.instance.Done();
        print("done");
    }
}
