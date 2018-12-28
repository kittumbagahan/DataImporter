using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Cheat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("paid") == 1){
			gameObject.SetActive(false);
		}
	}
	


    public void Unlock()
    {
		if(PlayerPrefs.GetInt("paid") == 0)
		{
			PlayerPrefs.SetInt("bundle_all", 1);
			PlayerPrefs.SetInt("paid", 1);
			//SceneManager.LoadScene("initialization");	
		}
        
    }
}
