using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TinaAndJun_Act2_Manager : MonoBehaviour {

    public Sprite[] sprts;
    public Image imgObject;
    public GameObject objX;
    public Text objText;
    string tempTextVal;
    public Button[] btns;
    public AudioClip[] audClip;
    AudioSource audSrc;
    [SerializeField]
    int sprtIndex = 0, pts = 0;

    public void Choose(string keyWord)
    {
		ScoreManager.ins.IncNumOfMoves();
        string tempText = objText.text;
        if (keyWord.ToUpper() == "EE" && tempTextVal.Contains("EE"))
        {
            print("okay");
            sprtIndex++;
            pts++;
            btns[0].interactable = false;
            btns[1].enabled = false;
            Invoke("Generate", 1f);
            audSrc.clip = audClip[0];
            audSrc.Play();
            //Generate();
        }
        else if (keyWord.ToUpper() == "OO" && tempTextVal.Contains("OO"))
        {
            print("OOkay");
            pts++;
            sprtIndex++;
            btns[1].interactable = false;
            btns[0].enabled = false;
            Invoke("Generate", 1f);
            audSrc.clip = audClip[0];
            audSrc.Play();
           // Generate();
        }
        else
        {
            audSrc.clip = audClip[1];
            audSrc.Play();
            StartCoroutine(IEAppear(objX, 1.5f));
            btns[0].enabled = false; //interactable = false;
            btns[1].enabled = false; //.interactable = false;
            StartCoroutine(IEWrong(objText, tempText, 1.5f));
        }

        if (keyWord.Contains("OO"))
        {
            objText.text = objText.text.Replace("_", "O");
        }
        else if (keyWord.Contains("EE"))
        {
            objText.text = objText.text.Replace("_", "E");
        }

        
    }


	void Start () {
        //sprts.Shuffle();
        sprtIndex = SaveTest.Set;
        Generate();
        audSrc = GetComponent<AudioSource>();

		ScoreManager.ins.AW();
	}

    void Generate()
    {
        if (sprtIndex < sprts.Length && pts < 3)
        {
            imgObject.sprite = sprts[sprtIndex];
            objText.text = sprts[sprtIndex].name.ToUpper();
            tempTextVal = objText.text;
            if (objText.text.Contains("EE"))
            {
                objText.text = objText.text.Replace("EE", " _ _ ");
            }
            else if (objText.text.Contains("OO"))
            {
                objText.text = objText.text.Replace("OO", " _ _ "); print("oo");
            }
            btns[0].enabled = true;
            btns[1].enabled = true;
            btns[0].interactable = true;
            btns[1].interactable = true;
            Grow grow = imgObject.GetComponent<Grow>();
            Spin spin = imgObject.GetComponent<Spin>();
            spin.SpinIt();
            grow.Play();
        }
        else {
            ActivityDone.instance.Done();
            print("GAME OVER");
        }
       
        
    }



    IEnumerator IEAppear(GameObject obj, float time)
    {
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(true);
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    IEnumerator IEWrong(Text txt, string b, float time )
    {
        //print(a); print(b);
        
        yield return new WaitForSeconds(time);
        //a = b;        
        txt.text = b;
        btns[0].enabled = true;
        btns[1].enabled = true;
        btns[0].interactable = true;
        btns[1].interactable = true;
    }

   
    
   
}
