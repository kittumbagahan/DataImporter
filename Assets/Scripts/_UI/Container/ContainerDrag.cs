using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ContainerDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	float startPoint, endPoint, distance, containerDefaultPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		endPoint = Input.mousePosition.x;
		distance = Mathf.Abs(startPoint - endPoint) * 0.05f;

		if(Input.GetAxis("Mouse X") < 0)
		{
			distance = -distance;
			print("left");
		}
		else if(Input.GetAxis("Mouse X") > 0)
		{
			print("right");
		}

		Debug.Log("Start: " + startPoint + ", End: " + endPoint + ", Distance: " + distance);
		if(OnObjectDrag != null)
			OnObjectDrag(distance);
	}

	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		startPoint = Input.mousePosition.x;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		Debug.Log("end");
	}

	public delegate void Drag(float distance);
	public static event Drag OnObjectDrag;
	#endregion
}
