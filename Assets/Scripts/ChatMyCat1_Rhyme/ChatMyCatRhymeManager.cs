using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Text;

public class ChatMyCatRhymeManager : MonoBehaviour {


    public Button btnListen; 
	public List<AudioClip> lstClip;
    public List<Text> lstText;
    public AudioSource audSrcBtn;
    public AudioClip[] audClipBtn;

    AudioSource audSrc;
    public Sprite[] sprtClicked;
    SpriteState tempSpriteState; //remove button pressed sprite to make this visually working
    Button recentBtnClicked = null;
    [SerializeField]
     Button[] btns = new Button[4];
    [SerializeField]
    int index = -1;
    int pts = 0;
    //StringBuilder strBuilder = new StringBuilder();

    void Start () {
        //index++;
        index = SaveTest.Set + 1;
        
        audSrc = GetComponent<AudioSource>();
        btnListen.interactable = false;
        ListOptions();
        for (int i = 0; i < lstText.Count; i++)
        {
            btns[i] = lstText[i].transform.parent.GetComponent<Button>();
            btns[i].interactable = false;
            //btn.enabled = b;
        }
        Invoke("Go", 4f);
		//ScoreManager.ins.maxMove = 15;
		ScoreManager.ins.AW();
    }

    void Go()
    {
        for (int i = 0; i < lstText.Count; i++)
        {
            //btns[i] = lstText[i].transform.parent.GetComponent<Button>();
            btns[i].interactable = true;
            //btn.enabled = b;
        }
        btnListen.interactable = true;
    }

    // Update is called once per frame
    void ListOptions()
    {
        FlyIn flyIn;
        for (int i = 0; i < btns.Length; i++)
        {
            flyIn = btns[i].GetComponent<FlyIn>();
            flyIn.Fly();
        }
        lstText.Shuffle();
        switch (index)
        {
            //set A
            case 0: ChangeTextsTo("Hat", "Kitten", "Rod", "Man"); break;
            case 1: ChangeTextsTo("Goat", "Pail", "House", "Bird"); break;
            case 2: ChangeTextsTo("Hook", "Ball", "Pearl", "Banana"); break;
           //
            case 3: ChangeTextsTo("Bar", "Jail", "Sail", "Ring"); break;
            case 4: ChangeTextsTo("Rain", "Toy", "Balloon", "Seen"); break;
            case 5: ChangeTextsTo("Flower", "Cat", "Snail", "Coat"); break; //boat
           //
            case 6: ChangeTextsTo("Tree", "Toy", "Sea", "Hay"); break; //boy
            case 7: ChangeTextsTo("Dog", "Flat", "Seat", "Mast"); break; //hat
            case 8: ChangeTextsTo("Read", "Leaf", "Cook", "Deck"); break; //book
           //
            case 9: ChangeTextsTo("Rope", "Mat", "Bubble", "Bath"); break; //soap
            case 10: ChangeTextsTo("Hip", "Finger", "Nail", "Shoe"); break; //ship  
            case 11: ChangeTextsTo("Jump", "Sting", "Heel", "Sea"); break; //bee
            //
            case 12: ChangeTextsTo("Deal", "Seal", "Pail", "Coat"); break; //mail
            case 13: ChangeTextsTo("Bone", "Foe", "Moon", "Blue"); break; //shoe
            case 14: ChangeTextsTo("Tire", "Star", "Shine", "Red"); break; //car
            //set C end


            default: break; 
        };

       
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
        try {
            audSrc.clip = lstClip[index];
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
        recentBtnClicked = btnOption.GetComponent<Button>();
        recentBtnClicked.interactable = false;
        //disable other buttons for the meantime to avoid clicking so many button at time
        for (int i = 0; i < btns.Length; i++){
            if (btns[i] != recentBtnClicked){
                btns[i].enabled = false;
            }
        }

        CheckAnswer(txt.text);
		ScoreManager.ins.IncNumOfMoves();
    }

    //void Correct()
    //{
    //    index++;
    //    //print("edi wow");
    //    ListOptions();
    //}

    IEnumerator IEPlay(Button btnPlay) {
        while (audSrc.isPlaying)
        {
            yield return new WaitForSeconds(0.05f);
        }
        btnPlay.interactable = true;
        
    }

    IEnumerator IECorrect() {
        
        print("Correct!");
        tempSpriteState.disabledSprite = sprtClicked[0];
        recentBtnClicked.spriteState = tempSpriteState;
        audSrcBtn.clip = audClipBtn[0];
        audSrcBtn.Play();
        index++;
        btnListen.interactable = false;
        yield return new WaitForSeconds(1f);

        btnListen.interactable = true;
        pts++;
        if (pts < 3)
        {
            ListOptions();
            recentBtnClicked.interactable = true;
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].enabled = true;
            }
        }
        else {
            Invoke("Done",0f);
            print("Game Over"); 
        }
        
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }

    IEnumerator IEWrong() {
        print("WRONG");
        tempSpriteState.disabledSprite = sprtClicked[1];
        recentBtnClicked.spriteState = tempSpriteState;
        audSrcBtn.clip = audClipBtn[1];
        audSrcBtn.Play();
        yield return new WaitForSeconds(1f);
        recentBtnClicked.interactable = true;
        //CheckButtonsState();
        for (int i = 0; i < btns.Length; i++){
            btns[i].enabled = true; 
        }
    }

   
    void CheckAnswer(string ans)
    {
        switch (index)
        {
            //set A
            case 0:
                if (ans == "Hat"){
                    StartCoroutine(IECorrect());
                }
                else {
                    StartCoroutine(IEWrong());
                }
                break;
            case 1:
                if (ans == "House"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 2:
                if (ans == "Pearl"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 3:
                if (ans == "Bar"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 4:
                if (ans == "Seen"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
                //set B
            case 5:
                if (ans == "Coat"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 6:
                if (ans == "Toy"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 7:
                if (ans == "Flat"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 8:
                if (ans == "Cook"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 9:
                if (ans == "Rope"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            //set C
            case 10:
                if (ans == "Hip"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 11:
                if (ans == "Sea"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 12:
                if (ans == "Pail"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 13:
                if (ans == "Blue") {
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            case 14:
                if (ans == "Star"){
                    StartCoroutine(IECorrect());
                }
                else{
                    StartCoroutine(IEWrong());
                }
                break;
            default:
               // ActivityDone.instance.Done();
                print("Game Over"); break;
        }
    }
}
