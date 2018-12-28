using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ChatMyCat_Act2_Manager : MonoBehaviour {

    public Button[] UIBtns;
    public string[] words;
    public Sprite[] wordSprites;
    public Text txtToFillIn;
    public Text txtMissing;
    public Image wordImg;
    [SerializeField]
    Sprite sprtWrongChoice,sprtRightChoice, sprtDefault;
    
    
    int wordIndex = 0;
    char answer;
    public AudioClip[] audClip;
    AudioSource audSrc;
    [SerializeField]
    int pts;
	void Start () {
        //words.Shuffle();
        wordIndex = SaveTest.Set;
        GameOn();
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.AW();
    }
	
    void GameOn()
    {
        Text txt = null;
        string wordToGuess = words[wordIndex];
        string usedLetters = "";
        SelectSprite(wordIndex);
        txtMissing.text = "";
        txtToFillIn.text = "_";
        txtToFillIn.text += wordToGuess.Substring(1, wordToGuess.Length-1);
        answer = wordToGuess[0];
      
        UIBtns.Shuffle();
        txt = UIBtns[0].GetComponent<Transform>().GetChild(0).GetComponent<Text>();
        txt.text = answer.ToString();
        usedLetters += txt.text;
        //--random
        txt = UIBtns[1].GetComponent<Transform>().GetChild(0).GetComponent<Text>();
        txt.text = MonoExtension.RandomLetter(usedLetters);
        usedLetters += txt.text;
        txt = UIBtns[2].GetComponent<Transform>().GetChild(0).GetComponent<Text>();
        txt.text = MonoExtension.RandomLetter(usedLetters);
        usedLetters += txt.text;
        txt = UIBtns[3].GetComponent<Transform>().GetChild(0).GetComponent<Text>();
        txt.text = MonoExtension.RandomLetter(usedLetters);

        UIBtns[0].interactable = true;
        UIBtns[1].interactable = true;
        UIBtns[2].interactable = true;
        UIBtns[3].interactable = true;
    }

    void SelectSprite(int index)
    {
        switch (index)
        {
            case 0:
                wordImg.sprite = wordSprites[0]; break;
            case 1:
                wordImg.sprite = wordSprites[1]; break;
            case 2:
                wordImg.sprite = wordSprites[2]; break;
            case 3:
                wordImg.sprite = wordSprites[3]; break;
            case 4:
                wordImg.sprite = wordSprites[4]; break;
            case 5:
                wordImg.sprite = wordSprites[5]; break;
            case 6:
                wordImg.sprite = wordSprites[6]; break;
            case 7:
                wordImg.sprite = wordSprites[7]; break;
            case 8:
                wordImg.sprite = wordSprites[8]; break;
            case 9:
                wordImg.sprite = wordSprites[9]; break;
            case 10:
                wordImg.sprite = wordSprites[10]; break;
            case 11:
                wordImg.sprite = wordSprites[11]; break;
            case 12:
                wordImg.sprite = wordSprites[12]; break;
            case 13:
                wordImg.sprite = wordSprites[13]; break;
            case 14:
                wordImg.sprite = wordSprites[14]; break;

            default: break;
        }

        FadeIn fadeIn = wordImg.GetComponent<FadeIn>();
        fadeIn.Play();
    }

    //void SelectSprite(string word)
    //{
    //    switch (word)
    //    {
    //        case "Bat":
    //            wordImg.sprite = wordSprites[0]; break;
    //        case "Bat_":
    //            wordImg.sprite = wordSprites[1]; break;
    //        case "Cat":
    //            wordImg.sprite = wordSprites[2]; break;
    //        case "Chat":
    //            wordImg.sprite = wordSprites[3]; break;
    //        case "Fat":
    //            wordImg.sprite = wordSprites[4]; break;
    //        case "Hat":
    //            wordImg.sprite = wordSprites[5]; break;
    //        case  "Mat":
    //            wordImg.sprite = wordSprites[6]; break;
    //        case "Ngat":
    //            wordImg.sprite = wordSprites[7]; break;
    //        case "Pat":
    //            wordImg.sprite = wordSprites[8]; break;
    //        case "Rat":
    //            wordImg.sprite = wordSprites[9]; break;
    //        case "Sat":
    //            wordImg.sprite = wordSprites[10]; break;
    //        case "Splat":
    //            wordImg.sprite = wordSprites[11]; break;
    //        case "That":
    //            wordImg.sprite = wordSprites[12]; break;
    //        case "Vat":
    //            wordImg.sprite = wordSprites[13]; break;
    //        case "Flat":
    //            wordImg.sprite = wordSprites[14]; break;

    //        default: break;
    //    }

    //    FadeIn fadeIn = wordImg.GetComponent<FadeIn>();
    //    fadeIn.Play();
    //}

    public void Choose(Button btn)
    {
        Text txtBtnTextValue = btn.GetComponent<Transform>().GetChild(0).GetComponent<Text>();
		ScoreManager.ins.IncNumOfMoves();
       // SpriteState tempSprtState = btn.spriteState;
        if (txtBtnTextValue.text == answer.ToString())
        {
            print("Correct");
            pts++;
            StartCoroutine(IENext());
            txtMissing.text = answer.ToString();
            for (int i = 0; i < UIBtns.Length; i++ )
            {
                UIBtns[i].GetComponent<Image>().sprite = sprtWrongChoice;
                UIBtns[i].enabled = false;
            }
            //tempSprtState.disabledSprite = sprtRightChoice;
            //btn.spriteState = tempSprtState;
            btn.GetComponent<Image>().sprite = sprtRightChoice;
            audSrc.clip = audClip[0];
            audSrc.Play();
        }
        else
        {
            print("Wrong");
            //tempSprtState.disabledSprite = sprtWrongChoice;
            //btn.spriteState = tempSprtState;
            btn.GetComponent<Image>().sprite = sprtWrongChoice;
            btn.enabled = false;
            audSrc.clip = audClip[1];
            audSrc.Play();
        }
        btn.interactable = false;
    }

    IEnumerator IENext()
    {
        //if(wordIndex == words.Length-1)
        //{
        //    Invoke("Done",1f);
        //    print("GAME OVER");
        //}
        if (pts >= 3)
        {
            Invoke("Done", 1f);
            print("GAME OVER");
        }
        else
        {
            yield return new WaitForSeconds(2f);
            if (wordIndex < words.Length - 1)
            {
                for (int i = 0; i < UIBtns.Length; i++)
                {
                    UIBtns[i].enabled = true;
                    UIBtns[i].GetComponent<Image>().sprite = sprtDefault;
                }
                wordIndex++;
                GameOn();
            }
           
        }
        
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
