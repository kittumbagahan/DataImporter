using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundsFantastic_Act3_Manager : MonoBehaviour {

    public Image imgContentMain;
    public Image imgContentStart;
    public Sprite[] sprtMainContents;
    public Button[] btns;

    public Sprite sprtScreenBG;
    public Button btnRecent;

    Sprite tempSprtScreenBG;
    AudioSource audSrc;
	void Start () {
        audSrc = GetComponent<AudioSource>();
        tempSprtScreenBG = imgContentStart.sprite;
      //  SaveTest.Save();
	}

    public void Press(Sprite sprt)
    {
        if (!imgContentMain.gameObject.activeInHierarchy) {
            
            imgContentMain.gameObject.SetActive(true);
          
        }
        imgContentMain.enabled = true;
        imgContentMain.sprite = sprt;
        imgContentStart.sprite = sprtScreenBG;
    }

    public void Press(AudioClip clip)
    {
        audSrc.clip = clip;
        audSrc.Play();
    }

    public void Press(Button thisBtn)
    {
        for (int i = 0; i < btns.Length; i++ )
        {
          //  btns[i].enabled = false;
            btns[i].interactable = true;
        }
        thisBtn.enabled = true;
        thisBtn.interactable = false;
        btnRecent = thisBtn;
        StartCoroutine(IEPlay());
    }

    public void Disable(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Enable(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Stop(Sprite sprt)
    {
        imgContentStart.sprite = sprt;
        imgContentMain.enabled = false;
       if(audSrc.isPlaying)
       {
           audSrc.Stop();
       }
    }

    public void StopSound()
    { 
        if(audSrc.isPlaying){
            audSrc.Stop();
        }
    }

    IEnumerator IEPlay()
    { 
        while(audSrc.isPlaying){
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].enabled = true;
        }
        btnRecent.interactable = true;
    }
}
