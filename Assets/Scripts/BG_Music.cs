using UnityEngine;
using System.Collections;

public class BG_Music : MonoBehaviour {

	public static BG_Music ins;

    [SerializeField]
    AudioClip[] clip;
    AudioSource audSrc;

	float recentClipVolume;

	public float RecentClipVolume{
		get{return recentClipVolume;}
	}

	public AudioSource Audio{
		get{return audSrc;}
	}
    void Awake()
    {
		ins = this;
        audSrc = GetComponent<AudioSource>();
    }
    void Start () {
	   audSrc.clip = clip[Random.Range(0,2)];
       audSrc.Play();

		//will be the normal sound volume
		if(audSrc.clip == clip[0]) {
			audSrc.volume = 0.5f;	
			recentClipVolume = 0.5f;
		}
		else{
			audSrc.volume = 0.5f;	
			recentClipVolume = 0.5f;
		} 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetVolume()
	{
		return audSrc.volume;
		/*if(audSrc.volume < 0.1f)
			return true
		else 
			return false;*/
	}

	public void Mute()
	{
		float x = audSrc.volume;
		while(audSrc.volume > 0.1f)
		{
			x = Mathf.Lerp(x, 0f, 0.01f);
			audSrc.volume = x;
		}
	}

	public void MuteBG()
	{
		if(audSrc.mute) audSrc.mute = false;
		else audSrc.mute = true;
	}

	public void SetVolume(float volume)
	{
		audSrc.volume = volume;
	}

	public void SetToReadingVolume()
	{
		audSrc.volume = 0.1f;
		/*if*(audSrc.clip == clip[0]) audSrc.volume = 0.1f;
		//else audSrc.volume = 0.3f; */
	}
}
