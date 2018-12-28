using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ImageManager : MonoBehaviour{

	public static ImageManager instance;

	[SerializeField]
	RectTransform[] tile;

	RectTransform blankTile, left, right, up, down;

	const float animationWaitTime = 0.7f;
    SwipeControl swipeCntrl;
    [SerializeField]
    AudioClip swipeClip;
	// Use this for initialization
	void Start () {
        swipeCntrl = GameObject.Find("SwipeController").GetComponent<SwipeControl>();
        swipeCntrl.SetMethodToCall(SwipeMethod);
        swipeCntrl.enabled = false;
        print("called");
		instance = this;
		blankTile = tile[0];
		right = tile[1];
		down = tile[3];

        Invoke("Go",3f);

		//RandomTilePosition();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Go()
    {
        swipeCntrl.enabled = true;
    }

//	void RandomTilePosition()
//	{
//		RectTransform temp;
//		int index;
//		for (int i = tile.Length; i >= 1; i--) {
//			index = Random.Range(1, i);
//			temp = tile[index].GetChild(0);
//			tile[index] = tile[i];
//			tile[i] = temp;
//		}
//	}

	void SwipeMethod (SwipeControl.SWIPE_DIRECTION iDirection)
	{
		ScoreManager.ins.IncNumOfMoves();
		switch(iDirection)
		{
		case SwipeControl.SWIPE_DIRECTION.SD_UP:
			//print("up");
			if(canSwipe)
				SwipeUp();
			break;
		case SwipeControl.SWIPE_DIRECTION.SD_DOWN:
			//print("down");
			if(canSwipe)
				SwipeDown();
			break;
		case SwipeControl.SWIPE_DIRECTION.SD_LEFT:
			//print("left");
			if(canSwipe)
		 		SwipeLeft();
			break;
		case SwipeControl.SWIPE_DIRECTION.SD_RIGHT:
			//print("right");
			if(canSwipe)
				SwipeRight();
			break;
		}

		StartCoroutine("CheckTile");
		BlankTileCheck();
	}

	public RectTransform BlankTile
	{
		get { return blankTile; }
		set { blankTile = value; }
	}

	public void SwipeLeft()
	{
		if(right != null)
		{
            UI_SoundFX.ins.Play(swipeClip);
			//right.GetChild(0).GetComponent<RectTransform>().position = blankTile.GetComponent<RectTransform>().position;
			StartCoroutine(SwipeTween(right.gameObject, blankTile.gameObject));
			right.GetChild(0).transform.SetParent(blankTile.transform);
			blankTile = right;
			//print("blank " + blankTile.gameObject.name);
		}
	}

	public void SwipeRight()
	{
		if(left != null)
		{
            UI_SoundFX.ins.Play(swipeClip);
			//left.GetChild(0).GetComponent<RectTransform>().position = blankTile.GetComponent<RectTransform>().position;
			StartCoroutine(SwipeTween(left.gameObject, blankTile.gameObject));
			left.GetChild(0).transform.SetParent(blankTile.transform);
			blankTile = left;
			//print("blank " + blankTile.gameObject.name);
		}
	}

	public void SwipeUp()
	{
		if(down != null)
		{
            UI_SoundFX.ins.Play(swipeClip);
			//down.GetChild(0).GetComponent<RectTransform>().position = blankTile.GetComponent<RectTransform>().position;
			StartCoroutine(SwipeTween(down.gameObject, blankTile.gameObject));
			down.GetChild(0).transform.SetParent(blankTile.transform);
			blankTile = down;
			//print("blank " + blankTile.gameObject.name);
		}
	}

	public void SwipeDown()
	{
		if(up != null)
		{
            UI_SoundFX.ins.Play(swipeClip);
			//up.GetChild(0).GetComponent<RectTransform>().position = blankTile.GetComponent<RectTransform>().position;
			StartCoroutine(SwipeTween(up.gameObject, blankTile.gameObject));
			up.GetChild(0).transform.SetParent(blankTile.transform);
			blankTile = up;
			//print("blank " + blankTile.gameObject.name);
		}
	}

 	public void BlankTileCheck()
	{
		switch(blankTile.gameObject.name)
		{
		case "Tile A-1":
			left = null;
			right = tile[1];
			up = null;
			down = tile[3];
			break;

		case "Tile A-2":
			left = tile[0];
			right = tile[2];
			up = null;
			down = tile[4];
			break;

		case "Tile A-3":
			left = tile[1];
			right = null;
			up = null;
			down = tile[5];
			break;

		case "Tile B-1":
			left = null;
			right = tile[4];
			up = tile[0];
			down = tile[6];
			break;

		case "Tile B-2":
			left = tile[3];
			right = tile[5];
			up = tile[1];
			down = tile[7];
			break;

		case "Tile B-3":
			left = tile[4];
			right = null;
			up = tile[2];
			down = tile[8];
			break;

		case "Tile C-1":
			left = null;
			right = tile[7];
			up = tile[3];
			down = null;
			break;

		case "Tile C-2":
			left = tile[6];
			right = tile[8];
			up = tile[4];
			down = null;
			break;

		case "Tile C-3":
			left = tile[7];
			right = null;
			up = tile[5];
			down = null;
			break;
		}
	}

	bool isDone = true;
	public IEnumerator CheckTile()
	{
		yield return new WaitForSeconds(animationWaitTime);
		for (int i = 0; i < tile.Length; i++) {
            if (!tile[i].GetComponent<Tile>().IsBlank)
            {
                if (!tile[i].GetComponent<Tile>().IsCorrect)
                {
                    print("Nope");
                    yield break;
                }
            }
		}
		//GameObject.Find("ChatAct3").GetComponent<ChatAspect>().GameCompleteDialog();
        Invoke("Done",0.1f);
	}

	bool canSwipe = true;

	IEnumerator SwipeTween(GameObject from, GameObject to)
	{
		canSwipe = false;
		iTween.MoveTo(from.transform.GetChild(0).gameObject, to.gameObject.transform.position, animationWaitTime);
		yield return new WaitForSeconds(animationWaitTime);
		canSwipe = true;
	}

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
