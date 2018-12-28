using UnityEngine;
using System.Collections;


public class HowToPlayAudio : MonoBehaviour {

  //  public AudioClip audClip;
    public float delay;
    AudioSource audSrc;


	void Start () {
        audSrc = GetComponent<AudioSource>();
        Invoke("Play",delay);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Play()
    {
        audSrc.Play();
    }
}
