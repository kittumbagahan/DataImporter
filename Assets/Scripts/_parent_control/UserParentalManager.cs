using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.UI;


public class UserParentalManager : MonoBehaviour {

    public static UserParentalManager ins;
    public GameObject parentControl, securityPanel;

    public AudioClip clipLoad, clipClick;

    GameObject parentControlClone, securityClone;
    AudioSource audSrc;
    bool isUserActive;
    Button b;
    public bool IsUserActive { set { isUserActive = value; } get { return isUserActive; } }

    void Start () {
        ins = this;
        audSrc = GetComponent<AudioSource>();
        //Invoke("CheckFirstRun", 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CheckFirstRun()
    {
        UserAccountManager uam;
        if (PlayerPrefs.GetString("0").Equals("", StringComparison.Ordinal) && PlayerPrefs.GetString("1").Equals("", StringComparison.Ordinal) && PlayerPrefs.GetString("2").Equals("", StringComparison.Ordinal))
        {
            print("NO EXISTING USERS");
            SpawnParentControl();
            parentControlClone.transform.Find("Panel_LoggedIn_User").gameObject.SetActive(false);
            uam = parentControlClone.transform.Find("USER_MANAGER").GetComponent<UserAccountManager>();//.gameObject.SetActive(true);
            uam.gameObject.SetActive(true);
            parentControlClone.transform.Find("USER_MANAGER").Find("Panel_Saves").gameObject.SetActive(true);
            //UserAccountManager.ins.PanelInput.SetActive(true);
            b = parentControlClone.transform.Find("USER_MANAGER").Find("Panel_Saves").Find("BtnSave1").GetComponent<Button>(); //.onClick.Invoke();
            ////UserAccountManager.ins.SelectedSlot = b.gameObject.GetComponent<UserAccount>();
            //b.onClick.Invoke();
            Invoke("CreateFirstAccount", 0.1f);
        }
        else
        {
           
        }
    }

    void CreateFirstAccount()
    {
        b.onClick.Invoke();
    }

    public void SpawnSecurity()
    {
        if (securityClone == null)
        {
            securityClone = (GameObject)Instantiate(securityPanel);
        }
        else
        {
            securityClone.SetActive(true);
        }
        
        //obj.transform.SetParent(GameObject.Find("Canvas").transform);
        //obj.transform.SetXPos(0);
        //obj.transform.SetAsLastSibling();
    }
    public void SpawnParentControl()
    {
        //GameObject obj = GameObject.Find("Canvas_Parental_Control");
        if (parentControlClone == null)
        {

            parentControlClone = (GameObject)Instantiate(parentControl);
          
        }
        else
        {
            parentControlClone.SetActive(true);
            if (!isUserActive)
            {
                try 
                {
                    //UserAccountManager.ins.PanelSaves.SetActive(true);
                }catch(Exception ex)
                {
                }
                
            }
            else
            {
                UserAccountManager.ins.PanelSaves.SetActive(false);
            }
        }

       
    }

    public void PlayLoadClip()
    {
        audSrc.PlayOneShot(clipLoad);
    }
    public void PlayClickClip()
    {
        audSrc.PlayOneShot(clipClick);
    }

    public void PlayClip(AudioClip clip )
    {
        audSrc.PlayOneShot(clip);
    }
}
