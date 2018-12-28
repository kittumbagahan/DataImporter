using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class ProgressBar : MonoBehaviour {

    [SerializeField]
    private RectTransform foreProgress, backgroundProgess;
    [SerializeField]
    private Text txtProgress, txtTitle;
    [SerializeField]
    float bg_Max_Width;
    float fore_width;
    public Text TextTitle {
        set { txtTitle = value; }
        get { return txtTitle; }
    }
    void Start()
    {
        //foreProgress = GameObject.Find("fore").GetComponent<RectTransform>();
        //backgroundProgess = GameObject.Find("bg").GetComponent<RectTransform>();
        //txtProgress = GameObject.Find("downloadProgress").GetComponent<TextMeshProUGUI>();
       // txtTitle = GameObject.Find("title").GetComponent<TextMeshProUGUI>();


        bg_Max_Width = backgroundProgess.GetWidth();
        //StartCoroutine(test());
    }
    //float n = 0;
    //void FixedUpdate()
    //{

    //    n++;
    //    SetProgress(n / 100);
    //}


    public void SetTitle(string s)
	{
		txtTitle.text =s;
	}

    public void SetProgress(float downloadProgress)
    {
        if(downloadProgress * 100 == 0)
        {
            txtProgress.text = "Loading...";
        }else
        {
            if (downloadProgress <= 1)
            {
                txtProgress.text = (downloadProgress * 100).ToString("0.00") + "%";
                foreProgress.SetWidth(bg_Max_Width * downloadProgress);
            }
            
            //if(foreProgress.GetWidth() < bg_Max_Width)
            //print(downloadProgress + "%");
                
        }
       
       
        
    }

	// number of process convert to 100%
	// n process = 2300
	//800 * n%


    //800 = 100%
    //
   

   

}
