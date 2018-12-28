using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public class MessageBox : MonoBehaviour {

    public static MessageBox ins;
    public GameObject win;
    public TextMeshProUGUI message;
    public Image iconImage;
    public Button btnYes;
    public Button btnNo;
    public Button btnCancel;
    public Button btnOk;
    
    UnityAction actionYes, actionNo;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Sprite iconErr, iconInfo, iconWarning;
    //[SerializeField]
    //GameObject objYesNo, objOK, objOkCancel;

    public enum MsgType { msgYesNo, msgOK, msgOkCancel };
    public enum MsgIcon { msgWarning, msgError, msgInformation };

    void Awake()
    {
        ins = this;
        //if (canvas != null) { canvas = GetComponent<Canvas>(); }
        //print("Messagebox object");
    }

    void Start()
    {
		btnYes.onClick.RemoveAllListeners();
		btnNo.onClick.RemoveAllListeners();
		btnOk.onClick.RemoveAllListeners();
		btnCancel.onClick.RemoveAllListeners();
    }
		
    protected void ChangeIcon(MsgIcon ic)
    {
        switch (ic)
        {
            case MsgIcon.msgError:
                iconImage.sprite = iconErr;
                break;
            case MsgIcon.msgInformation:
                iconImage.sprite = iconInfo;
                break;
            case MsgIcon.msgWarning:
                iconImage.sprite = iconWarning;
                break;
        }
    }

    public void ShowQuestion(string msg, MsgIcon icon, UnityAction uActionYes, UnityAction uActionNo)
    {
        ChangeIcon(icon);
        message.text = msg;
        btnYes.gameObject.SetActive(false);
        btnNo.gameObject.SetActive(false);
        btnOk.gameObject.SetActive(false);
        btnCancel.gameObject.SetActive(false);

        btnNo.onClick.RemoveAllListeners();
        btnYes.onClick.RemoveAllListeners();
        btnYes.gameObject.SetActive(true);
        btnNo.gameObject.SetActive(true);

        btnYes.onClick.AddListener(Close);
        btnNo.onClick.AddListener(Close);
        if(uActionYes != null) btnYes.onClick.AddListener(uActionYes);
        if(uActionNo != null) btnNo.onClick.AddListener(uActionNo);
       

        //transform.SetAsLastSibling();
        canvas.sortingOrder = 10;
        win.SetActive(true);
    }

    public void ShowOkCancel(string msg, MsgIcon icon, UnityAction uActionOk, UnityAction uActionCancel)
    {
        ChangeIcon(icon);
        message.text = msg;
        btnYes.gameObject.SetActive(false);
        btnNo.gameObject.SetActive(false);
        btnOk.gameObject.SetActive(false);
        btnCancel.gameObject.SetActive(false);

        btnOk.onClick.RemoveAllListeners();
        btnCancel.onClick.RemoveAllListeners();
       
        btnOk.gameObject.SetActive(true);
        btnCancel.gameObject.SetActive(true);

        btnCancel.onClick.AddListener(Close);
        btnOk.onClick.AddListener(Close);
        if(uActionOk != null) btnOk.onClick.AddListener(uActionOk);
        if(uActionCancel != null) btnCancel.onClick.AddListener(uActionCancel);
       

        //transform.SetAsLastSibling();
        canvas.sortingOrder = 10;
        win.SetActive(true);
    }

    public void ShowOk(string msg, MsgIcon icon, UnityAction uActionOk)
    {
        ChangeIcon(icon);
        message.text = msg;
        btnYes.gameObject.SetActive(false);
        btnNo.gameObject.SetActive(false);
        btnOk.gameObject.SetActive(false);
        btnCancel.gameObject.SetActive(false);

        btnOk.onClick.RemoveAllListeners();
       
        btnOk.gameObject.SetActive(true);

        btnOk.onClick.AddListener(Close);
        if (uActionOk != null) btnOk.onClick.AddListener(uActionOk);
       
        //transform.SetAsLastSibling();
        canvas.sortingOrder = 10;
        win.SetActive(true);
    }

    void Close()
    {
        //transform.SetAsFirstSibling();
        canvas.sortingOrder = -1;
        win.SetActive(false);
    
    }
}
