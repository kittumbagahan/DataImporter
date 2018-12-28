using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class ActivityDone : MonoBehaviour {

	public static ActivityDone instance;
    //[SerializeField]
    //bool showAds;
	[SerializeField]
	AudioClip[] goodJob;
    [SerializeField]
    AudioClip doneFX;
	AudioSource audioSource;
	
	[SerializeField]
	RectTransform dialog;

	//[SerializeField]
	string levelToLoad = "ActivitySelection";

	// Use this for initialization
	void Start () {
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}

    void Update()
    { 
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Item.RemoveSubscribers();
            WordGameManager.RemoveSubscribers();
            WordGameManager_2.RemoveSubscribers();
                       
            SceneManager.LoadScene(0);
        }
    }

	public void Done()
	{
        //if (showAds) AdsManager.ins.ShowAds();
        audioSource.clip = doneFX;
        audioSource.Play();
        Item.RemoveSubscribers();
        WordGameManager.RemoveSubscribers();
        WordGameManager_2.RemoveSubscribers();
		StartCoroutine(ShowDialog());
	}

	IEnumerator ShowDialog()
	{
		dialog.gameObject.SetActive(true);//Show Dialog

		audioSource.PlayOneShot(goodJob[Random.Range(0, goodJob.Length)]);

		yield return new WaitForSeconds(3);
        //if (showAds) {

        //    if (PlayerPrefs.GetInt("played_free_count") >= 3)
        //    {
        //        PlayerPrefs.SetInt("played_free_count", 0);
        //        AdsManager.ins.ShowAds();
        //        yield return new WaitForSeconds(1f);
        //    }
        //    else
        //    {
        //        PlayerPrefs.SetInt("played_free_count", PlayerPrefs.GetInt("played_free_count") + 1);
        //    }

        //} 
        if (1 == UserRestrictionController.ins.restriction)
        {
            SaveTest.Save();
            ToActivitySelection();
        }
        else {
            ToActivitySelection();
           // MessageBox.ins.ShowOk("This is not saved.", MessageBox.MsgIcon.msgInformation, new UnityAction(ToActivitySelection));
        }
       
		//Application.LoadLevel(levelToLoad);
      
	}

    void ToActivitySelection()
    {
        SceneManager.LoadScene(levelToLoad);
    }

	public void PlayAgain()
	{
        //SceneManager.LoadScene(SceneManager.GetActiveScene);
	
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
