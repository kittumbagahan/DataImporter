using UnityEngine;
using System.Collections;
//using Holoville.HOTween;

public class PuzzleController : MonoBehaviour {
    
   // [SerializeField]
   // GameObject CompletePuzzle, PuzzleContainer;

	[SerializeField]
	AudioClip completeAudioClip, clickClip;

	AudioSource audioSource;

   // Animator animator;
    [SerializeField]
    Piece[] pieces;
    //int[,] pattern = { { 3, 2, 4, 1, 3, 5, 1, 3 }, { 1, 1, 5, 3, 3, 5, 1, 1 }, { 1, 2, 4, 5, 4, 5, 2, 1 } };
    //int[,] pattern = { { 2, 3, 2, 2, 3, 2, 1, 3 }, { 1, 2, 2, 2, 3, 2, 2, 1 }, { 1, 3, 1, 2, 2, 2, 2, 2 } };

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
       // animator = GetComponent<Animator>();
     //   HOTween.Init();
     //   StartCoroutine("InitializePuzzle");
        InitializePuzzle();
	}

    void PlayClick()
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

   void InitializePuzzle()
    {
        int index = 0;// Random.Range(0, 3);
        print(index);

        for (int i = 0; i < pieces.Length; i++)
        {
          
            index = Random.Range(0, 4);
            pieces[i].value = index;
            
            if (pieces[i].value % 2 == 0)
            {
                pieces[i].IsCorrect = false;
                pieces[i].transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                pieces[i].IsCorrect = true;
                pieces[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            
           
        }

    
        for (int i = 0; i < 8; i++)
        {
            
            pieces[i].IsPressable = true;


        }
       
    }

    //IEnumerator InitializePuzzle()
    //{
    //    int index = 0;// Random.Range(0, 3);
    //    print(index);

    //    //GameObject piece;               

    //    for (int i = 0; i < pieces.Length; i++)
    //    {
    //        //Spin spin = pieces[i].GetComponent<Spin>();
    //        index = Random.Range(0, 4);
    //        float deg = 180f;
    //        float tempDeg = deg;
    //        pieces[i].value = index; //pattern[index, i];
    //        for (int j = 0; j < index; j++)
    //        {
                
    //            //pieces[i].transform.SetLocalZRot(deg);
    //            deg += tempDeg;
    //            ////DO NOT USE HOTWEEN OR QUATERNION.SLERP HERE... THIS CAUSES SUDDEN HANG
    //            ////HOTween.To(pieces[i].transform, 0.2f, "rotation", new Vector3(0 ,0, -180), true);
    //            ////pieces[i].play = true;
               
    //            //pieces[i].value++;

    //            //while (pieces[i].Play == true)
    //            //{
    //            //    yield return new WaitForSeconds(0.01f);
    //            //}
               
    //            pieces[i].Rotation();
    //            //audioSource.PlayOneShot(clickClip);
    //            audioSource.clip = clickClip;
    //            audioSource.Play();
    //        }
            
    //        //pieces[i].transform.SetLocalZRot(deg);

    //        pieces[i].transform.rotation = Quaternion.Euler(0,0,deg);
    //        //spin.finalZ = deg;
    //        //while (spin.finalZ >= 180)
    //        //{
    //        //    spin.finalZ -= 360;
    //        //}
    //        //if (spin.finalZ < 0)
    //        //{
    //        //    spin.finalZ = 180;
    //        //}
    //        //else
    //        //{
    //        //    spin.finalZ = 360;
    //        //}
    //        //spin.SpinToFinalZ();
    //    }

    //    yield return new WaitForSeconds(1f);
    //    for (int i = 0; i < 8; i++)
    //    {
    //       // piece = PuzzleContainer.transform.GetChild(child).gameObject;
    //       // piece.GetComponent<Piece>().IsPressable = true;
    //        pieces[i].IsPressable = true;
            
           
    //    }
    //  //  print("press");
    //}

	#region EVENT
    void OnEnable()
    {
        //Piece.OnCheckPiece += CheckAllPiece;
		//Piece.onPuzzlePieceClick += PlayClickAudio;
        Piece.onPuzzlePieceClick += PlayClick;
        Piece.onPuzzlePieceClick += CheckAllPiece;

        print("puzzle controller open");
    }

	void OnDisable()
	{
		//Piece.OnCheckPiece -= CheckAllPiece;
		//Piece.onPuzzlePieceClick -= PlayClickAudio;
        Piece.onPuzzlePieceClick -= PlayClick;
        Piece.onPuzzlePieceClick -= CheckAllPiece;
        print("puzzle controller close");
    }
	#endregion

	void PlayCompleteSound()
	{
		audioSource.PlayOneShot(completeAudioClip);
	}

	

    bool isAllCorrect = true;
    public void CheckAllPiece()
    {
        print("CheckAllPiece");
        for (int i = 0; i < pieces.Length; i++)
        {
            Piece piece = pieces[i];//PuzzleContainer.transform.GetChild(i).GetComponent<Piece>();
            if (!piece.IsCorrect)
            {
                isAllCorrect = false;
                print(piece.name + " " + piece.IsCorrect);
                i = 100;
            }
            else
            {
                isAllCorrect = true;
                print(piece.name + " " + piece.IsCorrect);
            }
            print(piece.transform.rotation.z + " " + piece.name + i);
        }

        if(isAllCorrect)
        {
            print("perfect");
           // animator.enabled = true;
			//PlayCompleteSound();
            ActivityDone.instance.Done();
            
        }
        else
        {
            print("not perfect");
        }        
    }  
}
