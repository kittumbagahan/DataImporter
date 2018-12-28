using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChatWithCat_Act4_Manager : MonoBehaviour {
    public static ChatWithCat_Act4_Manager ins;
    public Transform[] spot;
    public GameObject mole;
    public GameObject tail;

    
    [SerializeField]
    private int pts = 0 ;
    [SerializeField]
    GameObject[] objHeadPts;
    //[SerializeField]
    public Transform ptsParentOnShow;
	void Start () {
        ins = this;
        StartCoroutine("WhackAMole");
		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int Points {
        set { 
            pts = value;
            for (int i = 0; i < objHeadPts.Length; i++)
            {
                if (objHeadPts[i].GetComponent<Image>().color == Color.black)
                {
                    objHeadPts[i].transform.SetParent(ptsParentOnShow);
                    objHeadPts[i].GetComponent<Image>().color = Color.white;
                    objHeadPts[i].GetComponent<Grow>().Play();
                    objHeadPts[i].GetComponent<Shrink>().Play();
                    break;
                }
            }
            if(pts == 5)
            {
                StopCoroutine("WhackAMole");
                //mole.SetActive(false);
                //tail.SetActive(false);
                Invoke("Done",1f);
              
            }
        }
         get { return pts; }
    }

    public GameObject[] ObjHeadPts {
        set { objHeadPts = value; }
        get { return objHeadPts; }
    }

    void ShowUp(Transform location, GameObject obj)
    {
        obj.transform.SetParent(location);
        location.GetComponent<Hole>().ResizeMole();
        obj.transform.SetLocalXPos(0);
        obj.transform.SetLocalYPos(-2f);
        obj.SetActive(true);

    }

    //random time to show tail
    //random time to shoup
    IEnumerator WhackAMole()
    { 
        while(true)
        {
            Transform loc = spot[Random.Range(0, spot.Length)];
          
            ShowUp(loc,tail);
            tail.GetComponent<Tail>().up = true;
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            tail.GetComponent<Tail>().up = false;
            
            if(Random.Range(0,101) < 50)
            {
                yield return new WaitForSeconds(0.5f);
				ScoreManager.ins.IncNumOfMoves();
                ShowUp(loc, mole);
                //objMole.GetComponent<Mole>().up = true;
                mole.GetComponent<Mole>().dir = Direction.up;
                tail.SetActive(false);
                yield return new WaitForSeconds(Random.Range(1f, 1.5f));
                if(mole.GetComponent<Mole>().interactive == true)
                {
                    mole.GetComponent<Mole>().dir = Direction.down;
                
                }
            }
            
            yield return new WaitForSeconds(2.5f);
            mole.GetComponent<Mole>().interactive = true;
        }
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
