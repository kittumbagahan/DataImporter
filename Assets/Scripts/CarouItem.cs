using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CarouItem : MonoBehaviour, IPointerClickHandler{

    [SerializeField]
    string bundlePID;
    [SerializeField]
    byte bookIndex;
    
	public Transform destination;
    private Transform parentContent;

    float dist = 0;
    
	public float min, max;

    [SerializeField]
    string sceneToLoad;
    public bool isClickable;
    bool clicked;
	[SerializeField]
	StoryBook selectedStoryBook;
    private bool spotLight;
    private float origScale = 1;
    [SerializeField]
    GameObject /*imgHighlight,*/ imgClickEffect;
    bool highlighted=false;
    bool highlightPlayed=false;
    //[SerializeField]
    //int locked = 0;
    Image _img;

    void Awake()
    {
        //locked = PlayerPrefs.GetInt("product" + sceneToLoad);
        //if (sceneToLoad.Equals("FavoriteBox")) locked = 1;
    }
    void Start () {
        parentContent = transform.parent;
        origScale -= 0.1f;
        _img = GetComponent<Image>();
        
        //if (locked == 0)
        //{
        //    //print("tts");
        //    _img.color = new Color32(135,135,135,255); //new Color(135, 135, 135, 255);
        //}
        //else
        //    _img.color = new Color32(255, 255, 255, 255);

       
    }
  
	void Update () {
        dist = Vector2.Distance(transform.position, destination.position);
       //print(dist);
       if (dist >= min && dist <= max)
       {
           
          
           //isClickable = true;
           if (!BookshelfManager.ins.aBookIsActive) {
               transform.SetAsLastSibling();
               Grow();
               BookshelfManager.ins.aBookIsActive = true;
           }
           
           //imgHighlight.transform.SetParent(transform);
           //imgHighlight.transform.SetLocalXPos(0);
           //imgHighlight.transform.SetLocalYPos(0);
           if (!BookshelfManager.ins.AudioSrc.isPlaying && isClickable == false && !highlightPlayed)
           {
              
               //print("1");
               if (!clicked) {
                   //transform.parent.GetComponent<AudioSource>().PlayOneShot(BookshelfManager.ins.AudClipBookHighlight); 
                   BookshelfManager.ins.PlayBookActive();
                   highlightPlayed = true;
                   isClickable = true;
                  
               }
               
           }
             
           Book.instance.BookCover(bookIndex);
           Book.instance.BOokDescription(bookIndex);
       }
       else {
           //transform.SetAsFirstSibling();
           if (isClickable && BookshelfManager.ins.aBookIsActive)
               BookshelfManager.ins.aBookIsActive = false;
           isClickable = false;
           highlightPlayed = false;
           Shrink();
       }
    
	}

    void Grow()
    {
        if (transform.GetWidth() < 320f)
        {
            transform.SetHeight(Mathf.Lerp(transform.GetHeight(), 275f, 0.5f));
            transform.SetWidth(Mathf.Lerp(transform.GetWidth(), 290f, 0.5f));
        }
    }

    void Shrink()
    {
        if (transform.GetWidth() > 160f)
        {
           // print(transform.localScale.x);
            transform.SetHeight(Mathf.Lerp(transform.GetHeight(), 150f, 0.08f));
            transform.SetWidth(Mathf.Lerp(transform.GetWidth(), 160f, 0.08f));
           
        }
        if(transform.localScale.x < 1.1f)
        {
          //  transform.SetLocalWidth(1f);
            //transform.SetLocalHeight(1f);
        }

    }

    public void Click()
    {
        //Application.LoadLevel(sceneToLoad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClickable)
        {
            if (sceneToLoad != "")// && locked == 1)
            {
				if(TimeUsageCounter.ins.IsTimeOver())
				{
					MessageBox.ins.ShowOk("Subscription expired. \nPlease email us at \npalabaydev@gmail.com", MessageBox.MsgIcon.msgError, null);
				}
				else
				{
					//print("print " + sceneToLoad);
					StoryBookSaveManager.ins.selectedBook = selectedStoryBook;
					//StoryBookSaveManager.instance. = sceneToLoad;
					Singleton.SelectedBook = selectedStoryBook;
					StartCoroutine(IEClick());
					clicked = true;
					isClickable = false;
				}
               
            }
            else
            {
                if (PlayerPrefs.GetInt(bundlePID) == 0 && PlayerPrefs.GetInt("bundle_all") == 0)
                {
                    // InAppPurchaseManager.ins.ShowBuyWindow();
                }
                else {
                    //print("SHOW DOWNLOAD NOW MESSAGE!");
					MessageBox.ins.ShowOk("Connect to the internet and restart the app to download.", MessageBox.MsgIcon.msgError, null);
                }
                
            }
        }
       
    }

    IEnumerator IEClick()
    {


        //BookshelfManager.ins.PlayBookClick();
        //transform.parent.GetComponent<AudioSource>().PlayOneShot(BookshelfManager.ins.AudClipBookClick);
        BookshelfManager.ins.PlayBookClick();
        imgClickEffect.SetActive(true);
        imgClickEffect.transform.SetParent(transform);
        imgClickEffect.transform.SetLocalXPos(0);
        imgClickEffect.transform.SetLocalYPos(0);
        yield return new WaitForSeconds(1f);
		EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
        SceneLoader.instance.AsyncLoadStr("empty");
    }

  
}
