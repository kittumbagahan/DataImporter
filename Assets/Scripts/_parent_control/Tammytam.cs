using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class Tammytam : MonoBehaviour {
    public static Tammytam ins;
    [SerializeField]
    TextMeshProUGUI txtHello;

    [SerializeField]
    GameObject bubble;

    public TextMeshProUGUI TextHello { get; set; }

    public void Say(string sentence)
    {
        if (StoryBookSaveManager.ins.activeUser.Equals(""))
        {
            bubble.SetActive(false);
        }
        else
        {
            bubble.SetActive(true);
           // print("oldUsername is " + StoryBookSaveManager.instance.oldUsername);
        }
        txtHello.text = sentence;
    }

	public void Say2(string sentence)
	{
		txtHello.text = sentence;
	}
    void Start () {
        ins = this;
        //bubble.SetActive(true);
        //txtHello.text = StoryBookSaveManager.instance.oldUsername + abc;
	}

    
}
