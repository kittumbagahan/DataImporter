using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzlePieceStopper : MonoBehaviour {

	[SerializeField]
	GameObject[] puzzleContainer;

	[SerializeField]
	GameObject tryAgain, pushButton;

	[SerializeField]
	Medal medal;

	int startCount, endCount;

    bool isPressable;

	AudioSource audioSource;

  
	void Start () 
	{
		startCount = 0;
		endCount = puzzleContainer.Length;
        isPressable = true;
		audioSource = GetComponent<AudioSource>();
		ScoreManager.ins.maxMove = 12;
		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Stop()
	{
		if(startCount < puzzleContainer.Length)
		{
			if(pushButton != null) pushButton.SetActive(false);

			puzzleContainer[startCount].transform.GetChild(0).GetComponent<CarouselMove>().Stop();
			puzzleContainer[startCount].GetComponent<AsPerSpec.CarouselToggler>().SnapToClosest();
			
			startCount++;

			if(startCount == puzzleContainer.Length)
			{
				print("check puzzle piece");
				StartCoroutine("CheckPuzzlePiece");
			}		
			ScoreManager.ins.IncNumOfMoves();
		}
	}
	
	bool isCorrect;
 	IEnumerator CheckPuzzlePiece()
	{
		yield return new WaitForSeconds(0.5f);

        List<int> puzzlePieceIndex = new List<int>();
		
		isCorrect = true;

		print("Medal Index " + medal.GetComponent<Medal>().MedalIndex);

		for (int i = 0; i < puzzleContainer.Length; i++) 
		{
			foreach (var item in puzzleContainer[i].GetComponent<ToggleGroup>().ActiveToggles()) 
			{
				puzzlePieceIndex.Add(item.GetComponent<PuzzleIndex>().Index);
				print("puzzle piece index " + puzzlePieceIndex + " name " + item.name);
			}
        }

        for (int i = 0; i < puzzlePieceIndex.Count - 1; i++)
        {
            if (puzzlePieceIndex[i] != puzzlePieceIndex[i + 1])
            {
                isCorrect = false;
                isPressable = false;
				audioSource.PlayOneShot(medal.gameObject.GetComponent<Medal>().tryAgain[Random.Range(0, medal.gameObject.GetComponent<Medal>().tryAgain.Length)]);
                StartCoroutine("TryAgain");
                break;
            }
        }

		if(isCorrect)
		{
			print("Correct!");

			if(!medal.IsDone(puzzlePieceIndex[0]))
				audioSource.PlayOneShot(medal.gameObject.GetComponent<Medal>().medalCompleteClip);

			print("play audio");

			if(medal.MedalComplete(puzzlePieceIndex[0]))
			{
				StartCoroutine("ActivityCompleteDelay");
            }
            else
            {
                StartCoroutine("Restart");
            }            
			//GetComponent<ActivityDone>().Done();
		}
	}

	IEnumerator ActivityCompleteDelay()
	{
		yield return new WaitForSeconds(1);
		GetComponent<ActivityDone>().Done();
	}

	void PushButton()
	{		
		pushButton.SetActive(true);
	}

	IEnumerator TryAgain()
	{
		tryAgain.SetActive(true); 
               
		yield return new WaitForSeconds(3);

        if(pushButton != null) PushButton();

        isPressable = true;

		startCount = 0;
		endCount = puzzleContainer.Length;

		tryAgain.SetActive(false);

		for (int i = 0; i < puzzleContainer.Length; i++) 
		{
			puzzleContainer[i].transform.GetChild(0).GetComponent<CarouselMove>().IsMoving = true;
		}
	}

    IEnumerator Restart()
    {                
        yield return new WaitForSeconds(3);

		if(pushButton != null) PushButton();

        startCount = 0;
        endCount = puzzleContainer.Length;        

        for (int i = 0; i < puzzleContainer.Length; i++)
        {
            puzzleContainer[i].transform.GetChild(0).GetComponent<CarouselMove>().IsMoving = true;
        }
    }
}

