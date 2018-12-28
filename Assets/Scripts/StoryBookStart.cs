using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryBookStart : MonoBehaviour
{

    public static StoryBookStart instance;
    [SerializeField]
    public GameObject book;
    [SerializeField]
    RectTransform btnNext, btnPrev, btnRead, btnAct, btnBookShelf, BG;
    [SerializeField]
    Button[] btns;
    public ReadType selectedReadType;
    [SerializeField]
    AudioClip audClipClick;
    AudioSource audSrc;
    CachedAssetBundle bundle;
    bool clicked;
    [SerializeField]
    bool downloadBook = true;
    [SerializeField]
    GameObject loadingObj;
    [SerializeField]
    float bgMusicVolume = 0.1f;
    //float tempBgMusicVolume;
  
    public Button btnAgain;
    void Start()
    {
        instance = this;
        audSrc = GetComponent<AudioSource>();
        bundle = GetComponent<CachedAssetBundle>();
        btnNext.gameObject.SetActive(false);
        btnPrev.gameObject.SetActive(false);
        btnAgain.gameObject.SetActive(false);
       
       
        //reset the bg volume to original from reading volume 0.1f
        BG_Music.ins.SetVolume(0.5f);
        //BG_Music.ins.SetToReadingVolume();
    }


    public void DisableButtons()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = false;
        }
    }


    public void Read(ReadType readType)
    {
        if (!clicked)
        {
            //DisableButtons();
            btnNext.gameObject.SetActive(true);
            btnPrev.gameObject.SetActive(true);
            clicked = true;
            selectedReadType = readType;
            audSrc.PlayOneShot(audClipClick);
            StartCoroutine(IERead());
            

        }

    }
    IEnumerator IERead()
    {
        if (!BG_Music.ins.Audio.mute)
        {

        }
        BG_Music.ins.SetVolume(bgMusicVolume);
        if (!downloadBook)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(book, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            loadingObj.SetActive(true);
            yield return StartCoroutine(bundle.IELoadAsset());
        }

        btnNext.gameObject.SetActive(true);
        btnPrev.gameObject.SetActive(true);
        loadingObj.SetActive(false);
        BG.gameObject.SetActive(false);



    }
    public void ReStart()
    {
        btnNext.gameObject.SetActive(false);
        btnPrev.gameObject.SetActive(false);

        BG.gameObject.SetActive(false);

        print("Restart");
    }

    public void Activity(string name)
    {

        //Application.LoadLevel(name);
        audSrc.PlayOneShot(audClipClick);
        StartCoroutine(IELoad(name));
    }
    //float val = 0;
    IEnumerator IELoad(string s)
    {
        AsyncOperation async = null;
        async = Application.LoadLevelAsync(s);

        yield return async;

    }

}
