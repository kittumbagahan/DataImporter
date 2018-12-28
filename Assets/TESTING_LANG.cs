using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING_LANG : MonoBehaviour {


	public delegate void download(float progress);
	public static event download OnDownload;

	[SerializeField]
	ProgressBar pb;

	[SerializeField]
	float numOfProcess;

	[SerializeField]
	float progress;

	float n=0;

	/*
		2300 process
		1/2300 = 0.00043478 per process

	*/

	void Start () {
		n = 1 / numOfProcess;
		OnDownload += pb.SetProgress;
		StartCoroutine(IEProcess());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator IEProcess()
	{
		for(int i=0; i<numOfProcess; i++)
		{
			progress += n;
			if(OnDownload !=null) OnDownload(progress);
			yield return new WaitForSeconds(n);
		}

	}
}
