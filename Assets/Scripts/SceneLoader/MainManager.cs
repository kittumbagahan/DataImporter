using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour {

	public GameObject sceneLoader;
	void Start () {
		//Instantiate(sceneLoader);
        print(SceneManager.GetActiveScene().ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
