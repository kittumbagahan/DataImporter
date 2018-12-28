using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ABCCircus_Act1_Manager : MonoBehaviour {

    public GameObject[] options;
    public Text[] txtLetter;
    public Text txtBig;
    public GameObject xMark;
    string[] strRecent = { "","","","" };
    public Color32[] clr;

    //AudioSource audSrc;
    [SerializeField]
    int pts = 0;
    public char[] uniqueLetter;
    [SerializeField]
    int index = 0;
    [SerializeField]
    AudioClip correctClip, wrongClip;
	void Start () {
        index = SaveTest.Set;
       // audSrc = GetComponent<AudioSource>();
        Generate();

		ScoreManager.ins.AW();
	}
	void Generate()
    {
        txtLetter.Shuffle();
        clr.Shuffle();
        //txtBig.text = MonoExtension.RandomLetter();
        txtBig.text = uniqueLetter[index].ToString().ToUpper();
        txtBig.GetComponent<FlyIn>().Fly();
        txtLetter[0].text = txtBig.text.ToLower();
        txtLetter[0].color = clr[0];
        strRecent[0] = txtLetter[0].text;
        RandomText(txtLetter[1], txtBig.text, strRecent);
        strRecent[1] = txtLetter[1].text;
        txtLetter[1].color = clr[1];
        RandomText(txtLetter[2], txtBig.text, strRecent);
        strRecent[2] = txtLetter[2].text;
        txtLetter[2].color = clr[2];
        RandomText(txtLetter[3], txtBig.text, strRecent);
        strRecent[3] = txtLetter[3].text;
        txtLetter[3].color = clr[3];

        for (int i = 0; i < options.Length; i++ )
        {
            Grow grow = options[i].GetComponent<Grow>();
            Button btn = options[i].GetComponent<Button>();
            btn.interactable = true;
            grow.Play();
        }
    }

    void RandomText(Text txt, string strUnique, string[] recent)
    {
        txt.text = strUnique;
        while(txt.text == strUnique)
        {
            txt.text = MonoExtension.RandomLetter().ToLower();
            for (int i=0; i<recent.Length; i++) {
                if (txt.text == recent[i])
                {
                    RandomText(txt, strUnique, recent);
                }
            }
        }
       
    }

    public void Choose(Text txt)
    {
        xMark.SetActive(false);
		ScoreManager.ins.IncNumOfMoves();
        if (txt.text.ToUpper() == txtBig.text.ToUpper())
        {
           
            UI_SoundFX.ins.Play(correctClip);
            print("CORRECT!");
            pts++;
            index++;
            txt.transform.parent.GetComponent<Button>().interactable = false;
            if (pts >= 3){
                Invoke("Done",1f);
            }
            else {
                //Generate();
                Invoke("Generate", 1f);
            }
            
        }
        else {
            UI_SoundFX.ins.Play(wrongClip);
            xMark.SetActive(true);
            xMark.transform.SetXPos(txt.transform.parent.GetXPos());
            xMark.transform.SetYPos(txt.transform.parent.GetYPos());
            print("WRONG");
            Invoke("Disable", 1f);
        }
    }

    void Disable()
    {
        xMark.SetActive(false);
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
