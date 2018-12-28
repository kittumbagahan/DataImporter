using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DoneButton : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	RectTransform dialog;

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		ActivityDone.instance.Done();
	}

	#endregion
}
