using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StoryBookPlayer : MonoBehaviour {

	public static StoryBookPlayer instance;

	[SerializeField]
	GameObject[] text;

    [SerializeField]
    float[] delay;

    [SerializeField]
    AudioClip audClip;
    [SerializeField]
	AudioSource audioSource;

	#region MONO
	void Start () {
		instance = this;
	}

	#endregion MONO

	#region Defined
	public void PlayTextAnimation()
	{
        if (StoryBookStart.instance.selectedReadType != ReadType.ReadItMySelf)
        {
            audioSource.clip = audClip;
            StartCoroutine("TextsAnimation");
        }
		
	}

	IEnumerator TextsAnimation()
	{

        for (int i = 0; i < text.Length; i++)// reset
        {
			if(text[i] != null)			
            	iTween.ScaleTo(text[i].gameObject, new Vector3(1, 1, 1), 0);
        }

		//float time = 0.5f;

		if(StoryBookStart.instance.selectedReadType == ReadType.AutoRead)// shows next and prev button
		{
			SceneSpawner.ins.EnableButtons();
            StoryBookStart.instance.btnAgain.gameObject.SetActive(false);
		}
		else
		{
			SceneSpawner.ins.DisableButton();
		}

		yield return new WaitForSeconds(0.5f);

        if (StoryBookStart.instance.selectedReadType == ReadType.ReadItToMe || StoryBookStart.instance.selectedReadType == ReadType.AutoRead)// --
        {
            audioSource.Play();
        }        

        for (int i = 0; i < text.Length; i++)
        {
            text[i].transform.SetAsLastSibling();

            if (StoryBookStart.instance.selectedReadType == ReadType.ReadItToMe || StoryBookStart.instance.selectedReadType == ReadType.AutoRead)
            {

            }
			if(text[i] != null)
			{
	            //yield return new WaitForSeconds(time);
	            iTween.ScaleTo(text[i].gameObject, new Vector3(1.1f, 1.3f, 1), delay[i]); // change time to audioclip time if audio is available
			}

            yield return new WaitForSeconds(delay[i]);

			if(text[i] != null)
			{
	            iTween.ScaleTo(text[i].gameObject, new Vector3(1, 1, 1), delay[i]);
	            //yield return new WaitForSeconds(0.1f);
			}
        }

		if(StoryBookStart.instance.selectedReadType == ReadType.ReadItMySelf || StoryBookStart.instance.selectedReadType == ReadType.ReadItToMe)
		{
			SceneSpawner.ins.EnableButtons();
		}

		//Pressable();

		if(StoryBookStart.instance.selectedReadType == ReadType.AutoRead)
		{
           SceneSpawner.ins.Next();
		}
	}

	public void PlayAudio(AudioClip clip = null)
	{
		if(clip != null)
		{
			audioSource.PlayOneShot(clip);
		}
		print("play audio clip");
	}

	void Pressable()// also pressable when all audio has been played
	{
		for (int i = 0; i < text.Length; i++) 
		{
			text[i].GetComponent<Button>().interactable = true;
		}
	}
	#endregion
}
