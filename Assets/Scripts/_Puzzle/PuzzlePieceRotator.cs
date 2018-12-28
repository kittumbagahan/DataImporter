using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Holoville.HOTween;

public class PuzzlePieceRotator : MonoBehaviour {

	public delegate void PuzzleClick();
	public static event PuzzleClick OnClick;

	enum Direction { Clockwise, Counterclockwise }

	[SerializeField]
	Direction direction;

	public bool isPressable = true;

	[SerializeField]
	bool isCorrect;

	// Use this for initialization
	void Start () {
		//isPressable = true;
		isCorrect = false;
		HOTween.Init();
		ScoreManager.ins.AW();
	}
	

	#region Event
	void OnEnable()
	{
		ActivityManager.OnComplete += Reset;
	}

	void OnDisable()
	{
		ActivityManager.OnComplete -= Reset;
	}
	#endregion

	IEnumerator Click()
	{
		isPressable = false;
		ScoreManager.ins.IncNumOfMoves();
		if(direction == Direction.Clockwise)
		{
			HOTween.To(gameObject.transform, 0.5f, "rotation", new Vector3(0, 0, -90), true);
		}
		else if(direction == Direction.Counterclockwise)
		{
			HOTween.To(gameObject.transform, 0.5f, "rotation", new Vector3(0, 0, 90), true);
		}
		yield return new WaitForSeconds(0.5f);

		//isPressable = true;
		//print(transform.localRotation.eulerAngles.z);

		if(transform.localRotation.eulerAngles.z < 90)
		{
			isCorrect = true;
		}
		else
		{
			isCorrect = false;
		}

		if(OnClick != null)
			OnClick();

	}

	void OnMouseDown()
	{
		if(isPressable)// && !isCorrect
			StartCoroutine("Click");
	}

	void Reset()
	{
		isCorrect = false;
		//isPressable = true;
	}
	
	public bool IsCorrect
	{
		get { return isCorrect; }
	}
}

