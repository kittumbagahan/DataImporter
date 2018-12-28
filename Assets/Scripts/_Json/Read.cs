using System;
using System.Collections;
using System.IO;
using LitJson;
using UnityEngine;

public class Read : MonoBehaviour {

	public static Read instance;

	string jsonString;
	JsonData data;

	// Use this for initialization
	void Awake () {
		instance = this;

		//FileInfo fileInfo = new FileInfo(Application.dataPath + "/Resources/StoryBookActivityScene.json");
		string fileInfo = Resources.Load<TextAsset>("StoryBookActivityScene").text;
		//jsonString = File.ReadAllText(Application.dataPath + "/Resources/StoryBookActivityScene.json");
		data = JsonMapper.ToObject(fileInfo);
		print(jsonString + "\n" + "READ SCRIPT " + gameObject.name);
	}

    //"BUTTON INDEX" IS USE TO DIVIDE ONE ACTIVITY TO MANY
    //INDEX IN "StoryBookActivityScene.JSON" IS USE AS A SET INDEX TO SET AN ACTIVITY START UP INDEX
    //get the scene to be loaded
	public string SceneName(StoryBook storyBook, Module module, int buttonIndex)
	{
		return data[storyBook + "_" + module][buttonIndex]["scene"].ToString();
	}

    //get the index of the scene
	public int SceneIndex(StoryBook storyBook, Module module, int buttonIndex)
	{
		return (int)data[storyBook + "_" + module][buttonIndex]["index"];
	}
}
