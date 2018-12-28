using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Mole : MonoBehaviour {

    private AudioSource audSrc;
    public bool interactive = true;
    //public bool up;
    public Direction dir;// = new Direction();
    public GameObject head;
   
    void Start () {
        //rect = GetComponent<RectTransform>();
        audSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Direction.up == dir)
        {
                transform.SetLocalYPos(transform.GetLocalYPos() + Mathf.Lerp(0, 2f, 0.2f));

        }
        else if (Direction.down == dir)
        {

            transform.SetLocalYPos(transform.GetLocalYPos() - Mathf.Lerp(0, 5f, 0.5f));

        }
        else { 
            //pause
        }
	}


    IEnumerator wait() {
        yield return new WaitForSeconds(2f);
        dir = Direction.down;
            head.GetComponent<Grow>().Stop();
        head.GetComponent<RectTransform>().SetWidth(100);
        head.GetComponent<RectTransform>().SetHeight(100);
     }

    public void Talk()
    {
        print("meow");
        audSrc.Play();
    }

    public void Click()
    {
          
        if(interactive)
        {
            
            //GetComponent<Grow>().Play();
			//ScoreManager.ins.IncNumOfMoves();
            head.GetComponent<Grow>().Play();
            audSrc.Play();
            dir = Direction.left;
            StartCoroutine(wait());
            interactive = false;
            ChatWithCat_Act4_Manager.ins.Points += 1;
            
        }
    }
}
