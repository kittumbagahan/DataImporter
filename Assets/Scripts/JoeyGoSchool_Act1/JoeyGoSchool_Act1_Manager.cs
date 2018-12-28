using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class JoeyGoSchool_Act1_Manager : MonoBehaviour {

	[SerializeField]
	Button btnListen;
    public List<AudioClip> lstAudio;
    public List<Text> lstText;
    [SerializeField]
    int index = -1, recentIndex = 10;
    AudioSource audSrc;
    public Sprite[] sprtClicked;
    SpriteState tempSpriteState; //remove button pressed sprite to make this visually working

    public AudioClip[] audClip;
    public AudioSource audSrcBtns;

    [SerializeField]
    Button[] btns = new Button[4];
    Button recentBtnClicked = null;
    int pts;
    void Start()
    {
        //index++;
        index = SaveTest.Set;
       // ListOptions();
        audSrc = GetComponent<AudioSource>();
        for (int i = 0; i < lstText.Count; i++){
            btns[i] = lstText[i].transform.parent.GetComponent<Button>();
			btns[i].gameObject.SetActive(false);
        }
		ScoreManager.ins.AW();
    }

    // Update is called once per frame
    void ListOptions()
    {
		FlyIn flyIn;
		if(index != recentIndex)
		{
			for (int i = 0; i < btns.Length; i++ )
			{
				btns[i].gameObject.SetActive(true);
				flyIn = btns[i].GetComponent<FlyIn>();
				flyIn.enabled = true;
				flyIn.Fly();
				btns[i].gameObject.SetActive(true);
			}
			//btnListen.interactable = true;
			lstText.Shuffle();
			switch (index)
			{
			//set A
			case 0: ChangeTextsTo("Carlo", "Joey", "Teacher", "Dad"); break;
			case 1: ChangeTextsTo("Scared", "Happy", "Excited", "Sad"); break;
			case 2: ChangeTextsTo("Aquarium", "Gate", "Building", "School"); break;
				//
			case 3: ChangeTextsTo("Aquarium", "School", "Planet", "Home"); break;
			case 4: ChangeTextsTo("School of Fish", "Octopus", "Snails", "Jellyfish"); break;
			case 5: ChangeTextsTo("Banana", "Newt", "Fish", "Frog"); break; 
				//
			case 6: ChangeTextsTo("Net", "House", "School", "Gate"); break; 
			case 7: ChangeTextsTo("He smiled", "He cried", "He laughed", "He danced"); break; 
			case 8: ChangeTextsTo("Mom", "Dad", "Teacher", "Carlo"); break; 
				//
			case 9: ChangeTextsTo("Teacher", "Mom", "Friends", "Dad"); break; 
			case 10: ChangeTextsTo("Jaime", "Peter", "Carlo", "Teacher"); break;
			case 11: ChangeTextsTo("Antlers", "Fins", "A trunk", "A tail"); break;
				//
			case 12: ChangeTextsTo("Sad", "Happy", "Angry", "Afraid"); break;
			case 13: ChangeTextsTo("Mom", "Dad", "Friends", "Carlo"); break;
			case 14: ChangeTextsTo("Carlo", "Joey", "Jaime", "Peter"); break;

			default: break;
			};

			recentIndex = index;
		}
        
        
    }


    void ChangeTextsTo(string opt1, string opt2, string opt3, string opt4)
    {
        lstText[0].text = opt1;
        lstText[1].text = opt2;
        lstText[2].text = opt3;
        lstText[3].text = opt4;
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
		ListOptions();
    }

    public void Choose(GameObject btnOption)
    {
        Text txt = null;

        txt = btnOption.transform.GetChild(0).GetComponent<Text>();
        recentBtnClicked = btnOption.GetComponent<Button>();
        recentBtnClicked.interactable = false;
        //disable other buttons for the meantime to avoid clicking so many button at time
        for (int i = 0; i < btns.Length; i++){
            if (btns[i] != recentBtnClicked){
                btns[i].enabled = false;
            }
        }
        CheckAnswer(txt.text);
        audSrc.Stop();

		ScoreManager.ins.IncNumOfMoves();
    }

    void Correct()
    {
        index++;
        pts++;
        if (pts < 3)
        {
           // ListOptions();
    
        }
        else {
            ActivityDone.instance.Done();
        }
   }

	IEnumerator IENext(float time)
	{
		yield return new WaitForSeconds(time);
		Play(btnListen);
	}

    IEnumerator IEPlay(Button btnPlay)
    {
        while (audSrc.isPlaying)
        {
            yield return new WaitForSeconds(0.05f);
			//btnPlay.interactable = false;
        }
        btnPlay.interactable = true;

    }

    IEnumerator IECorrect()
    {
        print("Correct!");
        tempSpriteState.disabledSprite = sprtClicked[0];
        recentBtnClicked.spriteState = tempSpriteState;
        audSrcBtns.clip = audClip[0];
        audSrcBtns.Play();
		btnListen.interactable = false;
        yield return new WaitForSeconds(1f);
		for(int i=0; i < btns.Length; i++)
		{
			btns[i].gameObject.SetActive(false);
		}
		//btnListen.interactable = true;

        pts++;
        if (pts < 3)
        {
            index++;
            //ListOptions();
            recentBtnClicked.interactable = true;
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].enabled = true;
            }
			StartCoroutine(IENext(1f));
		
        }
        else
        {
            Invoke("Done",0.5f);
        }
       
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }

    IEnumerator IEWrong()
    {
        print("WRONG");
        tempSpriteState.disabledSprite = sprtClicked[1];
        recentBtnClicked.spriteState = tempSpriteState;
        audSrcBtns.clip = audClip[1];
        audSrcBtns.Play();
        yield return new WaitForSeconds(1f);
        recentBtnClicked.interactable = true;
        for (int i = 0; i < btns.Length; i++) {
            btns[i].enabled = true;
        }
    }



    void CheckAnswer(string ans)
    {
        switch (index)
        {
            //set A
            case 0:
                if (ans == "Joey")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 1:
                if (ans == "Scared")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 2:
                if (ans == "School")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 3:
                if (ans == "Aquarium")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 4:
                if (ans == "School of Fish")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            //set B
            case 5:
                if (ans == "Fish")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 6:
                if (ans == "Gate")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 7:
                if (ans == "He cried")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 8:
                if (ans == "Mom")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 9:
                if (ans == "Friends")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            //set C
            case 10:
                if (ans == "Teacher")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 11:
                if (ans == "Fins")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 12:
                if (ans == "Happy")
                {
                    StartCoroutine(IECorrect());
                 //   ActivityDone.instance.Done();
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 13:
                if (ans == "Mom")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            case 14:
                if (ans == "Joey")
                {
                    StartCoroutine(IECorrect());
                }
                else
                {
                    StartCoroutine(IEWrong());
                }
                break;
            default:
             
                print("Game Over"); break;
        }
    }
}
