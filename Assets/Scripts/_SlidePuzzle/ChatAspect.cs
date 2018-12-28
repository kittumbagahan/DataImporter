using UnityEngine;
using System.Collections;

public class ChatAspect : AspectManager {

	[SerializeField]
	RectTransform gameCompleteDialog;

	// Use this for initialization
	void Start () {
		Aspect();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameCompleteDialog()
	{
		StartCoroutine("_IEGameComplete");
	}

	IEnumerator _IEGameComplete()
	{
		if(gameCompleteDialog != null)
			gameCompleteDialog.gameObject.SetActive(true);

		yield return new WaitForSeconds(3);

		if(gameCompleteDialog != null)
			gameCompleteDialog.gameObject.SetActive(false);

		Application.LoadLevel(Application.loadedLevel);
	}
}
