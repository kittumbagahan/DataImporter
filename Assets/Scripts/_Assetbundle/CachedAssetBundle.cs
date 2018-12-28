using UnityEngine;
using System.Collections;
using System;
public class CachedAssetBundle : MonoBehaviour {
    
	public static CachedAssetBundle ins;

    public string assetName;
    public string BundleURL;
    public int version;

    AssetBundle bundle;
	GameObject obj;
    void Awake()
    {
        ins = this;
        //DontDestroyOnLoad(this);
    }

    public IEnumerator IELoadAsset()
    {
		WWW www;
        while (!Caching.ready)
            yield return null;
        using (www = WWW.LoadFromCacheOrDownload(BundleURL, version))
        {
			if (www.isDone) {}//print("dowload finished");
			else {}//print("downloading");
            while (!www.isDone)
            {
                //print("download progress " + www.progress * 100);
                yield return new WaitForFixedUpdate();
            }
            bundle = www.assetBundle;

        }
		AssetBundleRequest request = bundle.LoadAssetAsync (assetName, typeof(GameObject));
		yield return request;
		obj = request.asset as GameObject;
		/*
        if (bundle != null)
        {
            SpawnAsset(assetName);
        }
        */
        bundle.Unload(false);
		www.Dispose();
		Instantiate(obj);
		print(assetName + " loaded!");
    }

	public void UnloadBundle()
	{
		//print(bundle.name);
		//Destroy(obj);
	}

    void SpawnAsset(string name)
    { 
        if(name != "")
        {
             //print("MOON LOVERSD");
             //GameObject obj = (GameObject)bundle.LoadAsset(name);
			 //GameObject obj = (GameObject)bundle.LoadAssetAsync(name);
             //Instantiate(obj);
             
        }
    }
   
}
