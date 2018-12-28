using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IEndDragHandler {

	// Use this for initialization
	void Start () {
	
	}
	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		AnagramActivityManager.instance.Check();
	}

	#endregion
}
