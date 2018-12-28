using UnityEngine;
using System.Collections;

public class ScriptAudioPlayer : MonoBehaviour {

    public bool playOnStart;
    public ScriptAudioPlayer next;
    AudioSource audSrc;
	void Start () {
        audSrc = GetComponent<AudioSource>();
	    if(playOnStart)
        {
            Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        StartCoroutine(IEPlay());
    }

    IEnumerator IEPlay()
    {
        audSrc.Play();
        while(audSrc.isPlaying)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        if(next != null)
        {
            next.Play();
        }
        
    }
}
