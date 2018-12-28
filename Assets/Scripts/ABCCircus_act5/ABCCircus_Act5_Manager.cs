using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ABCCircus_Act5_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipCorrect, clipWrong, clipDrag;

    public GameObject[] setObj;

    [SerializeField]
    int index = 0, pts;
    AudioSource audSrc;
	void Start () {
        audSrc = GetComponent<AudioSource>();
        Item.OnInsert += Insert;
        Item.OnReturn += Return;
        Item.OnBeginDrag += BeginDrag;
        index = SaveTest.Set;
        setObj[index].SetActive(true);
        setObj[index].GetComponent<FlyIn>().Fly();

		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.clip = clipDrag;
        audSrc.Play();
    }

    void Return(Transform t)
    {
        audSrc.clip = clipWrong;
        audSrc.Play();
    }

    void Insert(Transform parent, Transform dis)
    {
        index++;
        parent.GetComponent<Image>().enabled = false;
        dis.gameObject.SetActive(false);
        if (index <= setObj.Length)
        {
            pts++;
            audSrc.clip = clipCorrect;
            audSrc.Play();
            if (index < setObj.Length)
            {
               
                //parent.GetComponent<Image>().enabled = false;
                //dis.gameObject.SetActive(false);
                UI_SoundFX.ins.PlaySetFin();
                parent.parent.GetComponent<FlyOut>().Fly();
                setObj[index].SetActive(true);
            }
            
           
            if (pts < 3)
            {
                setObj[index].GetComponent<FlyIn>().Fly();
            }
            
        }
        else
        {
            //Invoke("Done",1f);
            //print("gameover");
        }

        if(pts >= 3){
            Invoke("Done", 1f);
        }
        
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
