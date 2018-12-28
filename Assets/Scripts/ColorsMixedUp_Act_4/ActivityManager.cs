using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Holoville.HOTween;

public class ActivityManager : MonoBehaviour {

	[SerializeField]
	AudioClip puzzleCompleteClip;

	AudioSource audioSource;

	[SerializeField]
	Sprite[] smallPuzzlePiece, mediumPuzzlePiece, largePuzzlePiece;

	[SerializeField]
	GameObject[] puzzleAward;

	[SerializeField]
	GameObject smallPuzzle, mediumPuzzle, largePuzzle;

	int puzzleIndex;

	public delegate void Reset();
	public static event Reset OnComplete;

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
		puzzleIndex = 0;
		StartCoroutine(NextPuzzle(0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Rotation(GameObject _puzzlePiece)
	{
		switch(Random.Range(1,4))
		{
		case 1:
			_puzzlePiece.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 090));
			break;
		case 2:
			_puzzlePiece.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
			break;
		case 3:
			_puzzlePiece.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
			break;		
		}
	}

	void Checker()// check puzzle piece every tap
	{
		if(smallPuzzle.GetComponent<PuzzlePieceRotator>().IsCorrect &&
		   mediumPuzzle.GetComponent<PuzzlePieceRotator>().IsCorrect &&
		   largePuzzle.GetComponent<PuzzlePieceRotator>().IsCorrect)
		{
			print("correct");

			// disable the buttons
			smallPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = false;
			mediumPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = false;
			largePuzzle.GetComponent<PuzzlePieceRotator>().isPressable = false;

			audioSource.PlayOneShot(puzzleCompleteClip);

			HOTween.To(puzzleAward[puzzleIndex - 1].transform, 1, "localScale", new Vector3(1, 1, 1));

			if(OnComplete != null)
				OnComplete();

			if(puzzleIndex < smallPuzzlePiece.Length)
			{
				StartCoroutine(NextPuzzle(1));
			}
			else
			{
				StartCoroutine("Done");
			}
		}
		else
		{
			// enable the buttons
			smallPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = true;
			mediumPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = true;
			largePuzzle.GetComponent<PuzzlePieceRotator>().isPressable = true;
		}
	}

	IEnumerator NextPuzzle(float delay)
	{
		yield return new WaitForSeconds(delay);

		if(puzzleIndex < smallPuzzlePiece.Length)
		{
			smallPuzzle.GetComponent<Image>().sprite = smallPuzzlePiece[puzzleIndex];
			mediumPuzzle.GetComponent<Image>().sprite = mediumPuzzlePiece[puzzleIndex];
			largePuzzle.GetComponent<Image>().sprite = largePuzzlePiece[puzzleIndex];
			
			Rotation(smallPuzzle);
			Rotation(mediumPuzzle);
			Rotation(largePuzzle);
			
			puzzleIndex++;
		}	

		// enable the buttons
		smallPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = true;
		mediumPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = true;
		largePuzzle.GetComponent<PuzzlePieceRotator>().isPressable = true;
	}

	IEnumerator Done()
	{
		// disable the buttons
		smallPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = false;
		mediumPuzzle.GetComponent<PuzzlePieceRotator>().isPressable = false;
		largePuzzle.GetComponent<PuzzlePieceRotator>().isPressable = false;

		yield return new WaitForSeconds(1);
		ActivityDone.instance.Done();
	}

	void OnEnable()
	{
		PuzzlePieceRotator.OnClick += Checker;
	}

	void OnDisable()
	{
		PuzzlePieceRotator.OnClick -= Checker;
	}
}
