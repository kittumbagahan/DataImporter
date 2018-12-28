using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	[SerializeField]
	AudioClip audioClip;

	AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	#region Events
	void OnEnable()
	{
		AudioPlayer.onClick += PlayAudio;
	}

	void OnDisable()
	{
		AudioPlayer.onClick -= PlayAudio;
	}
	#endregion

	void PlayAudio()
	{
		audioSource.PlayOneShot(audioClip);
	}
}
