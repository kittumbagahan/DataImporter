using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
/*
 key, value
 * slotID, username
 */


public class UserAccount : MonoBehaviour {

    [SerializeField]
    int userId;
    [SerializeField]
    string userName;
    
    [SerializeField]
    TextMeshProUGUI txtLoadedusrname;
    [SerializeField]
    GameObject btnDelete, btnProgress;
    //[SerializeField]
    //GameObject saveOption;
    Button btn;
    
    //private bool continueSavedGame;
    public Button Btn {
        get { return btn; }
        set { btn = value; }
    }
    public string Username {
        set
        {
            userName = value;
			if (userName.Equals("") || userName.Equals("0")) txtLoadedusrname.text = "Empty";
            else txtLoadedusrname.text = userName;


			if (userName.Equals("") || userName.Equals("0"))
            {
                //btnDelete.SetActive(false);
                btnProgress.SetActive(false);
            }
            else {
                //btnDelete.SetActive(true);
                btnProgress.SetActive(true);
            }
        } 
        get { return userName; } }

    public int UserId { set { userId = value; } get { return userId; } }

	void Start () {
       
        btn = GetComponent<Button>();
        
	}
	
    public void Delete()
    {
        UserParentalManager.ins.PlayClickClip();
        if (Username.Equals(StoryBookSaveManager.ins.activeUser))
        {
            MessageBox.ins.ShowOk("Unable to delete active user.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            MessageBox.ins.ShowQuestion("Delete user " + Username + "?", MessageBox.MsgIcon.msgWarning, new UnityAction(DeleteSaveYes), new UnityAction(DeleteSaveNo));
        }
     
    }

    public void ShowProgress()
    {
        //UserAccountManager.ins.UserBooksManager.Show("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //    "student_id" + userId);
        UserAccountManager.ins.UserBooksManager.Show(userId);

        UserAccountManager.ins.SelectedSlot = this;
    }
 
    public void Click()
    {
        UserAccountManager.ins.SelectedSlot = this;
        UserParentalManager.ins.PlayClickClip();
        if (userName == "")
        {
            UserAccountManager.ins.PanelInput.SetActive(true);
            print("Enter your name");
        }
        else
        {
            //clear user if not loaded and play as guest
            StoryBookSaveManager.ins.activeUser = userName;
            LoadSave();
        }
    }

    void LoadSave()
    {
        //UserParentalManager.ins.PlayClickClip();
        //MessageBox.ins.ShowQuestion("Load user " + Username + "?", MessageBox.MsgIcon.msgInformation, new UnityAction(LoadSavedGameYes), new UnityAction(LoadSavedGameNo));
    }

    void NewUser()
    { 
      
    }

    void OverwriteYes()
    {
        UserParentalManager.ins.PlayClickClip();
        UserAccountManager.ins.Overwrite(true);
        UserAccountManager.ins.PanelInput.SetActive(true);
    }
    void OverwriteNo()
    {
        UserParentalManager.ins.PlayClickClip();
    }
    void LoadSavedGameYes()
    {
        Singleton.userActive = true;
        StoryBookSaveManager.ins.activeUser = userName;
        StoryBookSaveManager.ins.activeUser_id = userId;
        print(userName + " loaded!");
        PrintData(userName);

        UserAccountManager.ins.LoadButton.onClick.RemoveAllListeners();
        if (Random.Range(0, 5) > 2)
        {
            Tammytam.ins.Say("Hello, \n" + userName + "!");
        }
        else
        {
            Tammytam.ins.Say("Let's read \n" + userName + "!");
        }
        
        StartCoroutine(IELoad());
    }
    void LoadSavedGameNo()
    {
        UserParentalManager.ins.PlayClickClip();
    }
    void DeleteSaveNo()
    {
        UserParentalManager.ins.PlayClickClip();
    }
    void DeleteSaveYes()
    {
        PlayerPrefs.DeleteKey("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + userId);
        //2018 9 03 //PlayerPrefs.DeleteKey("student_id" + userId.ToString());
        //UserParentalManager.ins.PlayClickClip();
        //UserAccountManager.ins.DeleteActivity(userName);
        //UserAccountManager.ins.DeleteBooksUsage(userName);
        //UserAccountManager.ins.DeleteActivityUsage(userName);

        UserAccountManager.ins.DeleteActivity("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + userId);
        UserAccountManager.ins.DeleteBooksUsage("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + userId);
        UserAccountManager.ins.DeleteActivityUsage("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + userId);

        Username = "";
        Destroy(gameObject);
    }
    protected void PrintData(string user)
    {
        // print("data");
        // print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //     "student_id" + userId + StoryBook.FAVORITE_BOX.ToString() + Module.WORD.ToString() + "0"));
        // print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //     "student_id" + userId + StoryBook.FAVORITE_BOX.ToString() + Module.WORD.ToString() + "3"));
        // print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //     "student_id" + userId + StoryBook.FAVORITE_BOX.ToString() + Module.WORD.ToString() + "6"));
        // print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //     "student_id" + userId + StoryBook.FAVORITE_BOX.ToString() + Module.WORD.ToString() + "9"));
        // print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //     "student_id" + userId + StoryBook.FAVORITE_BOX.ToString() + Module.WORD.ToString() + "12"));
    }
    
  

    IEnumerator IELoad()
    {
        UserParentalManager.ins.PlayLoadClip();
        UserParentalManager.ins.IsUserActive = true;
        //UserAccountManager.ins.gameObject.SetActive(false);
        //needs animations
        yield return new WaitForSeconds(1f);
        UserAccountManager.ins.PanelSaves.SetActive(false);
        UserAccountManager.ins.PanelStart.SetActive(true);
        UserAccountManager.ins.gameObject.SetActive(false);
        //UserParentalManager.ins.gameObject.SetActive(false);
        //UserAccountManager.ins.gameObject.SetActive(false);
    }
}
