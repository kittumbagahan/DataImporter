using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {
	
	public static SceneLoader instance;
    [SerializeField]
    private string sceneToload;
	[SerializeField]
	GameObject loading;

	void Start () {
		instance = this;

	}

    public string SceneToLoad {
        set { sceneToload = value; }
        get { return sceneToload; }
    }

	public IEnumerator Load(int index)
	{
        loading.gameObject.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(index); //Application.LoadLevelAsync(index);
		yield return async;
	}
	public IEnumerator Load(string name)
	{
      AsyncOperation async = null;
      //print("what is my " + name);
      if(name != "")
      {
          //try { loading.gameObject.SetActive(true); }
          //catch (Exception ex) { print(ex); print("GAMEOBJECT LOADING IS NULL"); }  
          if (loading != null) loading.gameObject.SetActive(true);
          //async = Application.LoadLevelAsync(name);   
		 
          async = SceneManager.LoadSceneAsync(name);
      }
        yield return async;
	}

    public void AsyncLoadStr(string name)
    {
        StartCoroutine(Load(name));
    }

    public void AsyncLoadInt(int index)
    {
        StartCoroutine(Load(index));
    }

    public void LoadInt(int index)
	{
		//Application.LoadLevel(index);
        SceneManager.LoadScene(index);
	}

	public void LoadStr(string name)
	{
        
        if (sceneToload == "")
        {
            //Application.LoadLevel(name);
           AsyncLoadStr(name);
        }
        // //for gc
		// else if(sceneToload == "BookShelf"){
		// 	EmptySceneLoader.ins.sceneToLoad = sceneToload;
		// 	AsyncLoadStr ("empty");
		// }
        else {
            //print(sceneToload + " was loaded because it is not null.");
			//System.GC.Collect();
            //Application.LoadLevel(sceneToload);
            AsyncLoadStr(sceneToload);
        }
		
	}

    void OnDestroy()
    {
        Item.RemoveSubscribers();
        ObjectToSpot.RemoveSubscribers();
    }
}
