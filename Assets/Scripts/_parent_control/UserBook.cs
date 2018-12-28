using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class UserBook : MonoBehaviour {

    [SerializeField]
    Image cover;
    [SerializeField]
    TextMeshProUGUI txtInfo;
    [SerializeField]
    Text utxtInfo;
    public int readCount, playedCount;
    [SerializeField]
    public BookAccuracy bookAccuracy;

    public Sprite CoverSprite {
        get { return cover.sprite; }
    }
    public TextMeshProUGUI TxtInfo {
        set { txtInfo = value; }
        get { return txtInfo; }
    }
    public Text UTxtInfo {
        set { utxtInfo = value; }
        get { return utxtInfo; }
    }
    public int ReadCount {
        set 
        { 
            readCount = value;
            if (readCount > 999) readCount = 999;
            txtInfo.text = (readCount + playedCount).ToString();    
        }
        get { return readCount; }
    }
    public double AccuracyRate
    {
        get
        {
            return PlayedCount == 0 ? 0 : (bookAccuracy.total / bookAccuracy.max) * 100;
        }
    }

    public int PlayedCount
    {
        set
        {
            playedCount = value;
            if (playedCount > 999) playedCount = 999;
            txtInfo.text = (readCount + playedCount).ToString(); 
           // txtInfo.text = int.Parse(txtInfo.text + playedCount).ToString();
        }
        get { return playedCount; }
    }

    public Image Cover {
        set { cover = value; }
        get { return cover; }
    }

 
}
