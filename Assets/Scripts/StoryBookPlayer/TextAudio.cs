using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextAudio : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	AudioClip _audioClip;
    AudioSource audSrc;
	// Use this for initialization

    public AudioClip clip {
        get { return _audioClip; }
    }

    void Awake()
    {
       
    
    }
	void Start () {
        audSrc = GetComponent<AudioSource>();
       // audSrc.clip = _audioClip;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        audSrc.Play();
    }

	public AudioClip audioClip
	{
		get{ return _audioClip; }
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		if(GetComponent<Button>().interactable)
		{
			StoryBookPlayer.instance.PlayAudio(_audioClip);
		}
	}

	#endregion
}
