using UnityEngine;
using System.Collections;

public class UI_SoundFX : MonoBehaviour {

    public static UI_SoundFX ins;
    [SerializeField]
    AudioClip uI_btn_click, ui_turnPage;
    [SerializeField]
    AudioClip finFX;
    AudioSource audSrc;
    void Awake()
    {
       
        if (ins == null)
        {
            ins = this;
            audSrc = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
           
        }
        else {
            Destroy(gameObject);
        }
    }


	void Start () {
      
	}

    public bool IsPlaying()
    {
        if (audSrc.isPlaying) return true;
        else return false;
    }
    public void PlayUIButtonClick()
    {
        audSrc.PlayOneShot(uI_btn_click);
    }

    public void PlayUITurnPage()
    {
        audSrc.PlayOneShot(ui_turnPage);
    }

    public void Play(AudioClip clip)
    {
        audSrc.PlayOneShot(clip);
    }

    public void PlaySetFin()
    {
        audSrc.clip = finFX;
        audSrc.Play();
    }

}
