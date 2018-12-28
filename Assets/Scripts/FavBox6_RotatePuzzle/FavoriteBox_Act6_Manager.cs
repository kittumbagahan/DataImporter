using UnityEngine;
using System.Collections;

public class FavoriteBox_Act6_Manager : MonoBehaviour {
    public static FavoriteBox_Act6_Manager ins;
    [SerializeField]
    FavBox6_PuzzlePiece[] pieces;

    [SerializeField]
    AudioClip clickClip;

    AudioSource audSrc;
	void Start () {
        ins = this;
        Initialize();
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.maxMove = 8;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Initialize()
    {
        for (int i = 0; i < pieces.Length; i++ )
        {
            if (Random.Range(0, 100) <= 80)
            {
                pieces[i].transform.rotation = Quaternion.Euler(0, 0, 180);
                pieces[i].isUpsideDown = true;
            }
            else
            {
                pieces[i].isUpsideDown = false;
            }
        }
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].Enable = true;
        }
    }

    public void PlaySFXClick()
    {
        audSrc.clip = clickClip;
        audSrc.Play();
    }

    public void Check()
    {
        int score = 0;
        for (int i = 0; i < pieces.Length; i++ )
        {
            if(pieces[i].isUpsideDown == false)
            {
                score++;
            }
        }

        if(score == pieces.Length)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].Enable = false;
            }
            print("puzzle complete!");
            Invoke("Done",1.5f);
        }
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
