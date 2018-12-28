using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public static Tile instance;

    [SerializeField]
    bool blankTile;

	[SerializeField]
	string CorrectTile;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsCorrect
	{
		get
		{
			if(transform.childCount > 0 && CorrectTile == transform.GetChild(0).gameObject.name)
				return true; 
			else
				return false;
		}
	}

    public bool IsBlank
    {
        get
        {
            return blankTile;
        }
    }
}
