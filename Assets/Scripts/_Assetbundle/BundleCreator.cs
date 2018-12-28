using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;

public class BundleCreator{
	
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }

	
}
#endif