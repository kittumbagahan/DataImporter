using UnityEngine;
using System.Collections;

public class FavBox6_PuzzlePiece : MonoBehaviour {

    public bool isUpsideDown = false;
    bool enable = false;
    public bool Enable {
        set { enable = value; }
        get { return enable; }
    }

    FadeIn fadeIn;
	void Start () {
        fadeIn = GetComponent<FadeIn>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click()
    {
        if(enable)
        {
			ScoreManager.ins.IncNumOfMoves();
            fadeIn.Play();
            if (isUpsideDown)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isUpsideDown = false;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 180f);
                isUpsideDown = true;
            }
            FavoriteBox_Act6_Manager.ins.PlaySFXClick();
            FavoriteBox_Act6_Manager.ins.Check();
        }
        
    }
}
