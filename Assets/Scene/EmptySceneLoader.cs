using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySceneLoader : MonoBehaviour {

	public static EmptySceneLoader ins;

	public string sceneToLoad;

	void Awake()
	{
		if(ins == null)
		{
			ins = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);

		}
	}

   
}
