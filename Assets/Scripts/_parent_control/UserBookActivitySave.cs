using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UserBookActivitySave : MonoBehaviour, IPointerClickHandler {

    ButtonActivity btnAct;
	
    public void UpdatePlayedActivityUsage()
    {
        //2018 08 30//string key = btnAct.Mode + StoryBookSaveManager.ins.oldUsername + StoryBookSaveManager.ins.selectedBook;
        string key = btnAct.Mode + "section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id
            + StoryBookSaveManager.ins.selectedBook;
        // +btnAct.SceneToLoad; //SceneManager.GetActiveScene(); // + SaveTest.Set; DONT GET THE SET IT SEPERATES THE ONE GAME TO MANHY
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
        print("Saved: Activity Key " + key + " val=" + PlayerPrefs.GetInt(key));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        btnAct = GetComponent<ButtonActivity>();
        UpdatePlayedActivityUsage();
    }
}
