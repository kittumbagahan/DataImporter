using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySceneManager : MonoBehaviour {

	//[SerializeField]
	//SceneLoader sc;
	void Start () 
	{
		if(EmptySceneLoader.ins.sceneToLoad != ""){
			//sc.AsyncLoadStr(EmptySceneLoader.ins.sceneToLoad);

            SceneLoader.instance.AsyncLoadStr (EmptySceneLoader.ins.sceneToLoad);
			//System.GC.Collect ();
		}

	}
	

}
