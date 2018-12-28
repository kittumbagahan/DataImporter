using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ABCCircus_Act3_Manager : MonoBehaviour {


    public List<AudioClip> lstAudio;
    public List<Text> lstText;
    public Button[] btns;
    int index = -1;
    AudioSource audSrc;
    public Color32[] clr;
    public AudioSource audSrcBtn;
    public AudioClip[] audClipBtn;

    private bool chosen;
   // Button recentBtnClicked = null;
    [SerializeField]
    int pts = 0;
    void Start()
    {
        index++;
        ListOptions();
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.AW();
    }
    void ListOptions()
    {
        lstText.Shuffle();
        switch (index)
        {
            //set A
            case 0: ChangeTextsTo("A", "B", "C"); break;    //B
            case 1: ChangeTextsTo("C", "D", "E"); break;    //E
            case 2: ChangeTextsTo("F", "G", "H"); break;    //G
            case 3: ChangeTextsTo("I", "K", "L"); break;    // I
            case 4: ChangeTextsTo("M", "Z", "X"); break;    //Z
            //setA end

            //set B
            case 5: ChangeTextsTo("P", "Q", "R"); break; //Q
            case 6: ChangeTextsTo("F", "E", "A"); break; //F
            case 7: ChangeTextsTo("H", "A", "E"); break; //H
            case 8: ChangeTextsTo("C", "S", "L"); break; //S
            case 9: ChangeTextsTo("V", "B", "P"); break; //V
            //set B end

            //set C
            case 10: ChangeTextsTo("D", "B", "G"); break; //B
            case 11: ChangeTextsTo("H", "A", "J"); break; //J
            case 12: ChangeTextsTo("S", "K", "I"); break; //K
            case 13: ChangeTextsTo("J", "K", "I"); break; //I
            case 14: ChangeTextsTo("N", "M", "P"); break; //N
            //set C end


            default: break;
        };
      
    }

    void ChangeTextsTo(string opt1, string opt2, string opt3)
    {
        lstText[0].text = opt1;
        lstText[1].text = opt2;
        lstText[2].text = opt3;
    
    }

    public void Play(Button btnPlay)
    {
        try
        {
            audSrc.clip = lstAudio[index];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            print(ex.Message);
        }
        audSrc.Play();
        btnPlay.interactable = false;
        StartCoroutine(IEPlay(btnPlay));
    }

    public void Choose(GameObject btnOption)
    {
        Text txt = null;

        txt = btnOption.transform.GetChild(0).GetComponent<Text>();
        //recentBtnClicked = btnOption.GetComponent<Button>();
        //recentBtnClicked.interactable = false;
        CheckAnswer(txt);

		ScoreManager.ins.IncNumOfMoves();
    }

    void Correct()
    {
       
        index++;
        ListOptions();
       
    }

    public void Disable(Button btn)
    {
        btn.enabled = false;
    }

    IEnumerator IEPlay(Button btnPlay)
    {
        while (audSrc.isPlaying)
        {
            yield return new WaitForSeconds(0.05f);
        }
        btnPlay.interactable = true;

    }

    IEnumerator IECorrect(Text txt)
    {

        audSrcBtn.clip = audClipBtn[0];
        audSrcBtn.Play();
        print("Correct!");
        txt.color = clr[0];
        txt.transform.parent.GetComponent<Button>().interactable = false;
        pts++;
        if (pts == 3)
        {
            --pts; //prevent from onvoking two instance of activity done
            Invoke("Done", 1f);
            
            print("Game Over");
        }
        else
        {
            yield return new WaitForSeconds(1f);
            index++;
            ListOptions();
            btns[0].enabled = true;
            btns[1].enabled = true;
            btns[2].enabled = true;
            txt.color = clr[2];
            for (int i = 0; i < lstText.Count; i++)
            {
                FadeIn fadeIn = lstText[i].GetComponent<FadeIn>();
                fadeIn.Play();
            }
            txt.transform.parent.GetComponent<Button>().interactable = true;
            // recentBtnClicked.interactable = true;


        }
      
    }

    IEnumerator IEWrong(Text txt)
    {
        
        audSrcBtn.clip = audClipBtn[1];
        audSrcBtn.Play();
        print("WRONG");
        txt.color = clr[1];
        txt.transform.parent.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(1f);
        btns[0].enabled = true;
        btns[1].enabled = true;
        btns[2].enabled = true;
        
       // recentBtnClicked.interactable = true;
        txt.transform.parent.GetComponent<Button>().interactable = true;
        txt.color = clr[2];
       
    }

    void CheckAnswer(Text txtChosen)
    {
        String ans = txtChosen.text;
        switch (index)
        {
            //set A
            case 0:
                if (ans == "B")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 1:
                if (ans == "E")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 2:
                if (ans == "G")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 3:
                if (ans == "I")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 4:
                if (ans == "Z")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            //set B
            case 5:
                if (ans == "Q")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 6:
                if (ans == "F")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 7:
                if (ans == "H")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 8:
                if (ans == "S")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 9:
                if (ans == "V")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            //set C
            case 10:
                if (ans == "B")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 11:
                if (ans == "J")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 12:
                if (ans == "K")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 13:
                if (ans == "I")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            case 14:
                if (ans == "N")
                {
                    StartCoroutine(IECorrect(txtChosen));
                }
                else
                {
                    StartCoroutine(IEWrong(txtChosen));
                }
                break;
            default:
                break;
        }
        if(pts == 3)
        {
            Invoke("Done", 1f);
            print("Game Over"); 
        }
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
