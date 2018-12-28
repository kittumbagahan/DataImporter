using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Holoville.HOTween;

public class Medal : MonoBehaviour {

	[SerializeField]
	RectTransform[] medal;

	public AudioClip medalCompleteClip;
	public AudioClip[] tryAgain;
	bool isMedalComplete;

	// Use this for initialization
	void Start () 
	{

	}

	int index;

	public void GenerateMedal()
	{
		index = Random.Range(0, medal.Length);

		for (int i = 0; i < medal.Length; i++) 
		{
			if(index == i)
			{
				HOTween.To(medal[i].gameObject, 1, "scale", new Vector3(1, 1, 1));
				//medal[i].GetComponent<Image>().enabled = true;
			}
//			else
//			{
//				medal[i].GetComponent<Image>().enabled = false;
//			}
		}
	}

	public int MedalIndex
	{
		get { return index; }
	}

    public bool MedalComplete(int index)
    {
        //medal[index].enabled = true;

		HOTween.To(medal[index].gameObject.transform, 1, "localScale", new Vector3(1, 1, 1));

		medal[index].GetComponent<MedalValue>().IsDone = true;

        for (int i = 0; i < medal.Length; i++)
        {
			if(!medal[i].GetComponent<MedalValue>().IsDone)//medal[i].enabled == 
			{
				return false;
            }
        }

		return true;
    }

	public bool IsDone(int index)
	{
		if(medal[index].transform.localScale.x == 1)
		{
			print("already done");
			return true;
		}
		else
		{
			print("not yet done");
			return false;
		}
	}

	public bool IsMedalComplete
	{
		get { return isMedalComplete; }
	}
}

