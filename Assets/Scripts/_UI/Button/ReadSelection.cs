using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ReadSelection : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	ReadType readType;


	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		StoryBookStart.instance.Read(readType);
	}
	#endregion
}
