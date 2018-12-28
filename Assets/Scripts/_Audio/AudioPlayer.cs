using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AudioPlayer : MonoBehaviour, IPointerClickHandler {

	public delegate void PlayAudio();
	public static event PlayAudio onClick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		if(onClick != null)
			onClick();
	}

	#endregion
}
